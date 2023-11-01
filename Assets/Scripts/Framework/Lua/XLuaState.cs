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
    public class XLuaState
    {
        public LuaEnv luaEnv;
        public IntPtr nativeState;
        
        LuaLoader m_LuaLoader;

        double lastGCTime;
        const double GCInterval = 1; // 1 second
        
#if RUN_APP_WITH_LUA_SOURCE
    static bool m_UseLuac = false;
#else
        static bool m_UseLuac = true;
#endif

        public byte[] CustomLoader(ref string filepath, out int fileLength)
        {
            return m_LuaLoader.Load(m_UseLuac, ref filepath, out fileLength);
        }

        public void Init(string persistentDataPath)
        {   
            m_LuaLoader = new LuaLoader();
            
            luaEnv = new LuaEnv();
            
            nativeState = XLuaNative.InitNativeState(luaEnv.L);
            XLuaNative.InitLogFile(nativeState, $"{persistentDataPath}/native.log");
            
            XLuaNative.InitCSharpDelegate(XLuaNative.LogMessageFromCpp);

            InitLuaEnv();
        }
        
        void InitLuaEnv()
        {
#if UNITY_EDITOR
            if (EditorPrefs.HasKey("bUseLuacVer1"))
                m_UseLuac = EditorPrefs.GetBool("bUseLuacVer1");
            else
                m_UseLuac = false;

            Common.Log.Debug("使用Luac: " + m_UseLuac);
#endif
            luaEnv.AddLoader(CustomLoader);
            luaEnv.AddBuildin("crypt", XLua.LuaDLL.Lua.LoadCrypt);
            luaEnv.AddBuildin("cjson", XLua.LuaDLL.Lua.LoadCjson);
            luaEnv.AddBuildin("sproto.core", XLua.LuaDLL.Lua.LoadSprotoCore);
            luaEnv.AddBuildin("lpeg", XLua.LuaDLL.Lua.LoadLpeg);
            luaEnv.AddBuildin("xxtea", XLua.LuaDLL.Lua.LoadXXTea);

            //luaEnv.AddBuildin("luaconfig", XLuaNative.LoadLuaConfig);

            luaEnv.AddBuildin("sharearray", XLuaNative.LoadLuaShareArray);

            luaEnv.AddBuildin("tableaccess", XLuaNative.LoadLuaTableAccess);

            luaEnv.AddBuildin("serialize", XLuaNative.LoadLuaSerialize);
            
            luaEnv.AddBuildin("luastate_sharedata", LuaStateShareDataReg.LuaOpen_ShareData);
        }

        public void Update(float deltaTime, double time)
        {
            m_LuaLoader.Update();
            
            if (time - lastGCTime > GCInterval)
            {
                this.luaEnv.Tick();
                    
                lastGCTime = time;
            }
        }
    }
}