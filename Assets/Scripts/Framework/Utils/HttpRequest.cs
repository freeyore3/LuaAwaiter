using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using XLua;

namespace Framework
{
    [LuaCallCSharp]
    public class HttpRequest
    {
        public static async void SendGet(string uri, Action<UnityWebRequest.Result, DownloadHandler> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                await webRequest.SendWebRequest();
                callback?.Invoke(webRequest.result, webRequest.downloadHandler);
            }
        }
        
        public static async void SendGet(string uri, CancellationTokenSource cancelToken, Action<UnityWebRequest.Result, DownloadHandler> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                try
                {
                    await webRequest.SendWebRequest().WithCancellation(cancelToken.Token);
                    callback?.Invoke(webRequest.result, webRequest.downloadHandler);
                }
                catch (OperationCanceledException)
                {
                    callback?.Invoke(webRequest.result, null);
                }
            }
        }
    }
}