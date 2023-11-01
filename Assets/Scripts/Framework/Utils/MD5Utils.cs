using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework
{
    public static class MD5Utils
    {
        public static string ByteToHexStr(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X2"));
                }
            }
            return sb.ToString();
        }

        public static string GetFileMD5(string file)
        {
            using (FileStream fs = File.OpenRead(file))
            {
                return GetFileMD5(fs);
            }
        }
        public static string GetFileMD5(byte[] data)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] md5_result = md5.ComputeHash(data);

                return ByteToHexStr(md5_result);
            }
        }
        public static string GetFileMD5(byte[] data, int offset, int count)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] md5_result = md5.ComputeHash(data, offset, count);

                return ByteToHexStr(md5_result);
            }
        }
        public static string GetFileMD5(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] md5_result = md5.ComputeHash(stream);

                return ByteToHexStr(md5_result);
            }
        }
        public static UniTask<string> GetFileMD5Async(string file)
        {
            return UniTask.RunOnThreadPool(() => GetFileMD5(file));
        }
        public static UniTask<string> GetFileMD5Async(Stream stream)
        {
            return UniTask.RunOnThreadPool(() => GetFileMD5(stream));
        }
        public static bool IsTheSameFile(string srcFile, string destFile)
        {
            using (FileStream fsSrc = new FileStream(srcFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream fsDest = new FileStream(destFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    long srcLength = fsSrc.Length;
                    long destLength = fsDest.Length;
                    if (srcLength != destLength)
                    {
                        return false;
                    }

                    string srcMD5 = GetFileMD5(fsSrc);
                    string destMD5 = GetFileMD5(fsDest);
                    return srcMD5 == destMD5;
                }
            }
        }
    }
}
