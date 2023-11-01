
local LuaAwaiter = require "Utils.LuaAwaiter"

local UnitTest = require "Framework.UnitTest.UnitTest"

local CSharpAsyncLib = require "Utils.CSharpAsyncLib"

local TestAsyncCancellation = TestFixture("TestAsyncCancellation")

TestAsyncCancellation.waitSecond_2_async = async(function(cancelToken)
    print("[time-2.1] " .. tostring(CS.UnityEngine.Time.time))
    await(LuaAwaiter.WaitSeconds(2, cancelToken))
    print("[time-2.2] " .. tostring(CS.UnityEngine.Time.time))
end)

TestAsyncCancellation.Test.TestCancelWaitSeconds = async(function()
    print("TestCancelWaitSeconds started")
    local cancelToken = LuaAwaiter.createCancellationToken()
    
    TestAsyncCancellation.waitSecond_2_async(cancelToken)

    await(LuaAwaiter.WaitSeconds(1))
    print("Cancel waitSecond_2_async")
    cancelToken:Cancel()
    
    print("TestCancelWaitSeconds finished")
end)

TestAsyncCancellation.waitframes_60_async = async(function(cancelToken)
    local t = {}
    setmetatable(t,
            {
                __close = function(t, err)
                    print("waitframes_60_async closed")
                end
            })
    
    local tt <close> = t
    
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    await(LuaAwaiter.WaitFrames(60, cancelToken))
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
end)

