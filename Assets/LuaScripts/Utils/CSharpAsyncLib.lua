local CSharpAsyncLib = {}

local LuaAwaiter = require "Utils.LuaAwaiter"

CSharpAsyncLib.Addressable =
{
    ---@return CS.UnityEngine.GameObject
    InstantiateAsync = LuaAwaiter.sync_callback(CS.Logic.AddressableHelper.InstantiateCallback),
}

CSharpAsyncLib.HttpRequest =
{
    SendGetAsync = LuaAwaiter.sync_callback(CS.Framework.HttpRequest.SendGet),    
}

return CSharpAsyncLib