
local LuaAwaiter = require "Utils.LuaAwaiter"

local UnitTest = require "Framework.UnitTest.UnitTest"

local CSharpAsyncLib = require "Utils.CSharpAsyncLib"

local TestAsyncCompose = TestFixture("TestAsyncCompose")

TestAsyncCompose.waitSecond_1 = async_Task(function()
    print("[time-1.1] " .. tostring(CS.UnityEngine.Time.time))
    await(LuaAwaiter.WaitSeconds(1))
    print("[time-1.2] " .. tostring(CS.UnityEngine.Time.time))
end)

TestAsyncCompose.waitSecond_2 = async_Task(function()
    print("[time-2.1] " .. tostring(CS.UnityEngine.Time.time))
    await(LuaAwaiter.WaitSeconds(2))
    print("[time-2.2] " .. tostring(CS.UnityEngine.Time.time))
end)

TestAsyncCompose.DoSomeAsync = async(function()
    print("DoSomeAsync started")
    await(LuaAwaiter.WaitSeconds(1))
    print("[time-1.2] " .. tostring(CS.UnityEngine.Time.time))
    await(LuaAwaiter.WaitSeconds(2))
    print("DoSomeAsync finished")
end)

TestAsyncCompose.calculateAsync = async_Task(function(a, b)
    print("calculate started " .. tostring(a) .. " + " .. tostring(b) .. " = ?")
    await(TestAsyncCompose.waitSecond_1())
    print("calculate finished")
    return a + b
end)

TestAsyncCompose.calculateMultiRetAsync = async_Task(function(a, b)
    print("calculate started")
    await(TestAsyncCompose.waitSecond_1())
    print("calculate finished")
    return (a + b) * 2, (a + b) * 4, (a + b) * 8
end)

TestAsyncCompose.Test.TestCallAsync = async(function()
    print("CallAsync started")
    await(TestAsyncCompose.waitSecond_1())
    print("CallAsync finished")
end)

TestAsyncCompose.Test.TestCallAsyncResult = async(function()
    print("CallAsync started")
    local n = await(TestAsyncCompose.calculateAsync(1, 1))
    print("n = " .. tostring(n))
    print("CallAsync finished")
end)

TestAsyncCompose.Test.TestCallAsyncMultiResult = async(function()
    print("CallAsync started")
    local n1, n2, n3 = await(TestAsyncCompose.calculateMultiRetAsync(1, 1))
    print("n1 = " .. tostring(n1) .. ", n2 = " .. tostring(n2) .. ", n3 = " .. tostring(n3))
    print("CallAsync finished")
end)

TestAsyncCompose.Test.TestCallAsyncWithoutAwait = async(function()
    print("TestCallAsyncWithoutAwait started")
    TestAsyncCompose.DoSomeAsync()
    
    await(LuaAwaiter.WaitSeconds(1))
    print("TestCallAsyncWithoutAwait finished")
end)

TestAsyncCompose.Test.TestWhenAll = async(function()
    print("WhenAll started")
    local t = CS.UnityEngine.Time.time

    local task1 = CSharpAsyncLib.Addressable.InstantiateAsync("Assets/Prefabs/TestPrefab1.prefab")
    await(LuaAwaiter.WhenAll(
        TestAsyncCompose.waitSecond_1(), 
        TestAsyncCompose.waitSecond_2(),
        LuaAwaiter.WaitFrames(1),
        LuaAwaiter.WaitSeconds(1.5),
        LuaAwaiter.WaitUntil(function() return CS.UnityEngine.Time.time > t + 2 end),
        task1,
        LuaAwaiter.WaitEndOfFrame(),
        LuaAwaiter.WaitWhile(function() return CS.UnityEngine.Time.time < t + 3 end)
    ))
    
    local go = task1.rets and table.unpack(task1.rets)
    print(go)
    
    print("WhenAll finished")
end)

TestAsyncCompose.Test.TestWhenAny = async(function()
    print("WhenAny started")
    await(LuaAwaiter.WhenAny(
        TestAsyncCompose.waitSecond_1(), 
        TestAsyncCompose.waitSecond_2()
    ))
    print("WhenAny finished")
end)

return TestAsyncCompose