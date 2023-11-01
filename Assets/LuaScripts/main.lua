
if CS.UnityUtils.EnableLuaPanda then
    require("LuaPanda").start("127.0.0.1",8818)
end

print("Application.streamingAssetsPath=", CS.UnityEngine.Application.streamingAssetsPath);
print("Application.persistentDataPath=", CS.UnityEngine.Application.persistentDataPath);

local lua_config = require("luaconfig")
lua_config.setStreamingAssetsPath(CS.UnityEngine.Application.streamingAssetsPath);

local cs_coroutine = (require 'Utils/cs_coroutine')

local luastate_sharedata = require("luastate_sharedata")

local serialize = require "serialize"

local LuaAwaiter = require "Utils.LuaAwaiter"

local TestCoroutineEx = require "UnitTest.TestCoroutineEx"

local UnitTest = require "Framework.UnitTest.UnitTest"

local a = cs_coroutine.start(function()
    print('coroutine a started')

    coroutine.yield(cs_coroutine.start(function()
        print('coroutine b stated inside cotoutine a')
        coroutine.yield(CS.UnityEngine.WaitForSeconds(1))
        print('i am coroutine b')
    end))
    print('coroutine b finish')

    while true do
        coroutine.yield(CS.UnityEngine.WaitForSeconds(1))
        print('i am coroutine a')
    end
end)

cs_coroutine.start(function()
    print('stop coroutine a after 5 seconds')
    coroutine.yield(CS.UnityEngine.WaitForSeconds(5))
    cs_coroutine.stop(a)
    print('coroutine a stoped')
end)

function pr(t,...)
    for k,v in pairs(t) do
        if type(v) == "table" then
            pr(v)
        else
            print(k,v)
        end
    end
    print(...)
end

function HandleNetworkMessages()    
    local buffer, length = luastate_sharedata.queue_dequeue()
    if buffer then
        local msg = serialize.deserialize(buffer)            
    end
end

---@param deltaTime number
---@param unscaledDeltaTime number
function CSharpCallLuaUpdate(deltaTime, unscaledDeltaTime, frameCount)
    HandleNetworkMessages()

    LuaAwaiter.update(deltaTime, unscaledDeltaTime, frameCount)
end

---@param deltaTime number
---@param unscaledDeltaTime number
function CSharpCallLuaLateUpdate(deltaTime, unscaledDeltaTime, frameCount)
    LuaAwaiter.late_update(deltaTime, unscaledDeltaTime, frameCount)
end

function CSharpeCallLuaMain()
    
end
