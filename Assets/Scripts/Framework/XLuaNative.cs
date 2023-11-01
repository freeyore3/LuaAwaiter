using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using XLua;
using UnityEngine.Scripting;

[Preserve]
public class XLuaNative
{
#if (UNITY_IPHONE || UNITY_TVOS || UNITY_WEBGL || UNITY_SWITCH) && !UNITY_EDITOR
        const string LUADLL = "__Internal";
#else
    const string LUADLL = "xlua";
#endif

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int luaopen_config(System.IntPtr L);
    [MonoPInvokeCallback(typeof(XLua.LuaDLL.lua_CSFunction))]
    public static int LoadLuaConfig(System.IntPtr L)
    {
        return luaopen_config(L);
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int NativeJNIReadAllBytes(string fileName, byte[] buffer, int offset, int count);
#endif
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int EncodeBytes(string fileName, byte[] buffer, int bufsize);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int DecodeBytes(string fileName, byte[] buffer, int bufsize);

    const int LogLevel_Debug = 0;
    const int LogLevel_Warning = 1;
    const int LogLevel_Error = 2;

    [MonoPInvokeCallback(typeof(LogDelegate))]
    public static void LogMessageFromCpp(int level, string message)
    {
        string s_msg = "[Native]: " + message;
        switch (level)
        {
            case LogLevel_Debug:
                Common.Log.Debug(s_msg);
                break;
            case LogLevel_Warning:
                Common.Log.Warning(s_msg);
                break;
            case LogLevel_Error:
                Common.Log.Error(s_msg);
                break;
        }
        
    }
    public enum LogLevel
    {
        Debug,
        Warnning,
        Error,
    };

    public delegate void LogDelegate(int level, string message);

    [DllImport(LUADLL, EntryPoint = "InitCSharpDelegate", CallingConvention = CallingConvention.Cdecl)]
    public static extern void InitCSharpDelegate(LogDelegate callback);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void InitLogFile(IntPtr native_State, string fileName);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void OnApplicationQuit(IntPtr native_State);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ReleaseAllNativeStates();
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct callinfo
    {
        public IntPtr name; // const char* name
        public IntPtr source; // const char* source
        public IntPtr what; // const char* what
        public int line;
    }
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int lua_get_callinfo(System.IntPtr L, int level, string what, IntPtr pcallinfo);

    public struct lua_Reg
    {
        public string name;
        public XLua.LuaDLL.lua_CSFunction func;
    }

    public static void luaL_setfuncs(System.IntPtr L, XLuaNative.lua_Reg[] l, int nup)
    {
        foreach (XLuaNative.lua_Reg r in l)
        {
            for (int i = 0; i < nup; ++i)
                XLua.LuaDLL.Lua.lua_pushvalue(L, -nup);
            XLua.LuaDLL.Lua.lua_pushstdcallcfunction(L, r.func, nup);
            XLuaNative.lua_setfield(L, -(nup + 2), r.name);
        }
    }

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int lua_setfield(IntPtr L, int idx, string k);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr InitNativeState(System.IntPtr L);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetNativeConfigTable(IntPtr native_State, string name, int isAndroidStreamingAsset, string fileName);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetRowCount(IntPtr native_State, int hNCT);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr GetNativeLastErrorMessage(IntPtr native_State);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetRowBytes(IntPtr native_State, int hNCT, int rowNum, byte[] buffer, ref int bufsize);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetFieldCount(IntPtr native_State, int hNCT);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetField(IntPtr native_State, int hNCT, int idx, byte[] fieldName, ref int fieldNameSize, out int fieldType, out int fieldSize);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int CloseNativeConfigTable(IntPtr native_State, int hNCT);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void CloseAllNativeConfigTables(IntPtr native_State);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetPrimaryFields(IntPtr native_State, int hNCT, out int field1Index);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetIndexesCount(IntPtr native_State, int hNCT);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetIndexName(IntPtr native_State, int hNCT, int index, byte[] buf, ref int bufSize);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetIndexFields(IntPtr native_State, int hNCT, int index, out int fieldCount, out int field1Index, out int field2Index, out int field3Index);
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyStruct
    {
        public int fieldType;
        public long keyInt;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string keyString;
    }
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetPrimaryKeyRows(IntPtr native_State, int hNCT, ref KeyStruct key1, out short rowNum);
    [StructLayout(LayoutKind.Sequential)]
    public struct IndexKeyValue
    {
        public int fieldType;
        public long keyInt;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string keyString;
    }
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetIndexesRows(IntPtr native_State, int hNCT, string indexName, ref IndexKeyValue key1, short[] rowNums, int rowNumsOffset, ref int readCount);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetIndexes_2_Rows(IntPtr native_State, int hNCT, string indexName, ref IndexKeyValue key1, ref IndexKeyValue key2, short[] rowNums, int rowNumsOffset, ref int readCount);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int GetIndexes_3_Rows(IntPtr native_State, int hNCT, string indexName, ref IndexKeyValue key1, ref IndexKeyValue key2, ref IndexKeyValue key3, short[] rowNums, int rowNumsOffset, ref int readCount);


    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int luaopen_sharearray(System.IntPtr L);
    [MonoPInvokeCallback(typeof(XLua.LuaDLL.lua_CSFunction))]
    public static int LoadLuaShareArray(System.IntPtr L)
    {
        return luaopen_sharearray(L);
    }

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int sharearray_getint(IntPtr native_State, IntPtr sharearray, int idx, out long value);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int sharearray_setint(IntPtr native_State, IntPtr sharearray, int idx, long value);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int sharearray_getdouble(IntPtr native_State, IntPtr sharearray, int idx, out double value);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int sharearray_setdouble(IntPtr native_State, IntPtr sharearray, int idx, double value);
    

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int luaopen_serialize(System.IntPtr native_State);
    [MonoPInvokeCallback(typeof(XLua.LuaDLL.lua_CSFunction))]
    public static int LoadLuaSerialize(System.IntPtr L)
    {
        return luaopen_serialize(L);
    }
    
    
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int luaopen_tableaccess(System.IntPtr L);
    [MonoPInvokeCallback(typeof(XLua.LuaDLL.lua_CSFunction))]
    public static int LoadLuaTableAccess(System.IntPtr L)
    {
        return luaopen_tableaccess(L);
    }

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int tableaccess_getint(IntPtr native_State, IntPtr tableAccess, int idx, out long value);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int tableaccess_setint(IntPtr native_State, IntPtr tableAccess, int idx, long value);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int tableaccess_getdouble(IntPtr native_State, IntPtr tableAccess, int idx, out double value);

    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int tableaccess_setdouble(IntPtr native_State, IntPtr tableAccess, int idx, double value);



    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void lua_getfield(IntPtr L, int idx, string key);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void lua_createtable (IntPtr native_State, int narr, int nrec);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int luaL_newmetatable (IntPtr native_State, string tname);

    class InternalGlobals
    {
        internal static volatile int LUA_REGISTRYINDEX = -10000;
    }
    public static void luaL_getmetatable(IntPtr native_State, string name)
    {
        lua_getfield(native_State, /*XLua.*/InternalGlobals.LUA_REGISTRYINDEX, name);
    }
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr AssetFile_Open(string filename);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int AssetFile_Seek(IntPtr pNativeAsset, int offset);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int AssetFile_ReadBytes(IntPtr pNativeAsset, byte[] buf, int count);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void AssetFile_Close(IntPtr pNativeAsset);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void pushlstring_ex(IntPtr native_State, byte[] buf, int offset, int length);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void pushcsobj(IntPtr native_State, string tname, int key);
    [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int getcsobj(IntPtr native_State, int index);
}
