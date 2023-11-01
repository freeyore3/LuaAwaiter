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
    public class LogicAddressableHelperWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Logic.AddressableHelper);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 8, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "InstantiateCallback", _m_InstantiateCallback_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "InstantiateAsync", _m_InstantiateAsync_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReleaseInstance", _m_ReleaseInstance_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadSceneCallback", _m_LoadSceneCallback_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadSceneAsync", _m_LoadSceneAsync_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnloadSceneCallback", _m_UnloadSceneCallback_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnloadSceneAsync", _m_UnloadSceneAsync_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new Logic.AddressableHelper();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.AddressableHelper constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InstantiateCallback_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<UnityEngine.GameObject>>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Action<UnityEngine.GameObject> _callback = translator.GetDelegate<System.Action<UnityEngine.GameObject>>(L, 2);
                    
                        var gen_ret = Logic.AddressableHelper.InstantiateCallback( _path, _callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Transform>(L, 2)&& translator.Assignable<System.Action<UnityEngine.GameObject>>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.Transform _parent = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    System.Action<UnityEngine.GameObject> _callback = translator.GetDelegate<System.Action<UnityEngine.GameObject>>(L, 3);
                    
                        var gen_ret = Logic.AddressableHelper.InstantiateCallback( _path, _parent, _callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Threading.CancellationTokenSource>(L, 2)&& translator.Assignable<System.Action<UnityEngine.GameObject>>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    System.Threading.CancellationTokenSource _cancelToken = (System.Threading.CancellationTokenSource)translator.GetObject(L, 2, typeof(System.Threading.CancellationTokenSource));
                    System.Action<UnityEngine.GameObject> _callback = translator.GetDelegate<System.Action<UnityEngine.GameObject>>(L, 3);
                    
                        var gen_ret = Logic.AddressableHelper.InstantiateCallback( _path, _cancelToken, _callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Transform>(L, 2)&& translator.Assignable<System.Threading.CancellationTokenSource>(L, 3)&& translator.Assignable<System.Action<UnityEngine.GameObject>>(L, 4)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.Transform _parent = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    System.Threading.CancellationTokenSource _cancelToken = (System.Threading.CancellationTokenSource)translator.GetObject(L, 3, typeof(System.Threading.CancellationTokenSource));
                    System.Action<UnityEngine.GameObject> _callback = translator.GetDelegate<System.Action<UnityEngine.GameObject>>(L, 4);
                    
                        var gen_ret = Logic.AddressableHelper.InstantiateCallback( _path, _parent, _cancelToken, _callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.AddressableHelper.InstantiateCallback!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InstantiateAsync_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Transform>(L, 2)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.Transform _parent = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                        var gen_ret = Logic.AddressableHelper.InstantiateAsync( _path, _parent );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = Logic.AddressableHelper.InstantiateAsync( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Transform>(L, 2)&& translator.Assignable<System.Threading.CancellationTokenSource>(L, 3)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.Transform _parent = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    System.Threading.CancellationTokenSource _cancelToken = (System.Threading.CancellationTokenSource)translator.GetObject(L, 3, typeof(System.Threading.CancellationTokenSource));
                    
                        var gen_ret = Logic.AddressableHelper.InstantiateAsync( _path, _parent, _cancelToken );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Logic.AddressableHelper.InstantiateAsync!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReleaseInstance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    
                    Logic.AddressableHelper.ReleaseInstance( _go );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadSceneCallback_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.SceneManagement.LoadSceneMode _sceneMode;translator.Get(L, 2, out _sceneMode);
                    System.Action<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> _callback = translator.GetDelegate<System.Action<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>>(L, 3);
                    
                    Logic.AddressableHelper.LoadSceneCallback( _path, _sceneMode, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadSceneAsync_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.SceneManagement.LoadSceneMode _sceneMode;translator.Get(L, 2, out _sceneMode);
                    
                        var gen_ret = Logic.AddressableHelper.LoadSceneAsync( _path, _sceneMode );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnloadSceneCallback_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.ResourceManagement.ResourceProviders.SceneInstance _sceneInstance;translator.Get(L, 1, out _sceneInstance);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 2);
                    
                    Logic.AddressableHelper.UnloadSceneCallback( _sceneInstance, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnloadSceneAsync_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.ResourceManagement.ResourceProviders.SceneInstance _sceneInstance;translator.Get(L, 1, out _sceneInstance);
                    
                        var gen_ret = Logic.AddressableHelper.UnloadSceneAsync( _sceneInstance );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
