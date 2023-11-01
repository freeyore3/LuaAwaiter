using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Buffers;
using System.Runtime.InteropServices;
using UnityEditor;
using System.Threading;
using System.Collections.Concurrent;
using Microsoft.Extensions.ObjectPool;
using XLua;
using Cysharp.Threading.Tasks;
using Cysharp.Text;
using System.Diagnostics;

namespace Framework
{
    class FileReadCallback : AndroidJavaProxy, IDisposable
    {
        byte[] buffer;
        bool isDisposed;
        public FileReadCallback(string interfaceName) : base("com.unity.androidplugin.FileReadCallback")
        {

        }

        public void Dispose()
        {
            if (isDisposed)
                return;
            isDisposed = true;

            ArrayPool<byte>.Shared.Return(buffer);
        }

        public byte[] GetBuffer(int fileSize)
        {
            buffer = ArrayPool<byte>.Shared.Rent(fileSize);
            return buffer;
        }
    }

    [LuaCallCSharp]
    public class FileUtils
    {
        static AndroidJavaClass androidUtils;
        static AndroidJavaClass nativeAndroidUtils;
        public static void Initialize()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidUtils = new AndroidJavaClass("com.unity.androidplugin.AndroidUtils");
            nativeAndroidUtils = new AndroidJavaClass("com.unity.androidplugin.NativeAndroidUtils");

            AndroidJavaObject assetManager = androidUtils.CallStatic<AndroidJavaObject>("assetManager");

            nativeAndroidUtils.CallStatic("SetAssetManager", assetManager);
#endif
        }
        public static int ReadStreamingAssetAllBytes(string fileName, byte[] buffer, int offset, int count)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return XLuaNative.NativeJNIReadAllBytes(fileName, buffer, offset, count);
#else
            return ReadAllBytes(Path.Combine(Application.streamingAssetsPath, fileName), buffer, offset, count);
#endif
        }
        public static int ReadAllBytes(string fileName, byte[] buffer, int offset, int count)
        {
            if (!File.Exists(fileName))
            {
                return -1;
            }
            using (FileStream fs = File.OpenRead(fileName))
            {
                if (buffer == null)
                {
                    return (int)fs.Length;
                }
                else
                {
                    return fs.Read(buffer, offset, count);
                }
            }
        }
        public static bool IsStreamingAssetsFileExists(string fileName)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return androidUtils.CallStatic<bool>("IsFileExists", fileName);
#else
            return IsFileExists(Path.Combine(Application.streamingAssetsPath, fileName));
#endif
        }
        public static bool IsFileExists(string fileName)
        {
            return File.Exists(fileName);
        }
        public static async UniTask MirrorFolder(string srcPath, string destPath, string excludeFiles)
        {
            DirectoryInfo dirSrc = new DirectoryInfo(srcPath);
            if (!dirSrc.Exists)
            {
                throw new System.Exception($"failed to MirrorFolder: invalid src path {srcPath}");
            }
            DirectoryInfo dirDest = new DirectoryInfo(destPath);
            if (!dirDest.Exists)
            {
                Common.Log.Debug($"Create Directory {dirDest.FullName}");
                dirDest.Create();
            }

            if (RunOnUIThread.Instance != null) { }

            await UniTask.RunOnThreadPool(() =>
            {
                MirrorFolder(dirSrc, dirDest, excludeFiles.Split(';'));
            });
        }
        static bool IsFileExcluded(FileInfo file, string[] excludeFileExts)
        {
            if (excludeFileExts == null || excludeFileExts.Length == 0)
            {
                return false;
            }
            int idx = Array.IndexOf(excludeFileExts, file.Extension);
            if (idx == -1)
                return false;
            return true;
        }
        static bool CompareFileQuickly(FileInfo srcFile, FileInfo destFile)
        {
            if (srcFile.Length == destFile.Length/* && srcFile.LastWriteTime == destFile.LastWriteTime*/)
            {
                return true;
            }
            return false;
        }
        static async UniTask<bool> CompareFileAsync(FileInfo srcFile, FileInfo destFile)
        {
            UniTask<string> task1 = MD5Utils.GetFileMD5Async(srcFile.FullName);
            UniTask<string> task2 = MD5Utils.GetFileMD5Async(destFile.FullName);
            (string md5Src, string md5Dest) = await UniTask.WhenAll(task1, task2);
            if (md5Src == md5Dest)
            {
                return true;
            }
            return false;
        }
        public static string RelativePath(string rootPath, string path)
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

            rootPath = rootPath.Replace('\\', '/');
            path = path.Replace('\\', '/');
