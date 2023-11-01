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
    public class FrameworkCoroutineHelperWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Framework.CoroutineHelper);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForEndOfFrame", _m_WaitForEndOfFrame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForNextFrame", _m_WaitForNextFrame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnInit", _m_UnInit);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new Framework.CoroutineHelper();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Framework.CoroutineHelper constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitForEndOfFrame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Framework.CoroutineHelper gen_to_be_invoked = (Framework.CoroutineHelper)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Action>(L, 2)) 
                {
                    System.Action _action = translator.GetDelegate<System.Action>(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.WaitForEndOfFrame( _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Threading.CancellationTokenSource>(L, 2)&& translator.Assignable<System.Action>(L, 3)) 
                {
                    System.Threading.CancellationTokenSource _cancelToken = (System.Threading.CancellationTokenSource)translator.GetObject(L, 2, typeof(System.Threading.CancellationTokenSource));
                    System.Action _action = translator.GetDelegate<System.Action>(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.WaitForEndOfFrame( _cancelToken, _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Framework.CoroutineHelper.WaitForEndOfFrame!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitForNextFrame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Framework.CoroutineHelper gen_to_be_invoked = (Framework.CoroutineHelper)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Action>(L, 2)) 
                {
                    System.Action _action = translator.GetDelegate<System.Action>(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.WaitForNextFrame( _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Threading.CancellationTokenSource>(L, 2)&& translator.Assignable<System.Action>(L, 3)) 
                {
                    System.Threading.CancellationTokenSource _cancelToken = (System.Threading.CancellationTokenSource)translator.GetObject(L, 2, typeof(System.Threading.CancellationTokenSource));
                    System.Action _action = translator.GetDelegate<System.Action>(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.WaitForNextFrame( _cancelToken, _action );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Framework.CoroutineHelper.WaitForNextFrame!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnInit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Framework.CoroutineHelper gen_to_be_invoked = (Framework.CoroutineHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UnInit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
