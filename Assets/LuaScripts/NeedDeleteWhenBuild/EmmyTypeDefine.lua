---@class NotExportType @表明该类型未导出

---@class NotExportEnum @表明该枚举未导出

---@class CS
CS = {}
---@class System
CS.System = {}
---@class System.Reflection
CS.System.Reflection = {}
---@class UnityEngine
CS.UnityEngine = {}
---@class UnityEngine.Events
CS.UnityEngine.Events = {}
---@class UnityEngine.UI
CS.UnityEngine.UI = {}
---@class CS.System.ValueType : CS.System.Object
CS.System.ValueType = {}
---@param obj CS.System.Object
---@return boolean
function CS.System.ValueType:Equals(obj) end
---@return number
function CS.System.ValueType:GetHashCode() end
---@return string
function CS.System.ValueType:ToString() end

---@class CS.System.Reflection.BindingFlags
---@field Default CS.System.Reflection.BindingFlags
---@field IgnoreCase CS.System.Reflection.BindingFlags
---@field DeclaredOnly CS.System.Reflection.BindingFlags
---@field Instance CS.System.Reflection.BindingFlags
---@field Static CS.System.Reflection.BindingFlags
---@field Public CS.System.Reflection.BindingFlags
---@field NonPublic CS.System.Reflection.BindingFlags
---@field FlattenHierarchy CS.System.Reflection.BindingFlags
---@field InvokeMethod CS.System.Reflection.BindingFlags
---@field CreateInstance CS.System.Reflection.BindingFlags
---@field GetField CS.System.Reflection.BindingFlags
---@field SetField CS.System.Reflection.BindingFlags
---@field GetProperty CS.System.Reflection.BindingFlags
---@field SetProperty CS.System.Reflection.BindingFlags
---@field PutDispProperty CS.System.Reflection.BindingFlags
---@field PutRefDispProperty CS.System.Reflection.BindingFlags
---@field ExactBinding CS.System.Reflection.BindingFlags
---@field SuppressChangeType CS.System.Reflection.BindingFlags
---@field OptionalParamBinding CS.System.Reflection.BindingFlags
---@field IgnoreReturn CS.System.Reflection.BindingFlags
---@field DoNotWrapExceptions CS.System.Reflection.BindingFlags
CS.System.Reflection.BindingFlags = {}

---@class CS.UnityEngine.Vector2Int : CS.System.ValueType
---@field zero CS.UnityEngine.Vector2Int
---@field one CS.UnityEngine.Vector2Int
---@field up CS.UnityEngine.Vector2Int
---@field down CS.UnityEngine.Vector2Int
---@field left CS.UnityEngine.Vector2Int
---@field right CS.UnityEngine.Vector2Int
---@field x number
---@field y number
---@field Item number
---@field magnitude number
---@field sqrMagnitude number
CS.UnityEngine.Vector2Int = {}
---@param x number
---@param y number
---@return CS.UnityEngine.Vector2Int
function CS.UnityEngine.Vector2Int.New(x, y) end
---@param a CS.UnityEngine.Vector2Int
---@param b CS.UnityEngine.Vector2Int
---@return number
function CS.UnityEngine.Vector2Int.Distance(a, b) end
---@param lhs CS.UnityEngine.Vector2Int
---@param rhs CS.UnityEngine.Vector2Int
---@return CS.UnityEngine.Vector2Int
function CS.UnityEngine.Vector2Int.Min(lhs, rhs) end
---@param lhs CS.UnityEngine.Vector2Int
---@param rhs CS.UnityEngine.Vector2Int
---@return CS.UnityEngine.Vector2Int
function CS.UnityEngine.Vector2Int.Max(lhs, rhs) end
---@overload fun(a : CS.UnityEngine.Vector2Int, b : CS.UnityEngine.Vector2Int) : CS.UnityEngine.Vector2Int
---@param scale CS.UnityEngine.Vector2Int
function CS.UnityEngine.Vector2Int:Scale(scale) end
---@param v CS.UnityEngine.Vector2
---@return CS.UnityEngine.Vector2Int
function CS.UnityEngine.Vector2Int.FloorToInt(v) end
---@param v CS.UnityEngine.Vector2
---@return CS.UnityEngine.Vector2Int
function CS.UnityEngine.Vector2Int.CeilToInt(v) end
---@param v CS.UnityEngine.Vector2
---@return CS.UnityEngine.Vector2Int
function CS.UnityEngine.Vector2Int.RoundToInt(v) end
---@param x number
---@param y number
function CS.UnityEngine.Vector2Int:Set(x, y) end
---@param min CS.UnityEngine.Vector2Int
---@param max CS.UnityEngine.Vector2Int
function CS.UnityEngine.Vector2Int:Clamp(min, max) end
---@overload fun(other : CS.System.Object) : boolean
---@param other CS.UnityEngine.Vector2Int
---@return boolean
function CS.UnityEngine.Vector2Int:Equals(other) end
---@return number
function CS.UnityEngine.Vector2Int:GetHashCode() end
---@overload fun() : string
---@overload fun(format : string) : string
---@param format string
---@param formatProvider CS.System.IFormatProvider
---@return string
function CS.UnityEngine.Vector2Int:ToString(format, formatProvider) end

