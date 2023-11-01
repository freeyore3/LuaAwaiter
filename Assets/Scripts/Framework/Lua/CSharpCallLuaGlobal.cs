using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Framework
{
    public class CSharpCallLuaGlobal
    {
        LuaEnv luaEnv;

        Action<float, float, int> luaUpdate = null;
        Action<float, float, int> luaLateUpdate = null;
        Action<float> luaFixedUpdate = null;

        Action<bool, bool, bool> luaSetLogLevel = null;

        Func<string, string> _i18nFunc;
        public Func<string, string> i18nFunc
        {
            get
            {
                if(_i18nFunc == null)
                {
                    _i18nFunc = luaEnv.Global.Get<Func<string, string>>("i18n_html");
                }
                return _i18nFunc;
            }
        }

        public CSharpCallLuaGlobal(LuaEnv luaEnv)
        {
            this.luaEnv = luaEnv;
        }

        public void Init()
        {
            luaUpdate = luaEnv.Global.Get<Action<float, float, int>>("CSharpCallLuaUpdate");
            luaLateUpdate = luaEnv.Global.Get<Action<float, float, int>>("CSharpCallLuaLateUpdate");
            luaFixedUpdate = luaEnv.Global.Get<Action<float>>("CSharpCallLuaFixedUpdate");
        }

        public void Call(string funcName)
        {
            Action luaFunc = luaEnv.Global.Get<Action>(funcName);
            luaFunc?.Invoke();    
        }
        
        public void Update()
        {
            if (luaUpdate != null)
            {
                luaUpdate(Time.deltaTime, Time.unscaledDeltaTime, Time.frameCount);
            }
            if (luaLateUpdate != null)
            {
                luaLateUpdate(Time.deltaTime, Time.unscaledDeltaTime, Time.frameCount);
            }
        }

        public Action<bool, bool, bool> LuaSetLogLevel
        {
            get
            {
                if (luaSetLogLevel == null)
                {
                    luaSetLogLevel = luaEnv.Global.Get<Action<bool, bool, bool>>("SetLogLevel");
                    if (luaSetLogLevel == null)
                    {
                        Common.Log.Error($"Could not find lua global SetLogLevel");
                    }
                }
                return luaSetLogLevel;
            }
        }

        public void Purge()
        {
            luaUpdate=null;
            luaLateUpdate = null;
            luaFixedUpdate = null;
            luaSetLogLevel = null;
            _i18nFunc = null;
            luaEnv = null;
        }

    }
}
