
if CS.UnityUtils.EnableLuaPanda then
    require("LuaPanda").start("127.0.0.1",8818)
end

local LuaAwaiter = require "Utils.LuaAwaiter"

local TestCoroutineEx = require "UnitTest.TestCoroutineEx"
local TestAsyncCompose = require "UnitTest.TestAsyncCompose"
local TestAsyncCancellation = require "UnitTest.TestAsyncCancellation"
local TestLua54 = require("UnitTest.TestLua54")

local UnitTest = require "Framework.UnitTest.UnitTest"

---@param deltaTime number
---@param unscaledDeltaTime number
function CSharpCallLuaUpdate(deltaTime, unscaledDeltaTime, frameCount)
    LuaAwaiter.update(deltaTime, unscaledDeltaTime, frameCount)
end

---@param deltaTime number
---@param unscaledDeltaTime number
function CSharpCallLuaLateUpdate(deltaTime, unscaledDeltaTime, frameCount)
    LuaAwaiter.late_update(deltaTime, unscaledDeltaTime, frameCount)
end

function CSharpeCallLuaMain()
    --UnitTest.TestAll(TestAsyncCompose, TestCoroutineEx)
end
