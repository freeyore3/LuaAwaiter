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
    public class SystemCollectionsGenericDictionary_2_UnityEngineGameObjectSystemBoolean_Wrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>);
			Utils.BeginObjectRegister(type, L, translator, 0, 11, 4, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Add", _m_Add);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clear", _m_Clear);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ContainsKey", _m_ContainsKey);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ContainsValue", _m_ContainsValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEnumerator", _m_GetEnumerator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetObjectData", _m_GetObjectData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnDeserialization", _m_OnDeserialization);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Remove", _m_Remove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TryGetValue", _m_TryGetValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EnsureCapacity", _m_EnsureCapacity);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TrimExcess", _m_TrimExcess);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Comparer", _g_get_Comparer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Count", _g_get_Count);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Keys", _g_get_Keys);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Values", _g_get_Values);
            
			
			
			Utils.EndObjectRegister(type, L, translator, __CSIndexer, __NewIndexer,
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
					
					var gen_ret = new System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					int _capacity = LuaAPI.xlua_tointeger(L, 2);
					
					var gen_ret = new System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>(_capacity);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<System.Collections.Generic.IEqualityComparer<UnityEngine.GameObject>>(L, 2))
				{
					System.Collections.Generic.IEqualityComparer<UnityEngine.GameObject> _comparer = (System.Collections.Generic.IEqualityComparer<UnityEngine.GameObject>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IEqualityComparer<UnityEngine.GameObject>));
					
					var gen_ret = new System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>(_comparer);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && translator.Assignable<System.Collections.Generic.IEqualityComparer<UnityEngine.GameObject>>(L, 3))
				{
					int _capacity = LuaAPI.xlua_tointeger(L, 2);
					System.Collections.Generic.IEqualityComparer<UnityEngine.GameObject> _comparer = (System.Collections.Generic.IEqualityComparer<UnityEngine.GameObject>)translator.GetObject(L, 3, typeof(System.Collections.Generic.IEqualityComparer<UnityEngine.GameObject>));
					
					var gen_ret = new System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>(_capacity, _comparer);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> constructor!");
            
        }
        
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int __CSIndexer(RealStatePtr L)
        {
			try {
			    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				
				if (translator.Assignable<System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>>(L, 1) && translator.Assignable<UnityEngine.GameObject>(L, 2))
				{
					
					System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
					UnityEngine.GameObject index = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
					LuaAPI.lua_pushboolean(L, true);
					LuaAPI.lua_pushboolean(L, gen_to_be_invoked[index]);
					return 2;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
			
            LuaAPI.lua_pushboolean(L, false);
			return 1;
        }
		
        
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int __NewIndexer(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			try {
				
				if (translator.Assignable<System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>>(L, 1) && translator.Assignable<UnityEngine.GameObject>(L, 2) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3))
				{
					
					System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
					UnityEngine.GameObject key = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
					gen_to_be_invoked[key] = LuaAPI.lua_toboolean(L, 3);
					LuaAPI.lua_pushboolean(L, true);
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
			
			LuaAPI.lua_pushboolean(L, false);
            return 1;
        }
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Add(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _key = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    bool _value = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.Add( _key, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clear(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Clear(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ContainsKey(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _key = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                        var gen_ret = gen_to_be_invoked.ContainsKey( _key );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ContainsValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _value = LuaAPI.lua_toboolean(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.ContainsValue( _value );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEnumerator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetEnumerator(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetObjectData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Runtime.Serialization.SerializationInfo _info = (System.Runtime.Serialization.SerializationInfo)translator.GetObject(L, 2, typeof(System.Runtime.Serialization.SerializationInfo));
                    System.Runtime.Serialization.StreamingContext _context;translator.Get(L, 3, out _context);
                    
                    gen_to_be_invoked.GetObjectData( _info, _context );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnDeserialization(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _sender = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.OnDeserialization( _sender );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Remove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _key = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                        var gen_ret = gen_to_be_invoked.Remove( _key );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TryGetValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _key = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    bool _value;
                    
                        var gen_ret = gen_to_be_invoked.TryGetValue( _key, out _value );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    LuaAPI.lua_pushboolean(L, _value);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EnsureCapacity(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _capacity = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.EnsureCapacity( _capacity );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TrimExcess(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.TrimExcess(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _capacity = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.TrimExcess( _capacity );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>.TrimExcess!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Comparer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.Comparer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Count);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Keys(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Keys);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Values(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool> gen_to_be_invoked = (System.Collections.Generic.Dictionary<UnityEngine.GameObject, bool>)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Values);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
