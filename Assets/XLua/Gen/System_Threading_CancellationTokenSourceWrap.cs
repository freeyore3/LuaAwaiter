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
    public class SystemThreadingCancellationTokenSourceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.Threading.CancellationTokenSource);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 2, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Cancel", _m_Cancel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CancelAfter", _m_CancelAfter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsCancellationRequested", _g_get_IsCancellationRequested);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Token", _g_get_Token);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateLinkedTokenSource", _m_CreateLinkedTokenSource_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new System.Threading.CancellationTokenSource();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<System.TimeSpan>(L, 2))
				{
					System.TimeSpan _delay;translator.Get(L, 2, out _delay);
					
					var gen_ret = new System.Threading.CancellationTokenSource(_delay);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					int _millisecondsDelay = LuaAPI.xlua_tointeger(L, 2);
					
					var gen_ret = new System.Threading.CancellationTokenSource(_millisecondsDelay);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to System.Threading.CancellationTokenSource constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Cancel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Threading.CancellationTokenSource gen_to_be_invoked = (System.Threading.CancellationTokenSource)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Cancel(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _throwOnFirstException = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Cancel( _throwOnFirstException );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Threading.CancellationTokenSource.Cancel!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CancelAfter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Threading.CancellationTokenSource gen_to_be_invoked = (System.Threading.CancellationTokenSource)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _millisecondsDelay = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.CancelAfter( _millisecondsDelay );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.TimeSpan>(L, 2)) 
                {
                    System.TimeSpan _delay;translator.Get(L, 2, out _delay);
                    
                    gen_to_be_invoked.CancelAfter( _delay );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Threading.CancellationTokenSource.CancelAfter!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Threading.CancellationTokenSource gen_to_be_invoked = (System.Threading.CancellationTokenSource)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateLinkedTokenSource_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count >= 0&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 1) || translator.Assignable<System.Threading.CancellationToken>(L, 1))) 
                {
                    System.Threading.CancellationToken[] _tokens = translator.GetParams<System.Threading.CancellationToken>(L, 1);
                    
                        var gen_ret = System.Threading.CancellationTokenSource.CreateLinkedTokenSource( _tokens );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Threading.CancellationToken>(L, 1)&& translator.Assignable<System.Threading.CancellationToken>(L, 2)) 
                {
                    System.Threading.CancellationToken _token1;translator.Get(L, 1, out _token1);
                    System.Threading.CancellationToken _token2;translator.Get(L, 2, out _token2);
                    
                        var gen_ret = System.Threading.CancellationTokenSource.CreateLinkedTokenSource( _token1, _token2 );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Threading.CancellationTokenSource.CreateLinkedTokenSource!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsCancellationRequested(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Threading.CancellationTokenSource gen_to_be_invoked = (System.Threading.CancellationTokenSource)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsCancellationRequested);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Token(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Threading.CancellationTokenSource gen_to_be_invoked = (System.Threading.CancellationTokenSource)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Token);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
