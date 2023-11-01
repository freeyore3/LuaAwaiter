using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using XLua;
using UnityEngine.Scripting;
using System;
using System.Collections.Concurrent;
using Framework;

namespace Framework
{
    public class LuaStateShareDataReg
    {
        struct ShareDataItem
        {
            public IntPtr buffer;
            public int length;
        }
        
        static ConcurrentQueue<ShareDataItem> m_ShareDataQueue = new ConcurrentQueue<ShareDataItem>();
        
        [MonoPInvokeCallback(typeof(XLua.LuaDLL.lua_CSFunction))]
        public static int LuaOpen_ShareData(System.IntPtr L)
        {
            XLuaNative.lua_Reg[] l = new XLuaNative.lua_Reg[]
            {
                new XLuaNative.lua_Reg () { name = "queue_enqueue", func = lua_queue_enqueue },
                new XLuaNative.lua_Reg () { name = "queue_size", func = lua_queue_size },
                new XLuaNative.lua_Reg () { name = "queue_dequeue", func = lua_queue_dequeue },
            };
            XLua.LuaDLL.Lua.lua_createtable(L, 0, l.Length);
            XLuaNative.luaL_setfuncs(L, l, 0);

            return 1;
        }
        [MonoPInvokeCallback(typeof(XLua.LuaDLL.lua_CSFunction))]
        public static int lua_queue_enqueue(System.IntPtr L)
        {
            try
            {
                IntPtr buffer = XLua.LuaDLL.Lua.lua_touserdata(L, 1);
                int length = XLua.LuaDLL.Lua.xlua_tointeger(L, 2);

                m_ShareDataQueue.Enqueue(new ShareDataItem() { buffer = buffer, length = length });
            
                return 0;
            }
            catch (System.Exception e)
            {
                return XLua.LuaDLL.Lua.luaL_error(L, "c# exception:" + e);
            }
        }
        [MonoPInvokeCallback(typeof(XLua.LuaDLL.lua_CSFunction))]
        public static int lua_queue_size(System.IntPtr L)
        {
            try
            {
                XLua.LuaDLL.Lua.xlua_pushinteger(L, m_ShareDataQueue.Count);
            
                return 1;
            }
            catch (System.Exception e)
            {
                return XLua.LuaDLL.Lua.luaL_error(L, "c# exception:" + e);
            }
        }
        [MonoPInvokeCallback(typeof(XLua.LuaDLL.lua_CSFunction))]
        public static int lua_queue_dequeue(System.IntPtr L)
        {
            try
            {
                if (m_ShareDataQueue.TryDequeue(out var item))
                {
                    XLua.LuaDLL.Lua.lua_pushlightuserdata(L, item.buffer);
                    XLua.LuaDLL.Lua.xlua_pushinteger(L, item.length);
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            catch (System.Exception e)
            {
                return XLua.LuaDLL.Lua.luaL_error(L, "c# exception:" + e);
            }
        }
    }
}