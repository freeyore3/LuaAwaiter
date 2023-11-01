
local LuaAwaiter = require "Utils.LuaAwaiter"

local UnitTest = require "Framework.UnitTest.UnitTest"

local TestCoroutineEx = TestFixture("TestCoroutineEx")

local CSharpAsyncLib = require "Utils.CSharpAsyncLib"

function TestCoroutineEx.Test.Test1()
    print("------------------------------")
end

TestCoroutineEx.Test.TestRaiseError = async(function()
    await (LuaAwaiter.WaitSeconds(1))
    -- 一些业务逻辑
    error("An error occurred in the business function")
    print("Business function finished")    
end)

TestCoroutineEx.Test.TestWaitFrames = async(function()
    print("Business function started")
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    await(LuaAwaiter.WaitFrames(1))
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    await(LuaAwaiter.WaitFrames(2))
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
end)

TestCoroutineEx.Test.TestWaitSeconds = async(function()
    print("Business function started")
    print("time:" .. tostring(CS.UnityEngine.Time.time))
    await(LuaAwaiter.WaitSeconds(1.5))
    print("time:" .. tostring(CS.UnityEngine.Time.time))
end)

TestCoroutineEx.Test.TestWaitUntil = async(function()
    local t = CS.UnityEngine.Time.time + 2
    print("time:" .. tostring(CS.UnityEngine.Time.time))
    await(LuaAwaiter.WaitUntil(function() return CS.UnityEngine.Time.time > t end))
    print("time:" .. tostring(CS.UnityEngine.Time.time) .. " when time is 2, business function started")
end)

TestCoroutineEx.Test.TestCSharpCallback = async(function()
    local go = await(CSharpAsyncLib.Addressable.InstantiateAsync("Assets/Prefabs/TestPrefab1.prefab"))
    print(go.name)

    for i = 1, 3 do
        local go = await(CSharpAsyncLib.Addressable.InstantiateAsync("Assets/Prefabs/TestPrefab1.prefab"))
        print(i .. ": " .. go.name)
    end
    
    local goChild = await(CSharpAsyncLib.Addressable.InstantiateAsync("Assets/Prefabs/TestPrefab1.prefab", go.transform))
    print("child: " .. goChild.name)
end)

TestCoroutineEx.Test.TestWaitEndOfFrame = async(function()
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    await(LuaAwaiter.WaitEndOfFrame())
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    await(LuaAwaiter.WaitEndOfFrame())
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
end)

TestCoroutineEx.Test.TestWaitNextFrame = async(function()
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    await(LuaAwaiter.WaitNextFrame())
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    await(LuaAwaiter.WaitNextFrame())
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
end)

TestCoroutineEx.Test.TestWaitWhile = async(function()
    local t = CS.UnityEngine.Time.time + 3
    print("time:" .. tostring(CS.UnityEngine.Time.time))
    await(LuaAwaiter.WaitWhile(function() return CS.UnityEngine.Time.time < t end))
    print("time:" .. tostring(CS.UnityEngine.Time.time) .. " when time is up, business function started")
end)

return TestCoroutineEx