---@class CS.UnityEngine.PlayerPrefs : CS.System.Object
CS.UnityEngine.PlayerPrefs = {}
---@return CS.UnityEngine.PlayerPrefs
function CS.UnityEngine.PlayerPrefs.New() end
---@param key string
---@param value number
function CS.UnityEngine.PlayerPrefs.SetInt(key, value) end
---@overload fun(key : string, defaultValue : number) : number
---@param key string
---@return number
function CS.UnityEngine.PlayerPrefs.GetInt(key) end
---@param key string
---@param value number
function CS.UnityEngine.PlayerPrefs.SetFloat(key, value) end
---@overload fun(key : string, defaultValue : number) : number
---@param key string
---@return number
function CS.UnityEngine.PlayerPrefs.GetFloat(key) end
---@param key string
---@param value string
function CS.UnityEngine.PlayerPrefs.SetString(key, value) end
---@overload fun(key : string, defaultValue : string) : string
---@param key string
---@return string
function CS.UnityEngine.PlayerPrefs.GetString(key) end
---@param key string
---@return boolean
function CS.UnityEngine.PlayerPrefs.HasKey(key) end
---@param key string
function CS.UnityEngine.PlayerPrefs.DeleteKey(key) end
function CS.UnityEngine.PlayerPrefs.DeleteAll() end
function CS.UnityEngine.PlayerPrefs.Save() end

---@class CS.UnityEngine.Events.UnityEventBase : CS.System.Object
CS.UnityEngine.Events.UnityEventBase = {}
---@overload fun(obj : CS.System.Object, functionName : string, argumentTypes : CS.System.Type) : CS.System.Reflection.MethodInfo
---@param objectType CS.System.Type
---@param functionName string
---@param argumentTypes CS.System.Type
---@return CS.System.Reflection.MethodInfo
function CS.UnityEngine.Events.UnityEventBase.GetValidMethodInfo(objectType, functionName, argumentTypes) end
---@return number
function CS.UnityEngine.Events.UnityEventBase:GetPersistentEventCount() end
---@param index number
---@return CS.UnityEngine.Object
function CS.UnityEngine.Events.UnityEventBase:GetPersistentTarget(index) end
---@param index number
---@return string
function CS.UnityEngine.Events.UnityEventBase:GetPersistentMethodName(index) end
---@param index number
---@param state CS.UnityEngine.Events.UnityEventCallState
function CS.UnityEngine.Events.UnityEventBase:SetPersistentListenerState(index, state) end
---@param index number
---@return CS.UnityEngine.Events.UnityEventCallState
function CS.UnityEngine.Events.UnityEventBase:GetPersistentListenerState(index) end
function CS.UnityEngine.Events.UnityEventBase:RemoveAllListeners() end
---@return string
function CS.UnityEngine.Events.UnityEventBase:ToString() end

---@class CS.UnityEngine.Events.UnityEvent : CS.UnityEngine.Events.UnityEventBase
CS.UnityEngine.Events.UnityEvent = {}
---@return CS.UnityEngine.Events.UnityEvent
function CS.UnityEngine.Events.UnityEvent.New() end
---@param call CS.UnityEngine.Events.UnityAction
function CS.UnityEngine.Events.UnityEvent:AddListener(call) end
---@param call CS.UnityEngine.Events.UnityAction
function CS.UnityEngine.Events.UnityEvent:RemoveListener(call) end
function CS.UnityEngine.Events.UnityEvent:Invoke() end

---@class CS.UnityEngine.UI.Button.ButtonClickedEvent : CS.UnityEngine.Events.UnityEvent
CS.UnityEngine.UI.Button.ButtonClickedEvent = {}
---@return CS.UnityEngine.UI.Button.ButtonClickedEvent
function CS.UnityEngine.UI.Button.ButtonClickedEvent.New() end