#endif
            string newPath = path.Replace(rootPath, "");
            if (newPath.Length > 0 && (newPath[0] == '/' || newPath[0] == '\\'))
            {
                newPath = newPath.Substring(1);
            }
            return newPath;
        }
        public static string RelativePath(DirectoryInfo rootPath, FileSystemInfo path)
        {
            string newPath = path.FullName.Replace(rootPath.FullName, "");
            if (newPath.Length > 0 && (newPath[0] == '/' || newPath[0] == '\\'))
            {
                newPath = newPath.Substring(1);
            }
            return newPath;
        }
        static Dictionary<string, DirectoryInfo> GetDirectoriesRelative(DirectoryInfo root, string searchPattern, SearchOption searchOption)
        {
            var map = new Dictionary<string, DirectoryInfo>();

            DirectoryInfo[] dirs = root.GetDirectories(searchPattern, searchOption);
            foreach (var child in dirs)
            {
                string relativePath = RelativePath(root, child);
                map.Add(relativePath, child);
            }

            return map;
        }
        static Dictionary<string, FileInfo> GetFilesRelative(DirectoryInfo root, string searchPattern, SearchOption searchOption, string[] excludeFileExts)
        {
            var map = new Dictionary<string, FileInfo>();

            FileInfo[] files = root.GetFiles(searchPattern, searchOption);
            foreach (var child in files)
            {
                string relativePath = RelativePath(root, child);
                map.Add(relativePath, child);
            }

            return map;
        }
        public delegate void MirrorCallback(string msg, float progress);
        public static MirrorCallback OnMirrorCallback;
        class SyncMessageData
        {
            public enum MessageType
            {
                RefreshAssetDatabase,
                OnProgress,
            }
            public MessageType Type;
            public string Message;
            public float Progress;
            public bool IsDone { get; set; }
        }

        static void OnMessage(object o)
        {
            SyncMessageData messageData = (SyncMessageData)o;
            switch (messageData.Type)
            {
                case SyncMessageData.MessageType.RefreshAssetDatabase:
#if UNITY_EDITOR
                    UnityEditor.AssetDatabase.Refresh();
#endif
                    break;
                case SyncMessageData.MessageType.OnProgress:
                    OnMirrorCallback?.Invoke(messageData.Message, messageData.Progress);
                    break;
            }

            OnRecycle(messageData);
        }
        static Microsoft.Extensions.ObjectPool.ObjectPool<SyncMessageData> objectPoolSyncMessageData = new DefaultObjectPool<SyncMessageData>(new DefaultPooledObjectPolicy<SyncMessageData>());
        static void OnRecycle(SyncMessageData messageData)
        {
            objectPoolSyncMessageData.Return(messageData);
        }
        static SyncMessageData NewSyncMessageData()
        {
            return objectPoolSyncMessageData.Get();
        }
        static void MirrorProgress(string message, float progress)
        {
            if (OnMirrorCallback != null)
            {
                var messageData = NewSyncMessageData();
                messageData.Message = message;
                messageData.Progress = progress;
                RunOnUIThread.Instance.Post(OnMessage, messageData);
            }
        }
        static void MirrorFolder(DirectoryInfo dirSrcRoot, DirectoryInfo dirDestRoot, string[] excludeFileExts)
        {
            MirrorProgress("Scaning folders...", 0);

            Dictionary<string, DirectoryInfo> destDirs = GetDirectoriesRelative(dirDestRoot, "*", SearchOption.AllDirectories);
            Dictionary<string, DirectoryInfo> srcDirs = GetDirectoriesRelative(dirSrcRoot, "*", SearchOption.AllDirectories);

            float total = destDirs.Count + srcDirs.Count;
            float current = 0;

            foreach (var iter in srcDirs)
            {
                string srcChildRelativePath = iter.Key;
                DirectoryInfo srcChildDir = iter.Value;

                MirrorProgress(srcChildDir.FullName, ++current / total);

                DirectoryInfo destChildDir;
                if (!destDirs.TryGetValue(srcChildRelativePath, out destChildDir))
                {
                    // add directory
                    string newPath = srcChildDir.FullName.Replace(dirSrcRoot.FullName, dirDestRoot.FullName);
                    Directory.CreateDirectory(newPath);
                }
            }
            foreach (var iter in destDirs)
            {
                string destChildRelativePath = iter.Key;
                DirectoryInfo destChildDir = iter.Value;

                MirrorProgress(destChildDir.FullName, ++current / total);

                DirectoryInfo srcChildDir;
                if (!srcDirs.TryGetValue(destChildRelativePath, out srcChildDir))
                {
                    // remove directory
#if UNITY_EDITOR
                    string p = "Assets/" + RelativePath(Application.dataPath, destChildDir.FullName);
                    UnityEditor.AssetDatabase.DeleteAsset(p);
#else
                    destChildDir.Delete(true);
#endif

                    Common.Log.Debug($"Remove Directory {destChildDir}");
                }
            }

#if UNITY_EDITOR
            RunOnUIThread.Instance.Send(
                (data) =>
                {
                    UnityEditor.AssetDatabase.Refresh();
                },
                null);
#endif

            Dictionary<string, FileInfo> destFiles = GetFilesRelative(dirDestRoot, "*", SearchOption.AllDirectories, excludeFileExts);
            Dictionary<string, FileInfo> srcFiles = GetFilesRelative(dirSrcRoot, "*", SearchOption.AllDirectories, excludeFileExts);

            total = destFiles.Count + srcFiles.Count;
            current = 0;

            foreach (var iter in srcFiles)
            {
                string srcFileRelativePath = iter.Key;
                FileInfo srcFile = iter.Value;

                MirrorProgress(srcFile.FullName, ++current / total);

                if (IsFileExcluded(srcFile, excludeFileExts))
                {
                    continue;
                }

                FileInfo destFile;
                if (!destFiles.TryGetValue(srcFileRelativePath, out destFile))
                {
                    // add file
                    string newPath = srcFile.FullName.Replace(dirSrcRoot.FullName, dirDestRoot.FullName);
                    File.Copy(srcFile.FullName, newPath, true);

                    Common.Log.Debug($"Copy File {newPath}");
                }
                else
                {
                    bool isFileSame = CompareFileQuickly(srcFile, destFile);
                    if (isFileSame)
                    {
                        var awaiter = CompareFileAsync(srcFile, destFile);
                        isFileSame = awaiter.GetAwaiter().GetResult();
                    }
                    if (!isFileSame)
                    {
                        // modify file
                        File.Copy(srcFile.FullName, destFile.FullName, true);

                        Common.Log.Debug($"Copy File {destFile.FullName}");
                    }
                }
            }
            foreach (var iter in destFiles)
            {
                string destFileRelativePath = iter.Key;
                FileInfo destFile = iter.Value;

                MirrorProgress(destFile.FullName, ++current / total);

                if (IsFileExcluded(destFile, excludeFileExts))
                {
                    continue;
                }
                FileInfo srcFile;
                if (!srcFiles.TryGetValue(destFileRelativePath, out srcFile))
                {
                    // delete file
                    destFile.Delete();

                    Common.Log.Debug($"Remove File {destFile.FullName}");
                }
            }
        }
        public static void CopyDirectory(string srcDirPath, string destDirPath, string[] excludeFileExts, SearchOption searchOption)
        {
            DirectoryInfo srcDir = new DirectoryInfo(srcDirPath);
            DirectoryInfo destDir = new DirectoryInfo(destDirPath);

            if (!destDir.Exists)
            {
                Common.Log.Debug($"Create Directory {destDir.FullName}");
                destDir.Create();
            }

            foreach (var child in srcDir.EnumerateDirectories("*", searchOption))
            {
                string newPath = child.FullName.Replace(srcDir.FullName, destDir.FullName);
                Directory.CreateDirectory(newPath);

                Common.Log.Debug($"Create Directory {newPath}");
            }

            foreach (var child in srcDir.EnumerateFiles("*", searchOption))
            {
                if (IsFileExcluded(child, excludeFileExts))
                {
                    continue;
                }
                string newPath = child.FullName.Replace(srcDir.FullName, destDir.FullName);
                File.Copy(child.FullName, newPath, true);
                //await CopyFileAsync(child.FullName, newPath);

                Common.Log.Debug($"Copy File {child.FullName} => {newPath}");
            }
        }
        public static async UniTask CopyDirectoryAsync(string srcDirPath, string destDirPath, string[] excludeFileExts, SearchOption searchOption)
        {
            await UniTask.RunOnThreadPool(() => CopyDirectory(srcDirPath, destDirPath, excludeFileExts, searchOption));
        }
        public static UniTask DeleteDirectoryAsync(string dirPath)
        {
            return UniTask.FromResult(0);
        }
        public static async UniTask CopyFileAsync(string srcFile, string destFile)
        {
            using (var src = new FileStream(srcFile, FileMode.Open, FileAccess.Read))
            {
                DirectoryInfo path = new DirectoryInfo(Path.GetDirectoryName(destFile));
                if (!path.Exists)
                {
                    path.Create();
                }
                using (var dest = new FileStream(destFile, FileMode.Create, FileAccess.Write))
                {
                    await src.CopyToAsync(dest);
                }
            }
        }

        // 合并文件夹
        public static void MergeFolder(string srcFolder, string destFolder)
        {
            if (!Directory.Exists(srcFolder))
            {
                return;
            }
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }
            DirectoryInfo dir = new DirectoryInfo(srcFolder);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string targetFilePath = Path.Combine(destFolder, file.Name);
                file.CopyTo(targetFilePath, true);
            }
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestFolder = Path.Combine(destFolder, subDir.Name);
                MergeFolder(subDir.FullName, newDestFolder);
            }
        }

        public static void CopyDirectorySync(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectorySync(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        public static void ForeachFileInDir(System.IO.DirectoryInfo root, Action<FileInfo> op, string partern = "*.*")
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            try
            {
                files = root.GetFiles(partern);
            }
            catch (Exception e)
            {
                Common.Log.Error(e.Message);
            }


            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    op?.Invoke(fi);
                }

                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    ForeachFileInDir(dirInfo, op);
                }
            }
        }

        public static string FormatFileSize(int size)
        {
            if (size > 1024 * 1024)
            {
                return ZString.Format("{0:##} MB", 1.0f * size / 1024 / 1024);
            }
            else if (size > 1024)
            {
                return ZString.Format("{0:##} KB", 1.0f * size / 1024);
            }
            else
            {
                return ZString.Concat(size, " byte");
            }
        }

        public static long GetFreeDiskSpace()
        {
#if UNITY_ANDROID && !UNITY_EDITOR && false
            var GetFreeDiskSpace = androidUtils.CallStatic<long>("GetFreeDiskSpace");
            Common.Log.Debug($"剩余空间1{GetFreeDiskSpace}");
            Common.Log.Debug($"剩余空间{OneMT.SDK.OneMTTool.GetAvailableDiskSpace()}");
            return OneMT.SDK.OneMTTool.GetAvailableDiskSpace();


#elif UNITY_IPHONE && !UNITY_EDITOR && false
            Common.Log.Debug($"剩余空间{OneMT.SDK.OneMTTool.GetAvailableDiskSpace()}");
            return OneMT.SDK.OneMTTool.GetAvailableDiskSpace();
#else
            return 2147483647;
#endif
        }
        public static void SelectFileInExplorer(string path)
        {
#if UNITY_EDITOR_WIN
            Process.Start("explorer.exe", "/select," + path);
#elif UNITY_EDITOR && UNITY_STANDALONE_OSX
            Process.Start("open", "-R " + path);
#endif
        }
    }

    class SyncCallbackResult
    {
        public object Data;
        public bool IsDone;
    }
    class RunOnUIThread : IDisposable
    {
        Microsoft.Extensions.ObjectPool.ObjectPool<SyncCallbackResult> objectPool = new DefaultObjectPool<SyncCallbackResult>(new DefaultPooledObjectPolicy<SyncCallbackResult>());

        ConcurrentQueue<(SendOrPostCallback, object)> messageQueue = new ConcurrentQueue<(SendOrPostCallback, object)>();
        bool isPlaying;
        public RunOnUIThread()
        {
            // Application.isPlaying can only call by main thread
            isPlaying = Application.isPlaying;

#if UNITY_EDITOR
            EditorApplication.update += Update;
#endif
        }
        static RunOnUIThread _Instance;
        public static RunOnUIThread Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new RunOnUIThread();
                }
                return _Instance;
            }
        }
        public void Post(SendOrPostCallback callback, object data = null)
        {
            if (isPlaying)
            {
                SynchronizationContext.Current.Post(callback, data);
            }
            else
            {
                messageQueue.Enqueue((callback, data));
            }
        }
        public async UniTask SendAsync(SendOrPostCallback callback, object data = null)
        {
            SyncCallbackResult result = objectPool.Get();
            result.Data = data;
            result.IsDone = false;

            if (isPlaying)
            {
                SynchronizationContext.Current.Post(callback, result);
            }
            else
            {
                messageQueue.Enqueue((callback, result));
            }

            while (!result.IsDone)
                await UniTask.Yield();
        }
        public void Send(SendOrPostCallback callback, object data = null)
        {
            var awaiter = SendAsync(callback, data);
            awaiter.GetAwaiter().GetResult();
        }
        void Update()
        {
            while (messageQueue.Count > 0)
            {
                if (messageQueue.TryDequeue(out (SendOrPostCallback callback, object data) data))
                {
                    if (data.data is SyncCallbackResult result)
                    {
                        data.callback?.Invoke(result.Data);

                        result.IsDone = true;
                        objectPool.Return(result);
                    }
                    else
                    {
                        data.callback?.Invoke(data.data);
                    }
                }
            }
        }
        public void Dispose()
        {
#if UNITY_EDITOR
            EditorApplication.update -= Update;
#endif
        }
    }
}
