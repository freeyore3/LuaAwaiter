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
    public class FrameworkHttpRequestWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Framework.HttpRequest);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "SendGet", _m_SendGet_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new Framework.HttpRequest();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Framework.HttpRequest constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendGet_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<UnityEngine.Networking.UnityWebRequest.Result, UnityEngine.Networking.DownloadHandler>>(L, 2)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    System.Action<UnityEngine.Networking.UnityWebRequest.Result, UnityEngine.Networking.DownloadHandler> _callback = translator.GetDelegate<System.Action<UnityEngine.Networking.UnityWebRequest.Result, UnityEngine.Networking.DownloadHandler>>(L, 2);
                    
                    Framework.HttpRequest.SendGet( _uri, _callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Threading.CancellationTokenSource>(L, 2)&& translator.Assignable<System.Action<UnityEngine.Networking.UnityWebRequest.Result, UnityEngine.Networking.DownloadHandler>>(L, 3)) 
                {
                    string _uri = LuaAPI.lua_tostring(L, 1);
                    System.Threading.CancellationTokenSource _cancelToken = (System.Threading.CancellationTokenSource)translator.GetObject(L, 2, typeof(System.Threading.CancellationTokenSource));
                    System.Action<UnityEngine.Networking.UnityWebRequest.Result, UnityEngine.Networking.DownloadHandler> _callback = translator.GetDelegate<System.Action<UnityEngine.Networking.UnityWebRequest.Result, UnityEngine.Networking.DownloadHandler>>(L, 3);
                    
                    Framework.HttpRequest.SendGet( _uri, _cancelToken, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Framework.HttpRequest.SendGet!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
