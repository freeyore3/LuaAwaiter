using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using Framework;
using Logic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using XLua;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Framework
{
    public class XLuaManager : Singleton<XLuaManager>
    {
        public XLuaState LuaState { get; private set; }
        
        public CSharpCallLuaGlobal LuaGlobal;

        private bool isInited;
        public bool IsInited => isInited;
        
        protected override void Init()
        {
            base.Init();
            isInited = false;
        }

        public override void UnInit()
        {
            base.UnInit();
        }

        public void InitLua(string mainLuaFile)
        {
            if (isInited)
                return;
            
            FileUtils.Initialize();

            LuaState = new XLuaState();
            LuaState.Init(Application.persistentDataPath);
            
            LuaState.luaEnv.DoString($"require '{mainLuaFile}'");

            LuaGlobal = new CSharpCallLuaGlobal(LuaState.luaEnv);
            LuaGlobal.Init();
            
            isInited = true;
        }
        
        public void Update(float deltaTime, double time)
        {
            LuaGlobal?.Update();
            
            LuaState?.Update(deltaTime, time);
        }
    }
}