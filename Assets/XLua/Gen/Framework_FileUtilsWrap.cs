#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class FrameworkFileUtilsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Framework.FileUtils);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 18, 1, 1);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Initialize", _m_Initialize_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadStreamingAssetAllBytes", _m_ReadStreamingAssetAllBytes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadAllBytes", _m_ReadAllBytes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsStreamingAssetsFileExists", _m_IsStreamingAssetsFileExists_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsFileExists", _m_IsFileExists_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "MirrorFolder", _m_MirrorFolder_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RelativePath", _m_RelativePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyDirectory", _m_CopyDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyDirectoryAsync", _m_CopyDirectoryAsync_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DeleteDirectoryAsync", _m_DeleteDirectoryAsync_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyFileAsync", _m_CopyFileAsync_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "MergeFolder", _m_MergeFolder_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyDirectorySync", _m_CopyDirectorySync_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ForeachFileInDir", _m_ForeachFileInDir_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FormatFileSize", _m_FormatFileSize_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFreeDiskSpace", _m_GetFreeDiskSpace_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SelectFileInExplorer", _m_SelectFileInExplorer_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "OnMirrorCallback", _g_get_OnMirrorCallback);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "OnMirrorCallback", _s_set_OnMirrorCallback);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new Framework.FileUtils();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Framework.FileUtils constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Framework.FileUtils.Initialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadStreamingAssetAllBytes_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _fileName = LuaAPI.lua_tostring(L, 1);
                    byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = Framework.FileUtils.ReadStreamingAssetAllBytes( _fileName, _buffer, _offset, _count );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadAllBytes_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _fileName = LuaAPI.lua_tostring(L, 1);
                    byte[] _buffer = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        var gen_ret = Framework.FileUtils.ReadAllBytes( _fileName, _buffer, _offset, _count );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsStreamingAssetsFileExists_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _fileName = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = Framework.FileUtils.IsStreamingAssetsFileExists( _fileName );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsFileExists_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _fileName = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = Framework.FileUtils.IsFileExists( _fileName );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MirrorFolder_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _srcPath = LuaAPI.lua_tostring(L, 1);
                    string _destPath = LuaAPI.lua_tostring(L, 2);
                    string _excludeFiles = LuaAPI.lua_tostring(L, 3);
                    
                        var gen_ret = Framework.FileUtils.MirrorFolder( _srcPath, _destPath, _excludeFiles );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RelativePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _rootPath = LuaAPI.lua_tostring(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = Framework.FileUtils.RelativePath( _rootPath, _path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& translator.Assignable<System.IO.FileSystemInfo>(L, 2)) 
                {
                    System.IO.DirectoryInfo _rootPath = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    System.IO.FileSystemInfo _path = (System.IO.FileSystemInfo)translator.GetObject(L, 2, typeof(System.IO.FileSystemInfo));
                    
                        var gen_ret = Framework.FileUtils.RelativePath( _rootPath, _path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Framework.FileUtils.RelativePath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _srcDirPath = LuaAPI.lua_tostring(L, 1);
                    string _destDirPath = LuaAPI.lua_tostring(L, 2);
                    string[] _excludeFileExts = (string[])translator.GetObject(L, 3, typeof(string[]));
                    System.IO.SearchOption _searchOption;translator.Get(L, 4, out _searchOption);
                    
                    Framework.FileUtils.CopyDirectory( _srcDirPath, _destDirPath, _excludeFileExts, _searchOption );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyDirectoryAsync_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _srcDirPath = LuaAPI.lua_tostring(L, 1);
                    string _destDirPath = LuaAPI.lua_tostring(L, 2);
                    string[] _excludeFileExts = (string[])translator.GetObject(L, 3, typeof(string[]));
                    System.IO.SearchOption _searchOption;translator.Get(L, 4, out _searchOption);
                    
                        var gen_ret = Framework.FileUtils.CopyDirectoryAsync( _srcDirPath, _destDirPath, _excludeFileExts, _searchOption );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeleteDirectoryAsync_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _dirPath = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = Framework.FileUtils.DeleteDirectoryAsync( _dirPath );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyFileAsync_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _srcFile = LuaAPI.lua_tostring(L, 1);
                    string _destFile = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = Framework.FileUtils.CopyFileAsync( _srcFile, _destFile );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MergeFolder_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _srcFolder = LuaAPI.lua_tostring(L, 1);
                    string _destFolder = LuaAPI.lua_tostring(L, 2);
                    
                    Framework.FileUtils.MergeFolder( _srcFolder, _destFolder );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyDirectorySync_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _sourceDir = LuaAPI.lua_tostring(L, 1);
                    string _destinationDir = LuaAPI.lua_tostring(L, 2);
                    bool _recursive = LuaAPI.lua_toboolean(L, 3);
                    
                    Framework.FileUtils.CopyDirectorySync( _sourceDir, _destinationDir, _recursive );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ForeachFileInDir_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& translator.Assignable<System.Action<System.IO.FileInfo>>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    System.IO.DirectoryInfo _root = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    System.Action<System.IO.FileInfo> _op = translator.GetDelegate<System.Action<System.IO.FileInfo>>(L, 2);
                    string _partern = LuaAPI.lua_tostring(L, 3);
                    
                    Framework.FileUtils.ForeachFileInDir( _root, _op, _partern );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& translator.Assignable<System.Action<System.IO.FileInfo>>(L, 2)) 
                {
                    System.IO.DirectoryInfo _root = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    System.Action<System.IO.FileInfo> _op = translator.GetDelegate<System.Action<System.IO.FileInfo>>(L, 2);
                    
                    Framework.FileUtils.ForeachFileInDir( _root, _op );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Framework.FileUtils.ForeachFileInDir!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FormatFileSize_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _size = LuaAPI.xlua_tointeger(L, 1);
                    
                        var gen_ret = Framework.FileUtils.FormatFileSize( _size );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFreeDiskSpace_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = Framework.FileUtils.GetFreeDiskSpace(  );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SelectFileInExplorer_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                    Framework.FileUtils.SelectFileInExplorer( _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnMirrorCallback(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Framework.FileUtils.OnMirrorCallback);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnMirrorCallback(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Framework.FileUtils.OnMirrorCallback = translator.GetDelegate<Framework.FileUtils.MirrorCallback>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
