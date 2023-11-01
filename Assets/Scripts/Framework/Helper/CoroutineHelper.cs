using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using XLua;
using Object = UnityEngine.Object;

namespace Framework
{
    [LuaCallCSharp]
    public class CoroutineHelper : Singleton<CoroutineHelper>
    {
        private GameObject goCoroutine_Runner;
        Coroutine_Runner coroutine_Runner;
        
        protected override void Init()
        {
            base.Init();

            goCoroutine_Runner = new UnityEngine.GameObject("Coroutine_Runner");
            UnityEngine.Object.DontDestroyOnLoad(goCoroutine_Runner);
            coroutine_Runner = goCoroutine_Runner.AddComponent<Coroutine_Runner>();
        }

        public async UniTaskVoid WaitForEndOfFrame(Action action)
        {
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
            action?.Invoke();
        }

        public async UniTaskVoid WaitForEndOfFrame(CancellationTokenSource cancelToken, Action action)
        {
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate, cancelToken.Token);
            action?.Invoke();
        }

        public async UniTaskVoid WaitForNextFrame(Action action)
        {
            await UniTask.NextFrame();
            action?.Invoke();
        }
        
        public async UniTaskVoid WaitForNextFrame(CancellationTokenSource cancelToken, Action action)
        {
            await UniTask.NextFrame(cancelToken.Token);
            action?.Invoke();
        }

        public override void UnInit()
        {
            if (goCoroutine_Runner != null)
            {
                coroutine_Runner.StopAllCoroutines();
                Object.Destroy(goCoroutine_Runner);
            }
            base.UnInit();
        }
    }
}