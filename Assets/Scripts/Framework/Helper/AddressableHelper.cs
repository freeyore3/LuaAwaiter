using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using XLua;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using Cysharp.Threading.Tasks;

namespace Logic
{
    [LuaCallCSharp]
    public class AddressableHelper
    {
        public static async UniTaskVoid InstantiateCallback(string path, Action<GameObject> callback)
        {
            GameObject go = await InstantiateAsync(path);
            callback?.Invoke(go);
        }
        public static async UniTaskVoid InstantiateCallback(string path, Transform parent, Action<GameObject> callback)
        {
            GameObject go = await InstantiateAsync(path, parent);
            callback?.Invoke(go);
        }
        public static async UniTaskVoid InstantiateCallback(string path, CancellationTokenSource cancelToken, Action<GameObject> callback)
        {
            GameObject go = await InstantiateAsync(path, null, cancelToken);
            callback?.Invoke(go);
        }
        public static async UniTaskVoid InstantiateCallback(string path, Transform parent, CancellationTokenSource cancelToken, Action<GameObject> callback)
        {
            GameObject go = await InstantiateAsync(path, parent, cancelToken);
            callback?.Invoke(go);
        }
        public static async Task<GameObject> InstantiateAsync(string path, Transform parent = null)
        {
            var op = Addressables.InstantiateAsync(path, parent);
            await op.Task;
            if (op.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                var go = op.Result;
                return go;
            }
            
            return null;
        }
        public static async UniTask<GameObject> InstantiateAsync(string path, Transform parent, CancellationTokenSource cancelToken)
        {
            var op = Addressables.InstantiateAsync(path, parent);
            try
            {
                await op.WithCancellation(cancelToken.Token);
            }
            catch (OperationCanceledException)
            {
                Addressables.Release(op);
                return null;
            }
            
            if (op.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                var go = op.Result;
                return go;
            }
            
            return null;
        }
        public static void ReleaseInstance(GameObject go)
        {
            Addressables.ReleaseInstance(go);
        }
        public static async void LoadSceneCallback(string path, LoadSceneMode sceneMode, Action<SceneInstance> callback)
        {
            var sceneInstance = await LoadSceneAsync(path, sceneMode);
            callback?.Invoke(sceneInstance);
        }
        public static async Task<SceneInstance> LoadSceneAsync(string path, LoadSceneMode sceneMode)
        {
            var op = Addressables.LoadSceneAsync(path, sceneMode);
            await op;
            return op.Result;
        }
        public static async void UnloadSceneCallback(SceneInstance sceneInstance, Action callback)
        {
            await UnloadSceneAsync(sceneInstance);
            callback?.Invoke();
        }
        public static async Task UnloadSceneAsync(SceneInstance sceneInstance)
        {
            var op = Addressables.UnloadSceneAsync(sceneInstance);
            await op.Task;
        }
        public static async Task<TObject> LoadAsset<TObject>(string path)
        {
            var op = Addressables.LoadAssetAsync<TObject>(path);
            await op;
            return op.Result;
        }
        public static void UnloadAsset<TObject>(TObject obj)
        {
            Addressables.Release(obj);
        }
    }
}
