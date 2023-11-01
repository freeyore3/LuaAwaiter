# LuaAwaiter

**LuaAwaiter** 是一个基于 XLua 框架的工具，可以扩展到 tolua、ULua、Cocos2d-x 和 Unity3D (UE) 中。它提供了对协程（Coroutine）的封装，允许您以类似 C# 的 async/await 语法进行异步编程，并与 C# 协程无缝集成。此工具还支持 Cancellation，以更好地管理异步任务。

## 基本用法
首先，您可以定义一个 async 函数，如下所示：
```Lua
local LuaAwaiter = require "Utils.LuaAwaiter"

local Test = {}

Test.Test1 = async(function()
    print("Test1 began")
    await(LuaAwaiter.WaitSeconds(1))
    -- 其他操作...
    print("Test1 end")
end)
```
在上述代码中，我们使用 LuaAwaiter.WaitSeconds(1) 来等待 1 秒。这段代码展示了如何使用 await 来等待异步操作完成。
调用 async 函数只需简单地按常规的函数调用即可，支持参数和返回值：
```Lua
Test.Test1()
```

## 支持的等待操作
LuaAwaiter 支持多种常见等待操作，包括等待秒数、帧数、EndOfFrame、下一帧以及满足特定条件等。以下是一些示例：
```Lua
Test.Test2 = async(function()
    await(LuaAwaiter.WaitSeconds(1))
    await(LuaAwaiter.WaitFrames(1))
    await(LuaAwaiter.WaitEndOfFrame())
    await(LuaAwaiter.WaitNextFrame())
    
    local t = CS.UnityEngine.Time.time + 2
    await(LuaAwaiter.WaitUntil(function() return CS.UnityEngine.Time.time > t end))
    await(LuaAwaiter.WaitWhile(function() return CS.UnityEngine.Time.time < t end))

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
    
    await(LuaAwaiter.WhenAny(
        TestAsyncCompose.waitSecond_1(), 
        TestAsyncCompose.waitSecond_2()
    ))    
end)
```
这些等待操作允许您更精确地管理异步任务的执行。

## 声明 C# Async 函数

LuaAwaiter 允许您声明 C# 的异步函数以便在 Lua 中使用。您可以使用以下方式声明：
```Lua
LuaAwaiter.WaitEndOfFrame = LuaAwaiter.sync_callback(CS.Framework.CoroutineHelper.Instance.WaitForEndOfFrame, CS.Framework.CoroutineHelper.Instance)
```
这个声明将 C# 的异步函数与 Lua 的 async/await 机制连接起来。在这里，WaitForEndOfFrame 函数需要一个回调函数作为参数，用于在操作完成时执行。

## C# 函数实现和 Cancellation

C# 函数实现时，最后一个参数通常需要是回调函数，可以带参数和返回值。以下是一个示例：
```Lua
public async UniTaskVoid WaitForEndOfFrame(Action action)
{
    await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
    action?.Invoke();
}
```
这个示例中的 WaitForEndOfFrame 函数在 Unity 的 LastPostLateUpdate 时机上执行操作。

如果需要支持 Cancellation，函数的倒数第二个参数需要是 CancellationTokenSource。以下是一个示例：
```Lua
public async UniTaskVoid WaitForEndOfFrame(CancellationTokenSource cancelToken, Action action)
{
    await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate, cancelToken.Token);
    action?.Invoke();
}
```
这个示例中，cancelToken 参数用于取消操作。

## 定义 async Task 函数

您还可以定义返回多个值的 async Task 函数，以支持更复杂的异步操作。以下是示例代码：
```Lua
Test.waitSecond_1 = async_Task(function()
    print("[time-1.1] " .. tostring(CS.UnityEngine.Time.time))
    await(LuaAwaiter.WaitSeconds(1))
    print("[time-1.2] " .. tostring(CS.UnityEngine.Time.time))
end)

Test.calculateAsync = async_Task(function(a, b)
    print("calculate started " .. tostring(a) .. " + " .. tostring(b) .. " = ?")
    await(Test.waitSecond_1())
    print("calculate finished")
    return a + b
end)

Test.TestCallAsyncResult = async(function()
    print("CallAsync started")
    local n = await(Test.calculateAsync(1, 1))
    print("n = " .. tostring(n))
    print("CallAsync finished")
end)
```
这些示例展示了如何定义和使用返回多个值的 async Task 函数。

## Async Cancellation

最后，LuaAwaiter 支持 Cancellation 操作。您可以定义异步任务，如下所示：
```Lua
Test.wait_loop_async = async(function(cancelToken)
    for i = 1, 100 do
        print("frame:" ..  tostring(CS.UnityEngine.Time.frameCount))
        await(LuaAwaiter.WaitNextFrame())

        if cancelToken:IsCancellationRequested() then
            return
        end
    end
    local t = CS.UnityEngine.Time.time
    await(LuaAwaiter.WhenAll(
        CSharpAsyncLib.Addressable.InstantiateAsync("Assets/Prefabs/TestPrefab1.prefab", cancelToken),
        LuaAwaiter.WaitSeconds(100, cancelToken),
        LuaAwaiter.WaitFrames(1000, cancelToken),
        LuaAwaiter.WaitUntil(function() return CS.UnityEngine.Time.time > t + 9999 end, cancelToken),
        LuaAwaiter.WaitEndOfFrame(cancelToken),
        LuaAwaiter.WaitWhile(function() return CS.UnityEngine.Time.time < t + 9999 end, cancelToken)
    ))
    print("wait_loop_async finished")
end)

Test.TestCancelWaitLoop = async(function()
    print("TestCancelWaitLoop started")

    local cancelToken = LuaAwaiter.createCancellationToken()
    Test.wait_loop_async(cancelToken)

    await(LuaAwaiter.WaitFrames(5))
    print("Cancel wait_loop_async")
    cancelToken:Cancel()

    print("TestCancelWaitLoop finished")
end)
```
这些示例展示了如何在异步任务中使用 Cancellation 操作。