TestAsyncCancellation.Test.TestCancelWaitFrames = async(function()
    print("TestCancelWaitFrames started " .. "frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    local cancelToken = LuaAwaiter.createCancellationToken()

    TestAsyncCancellation.waitframes_60_async(cancelToken)

    await(LuaAwaiter.WaitFrames(10))
    print("Cancel waitframes_60_async " .. "frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    cancelToken:Cancel()

    print("TestCancelWaitFrames finished")
end)

TestAsyncCancellation.waituntil_2_async = async(function(cancelToken)
    local t = CS.UnityEngine.Time.time
    
    print("time:" .. tostring(CS.UnityEngine.Time.time))
    await(LuaAwaiter.WaitUntil(function() return CS.UnityEngine.Time.time > t + 2 end, cancelToken))
    print("time:" .. tostring(CS.UnityEngine.Time.time))
end)

TestAsyncCancellation.Test.TestCancelWaitUntil = async(function()
    print("TestCancelWaitUntil started")
    
    local t = CS.UnityEngine.Time.time

    local cancelToken = LuaAwaiter.createCancellationToken()
    TestAsyncCancellation.waituntil_2_async(cancelToken)
    
    await(LuaAwaiter.WaitSeconds(1))
    print("Cancel waituntil_2_async")
    cancelToken:Cancel()
    
    print("TestCancelWaitUntil finished")
end)

TestAsyncCancellation.waitwhile_2_async = async(function(cancelToken)
    local t = CS.UnityEngine.Time.time

    print("time:" .. tostring(CS.UnityEngine.Time.time))
    await(LuaAwaiter.WaitWhile(function() return CS.UnityEngine.Time.time < t + 2 end, cancelToken))
    print("time:" .. tostring(CS.UnityEngine.Time.time))
end)

TestAsyncCancellation.Test.TestCancelWaitWhile = async(function()
    print("TestCancelWaitWhile started")

    local t = CS.UnityEngine.Time.time

    local cancelToken = LuaAwaiter.createCancellationToken()
    TestAsyncCancellation.waitwhile_2_async(cancelToken)

    await(LuaAwaiter.WaitSeconds(1))
    print("Cancel waitwhile_2_async")
    cancelToken:Cancel()

    print("TestCancelWaitWhile finished")
end)

TestAsyncCancellation.wait_endofframe_async = async(function(cancelToken)
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    await(LuaAwaiter.WaitEndOfFrame(cancelToken))
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
end)

TestAsyncCancellation.Test.TestCancelWaitEndOfFrame = async(function()
    print("TestCancelWaitEndOfFrame started")

    local cancelToken = LuaAwaiter.createCancellationToken()
    TestAsyncCancellation.wait_endofframe_async(cancelToken)

    print("Cancel wait_endofframe_async")
    cancelToken:Cancel()

    print("TestCancelWaitEndOfFrame finished" .. " frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
end)

TestAsyncCancellation.wait_nextframe_async = async(function(cancelToken)
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    await(LuaAwaiter.WaitNextFrame(cancelToken))
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
end)

TestAsyncCancellation.Test.TestCancelWaitNextFrame = async(function()
    print("TestCancelWaitNextFrame started")

    local cancelToken = LuaAwaiter.createCancellationToken()
    TestAsyncCancellation.wait_nextframe_async(cancelToken)

    print("Cancel wait_nextframe_async")
    cancelToken:Cancel()

    print("TestCancelWaitNextFrame finished" .. "frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
end)

TestAsyncCancellation.wait_loop_async = async(function(cancelToken)
    for i = 1, 100 do
        print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
        await(LuaAwaiter.WaitNextFrame())

        if cancelToken:IsCancellationRequested() then
            return
        end
    end
    print("wait_loop_async finished")
end)

TestAsyncCancellation.Test.TestCancelWaitLoop = async(function()
    print("TestCancelWaitLoop started")

    local cancelToken = LuaAwaiter.createCancellationToken()
    TestAsyncCancellation.wait_loop_async(cancelToken)

    await(LuaAwaiter.WaitFrames(5))
    print("Cancel wait_loop_async")
    cancelToken:Cancel()

    print("TestCancelWaitLoop finished")
end)

TestAsyncCancellation.wait_loop_async_raise = async(function(cancelToken)
    for i = 1, 100 do
        print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
        await(LuaAwaiter.WaitNextFrame())

        cancelToken:ThrowIfCancellationRequested()
    end
    print("wait_loop_async_raise finished")
end)

TestAsyncCancellation.Test.TestCancelWaitLoopRaise = async(function()
    print("TestCancelWaitLoopRaise started")

    local cancelToken = LuaAwaiter.createCancellationToken()
    TestAsyncCancellation.wait_loop_async_raise(cancelToken)

    await(LuaAwaiter.WaitFrames(5))
    print("Cancel wait_loop_async_raise")
    cancelToken:Cancel()

    print("TestCancelWaitLoopRaise finished")
end)

TestAsyncCancellation.wait_http_request_async = async(function(cancelToken)
    print("wait_http_request_async started")

    local result, downloadHandler

    local t = {}
    setmetatable(t, {
        __close = function(t, err)
            print("wait_http_request_async closed")
            if cancelToken:IsCancellationRequested() then
                print("wait_http_request_async closed by cancel")
                print("result:" .. tostring(result))
            end
        end
    })
    local tt <close> = t

    print("CSharpAsyncLib.HttpRequest.SendGetAsync")
    result, downloadHandler = await(CSharpAsyncLib.HttpRequest.SendGetAsync("www.google.com", cancelToken))
    print("result:" .. tostring(result))
    print("downloadHandler.text:" .. tostring(downloadHandler.text))
    
    print("wait_http_request_async finished")
end)

TestAsyncCancellation.Test.TestCancelHttpRequest = async(function()
    print("TestCancelHttpRequest started")

   local result, downloadHandler = await(CSharpAsyncLib.HttpRequest.SendGetAsync("www.baidu.com"))
    print("result:" .. tostring(result))
    print("downloadHandler.text:" .. tostring(downloadHandler.text))
    
    local cancelToken = LuaAwaiter.createCancellationToken()
    
    TestAsyncCancellation.wait_http_request_async(cancelToken)

    print("time:" .. tostring(CS.UnityEngine.Time.time))
    await(LuaAwaiter.WaitSeconds(0.1))
    print("time:" .. tostring(CS.UnityEngine.Time.time))
    print("Cancel wait_http_request_async")
    cancelToken:Cancel()

    print("TestCancelHttpRequest finished")
end)

TestAsyncCancellation.wait_instantiate_async = async(function(cancelToken)
    print("wait_instantiate_async started")

    local go = await(CSharpAsyncLib.Addressable.InstantiateAsync("Assets/Prefabs/TestPrefab1.prefab", cancelToken))
    print(go.name)

    print("wait_instantiate_async finished")
end)

TestAsyncCancellation.Test.TestCancelInstantiate = async(function()
    print("TestCancelInstantiate started")

    local cancelToken = LuaAwaiter.createCancellationToken()

    TestAsyncCancellation.wait_instantiate_async(cancelToken)

    print("Cancel wait_instantiate_async")
    cancelToken:Cancel()
    
    print("TestCancelInstantiate finished")
end)

TestAsyncCancellation.wait_whenall_async = async(function(cancelToken)
    print("wait_whenall_async started")

    cancelToken:Register(
            function()
                print("wait_whenall_async cancel callback")
            end
    )
    
    local t = CS.UnityEngine.Time.time

    print("time:" .. tostring(CS.UnityEngine.Time.time))
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
    await(LuaAwaiter.WhenAll(
            CSharpAsyncLib.Addressable.InstantiateAsync("Assets/Prefabs/TestPrefab1.prefab", cancelToken),
            LuaAwaiter.WaitSeconds(100, cancelToken),
            LuaAwaiter.WaitFrames(1000, cancelToken),
            LuaAwaiter.WaitUntil(function() return CS.UnityEngine.Time.time > t + 9999 end, cancelToken),
            LuaAwaiter.WaitEndOfFrame(cancelToken),
            LuaAwaiter.WaitWhile(function() return CS.UnityEngine.Time.time < t + 9999 end, cancelToken)
    ))

    cancelToken:ThrowIfCancellationRequested()
    
    print("time:" .. tostring(CS.UnityEngine.Time.time))

    print("wait_whenall_async finished")
end)

TestAsyncCancellation.Test.TestCancelWhenAll = async(function()
    print("TestCancelWhenAll started")

    local cancelToken = LuaAwaiter.createCancellationToken()

    TestAsyncCancellation.wait_whenall_async(cancelToken)

    await(LuaAwaiter.WaitFrames(30))
    
    print("Cancel wait_whenall_async")
    cancelToken:Cancel()
    print("time:" .. tostring(CS.UnityEngine.Time.time))
    print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))

    print("TestCancelWhenAll finished")
end)

return TestAsyncCancellation
