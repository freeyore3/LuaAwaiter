using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Framework;
using System.Buffers;
using UnityEditor;
using Cysharp.Text;

public class LuaLoader
{
    private Stack<byte[]> m_LuaLoaderBuffers = new Stack<byte[]>();
    public bool m_EnableLuaPanda;

    public LuaLoader()
    {
        m_EnableLuaPanda = UnityUtils.EnableLuaPanda;
    }
    public byte[] Load(bool bUseLuac, ref string filepath, out int fileLength)
    {
        ReturnLuaLoaderBuffer();
        string pathnormlize = filepath.Replace('.', '/');

        if (m_EnableLuaPanda)
        {
            if (filepath == "libpdebug")
            {
                fileLength = 0;
                return null;
            }
        }

        filepath = ZString.Format("{0}.lua", pathnormlize);

        string path;
        int length = -1;
        int readed;
        byte[] buffer = null;
        bool bNeedDecodeLuaBytes = false;
        if (bUseLuac)
        {
            bNeedDecodeLuaBytes = true;
            path = ZString.Format("LuaScripts/{0}.lua.bytes", pathnormlize);
        }
        else
        {
            bNeedDecodeLuaBytes = false;
            path = ZString.Format("LuaScripts/{0}.lua", pathnormlize);
        }

#if UNITY_EDITOR && !ENABLE_HOT_UPDATE_TEST_ON_EDITOR
        if (bUseLuac)
        {
            length = FileUtils.ReadStreamingAssetAllBytes(path, null, 0, 0);
        }
        else
        {
            length = FileUtils.ReadAllBytes(Path.Combine(Application.dataPath, path), null, 0, 0);
        }
#else
        length = FileSystem.Instance.ReadAllBytes(path, null, 0, 0);
#endif
        if (length < 0)
        {
            Common.Log.Error($"failed to get lua file length. {path}");
            fileLength = 0;
            return null;
        }

        buffer = ArrayPool<byte>.Shared.Rent(length);
#if UNITY_EDITOR && !ENABLE_HOT_UPDATE_TEST_ON_EDITOR
        if (bUseLuac)
        {
            readed = FileUtils.ReadStreamingAssetAllBytes(path, buffer, 0, length);
        }
        else
        {
            readed = FileUtils.ReadAllBytes(Path.Combine(Application.dataPath, path), buffer, 0, length);
        }
#else
        
        readed = FileSystem.Instance.ReadAllBytes(path, buffer, 0, length);
#endif
        if (length != readed)
        {
            Common.Log.Error($"failed to read lua file. {path} length={length}, readed={readed}");
            fileLength = 0;
            return null;
        }

        if (bNeedDecodeLuaBytes)
        {
            XLuaNative.DecodeBytes(path, buffer, length);
        }

#if UNITY_EDITOR
        VerifyFileCaseSensitivity(path);
#endif

        fileLength = length;
        m_LuaLoaderBuffers.Push(buffer);
        return buffer;
    }

    private static string GetCaseSensitivePath(string basePath, string relatePath)
    {
        string path = basePath;
        string sensitivePath = null;
        try
        {
            var strList = relatePath.Split('/');
            for (int i = 0; i < strList.Length - 1; i++)
            {
                var dirName = strList[i];
                var dirNameLower = (path + "/" + dirName).ToLower();
                // Common.Log.Debug("dirNameLower: " + dirNameLower);

                bool dirNameFound = false;
                
                foreach (var currDirPath in Directory.EnumerateDirectories(path, dirName))
                {
                    var currDirPathUnixStyle = currDirPath.Replace("\\", "/");
                    // Common.Log.Debug("currDirPathUnixStyle: " + currDirPathUnixStyle);
                    if (currDirPathUnixStyle.ToLower().Equals(dirNameLower))
                    {
                        dirNameFound = true;
                        path = currDirPathUnixStyle;
                        break;
                    }
                }

                if (!dirNameFound)
                {
                    path = null;
                    break;
                }
            }

            if (path != null)
            {
                var fileName = strList[strList.Length - 1];
                var fileNameLower = (path + "/" + fileName).ToLower();
                foreach (var currFilePath in Directory.EnumerateFiles(path, fileName))
                {
                    var currFilePathUnixStyle = currFilePath.Replace("\\", "/");
                    // Common.Log.Debug("currFilePathUnixStyle: " + currFilePathUnixStyle);
                    if (currFilePathUnixStyle.ToLower().Equals(fileNameLower))
                    {
                        sensitivePath = currFilePathUnixStyle;
                        break;
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            // Log("Path not found: " + path);
        }
        return sensitivePath;
    }
    private static void VerifyFileCaseSensitivity(string filePath)
    {
        // Common.Log.Debug($"VerifyFileCaseSensitivity: {filePath}");
        // System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        // stopwatch.Start();
        var basePath = Application.dataPath.Replace("\\", "/");
        var relatePath = filePath.Replace("\\", "/");
        var fullPath = Path.Combine(basePath, relatePath).Replace("\\", "/");
        // Common.Log.Warning($"fullPath {fullPath}");
        if (File.Exists(fullPath))
        {
            var caseSensitivePath = GetCaseSensitivePath(basePath, relatePath);
            // Common.Log.Warning($"caseSensitivePath: {caseSensitivePath}");
            if (caseSensitivePath == null || !caseSensitivePath.Equals(fullPath))
            {
                Debug.LogError($"{filePath} 大小写不匹配");
            }
        }
        else
        {
            Debug.LogError($"{fullPath} 文件不存在");
        }
        // Common.Log.Debug($"VerifyFileCaseSensitivity cost {stopwatch.ElapsedMilliseconds} ms");
    }



    void ReturnLuaLoaderBuffer()
    {
        while (m_LuaLoaderBuffers.Count > 0)
        {
            byte[] buffer = m_LuaLoaderBuffers.Pop();

            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    public void Update()
    {
        ReturnLuaLoaderBuffer();
    }
}
