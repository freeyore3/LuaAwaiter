local LuaAwaiter = {}

local TablePool = require "Utils.TablePool"

local CoHandle_pool = {}

local waitframes_list = {}
local waitframes_pool = {}

local CoHandle_WaitFrames = 1
local CoHandle_WaitSeconds = 2
local CoHandle_WaitUntil = 3
local CoHandle_WhenAll = 4
local CoHandle_WhenAny = 5
local CoHandle_CSharpAsyncCallback = 6
local CoHandle_WaitWhile = 7
local CoHandle_Async = 8
local CoHandle_AsyncTask = 9

local WAITING = 100
local FINISHED = 101
local CANCLED = 102

local CoHandle_list = {}
local CoHandle_RemoveList = {}

LuaAwaiter.WAITING = WAITING
LuaAwaiter.FINISHED = FINISHED

require "Utils.functions"

local CANCEL_ERROR <const> = "LuaAwaiter.CANCEL_ERROR"

local function RaiseCancelError()
    error(CANCEL_ERROR)
end

local function IsCancelError(errorMessage)
    if string.find(errorMessage, CANCEL_ERROR) then
        return true
    else
        return false
    end
end

---@param frames number
function LuaAwaiter.WaitFrames(frames, cancelToken)
    local co = coroutine.running() or error ('this function must be run in coroutine')

    local CoHandle = table.remove(CoHandle_pool)
    if not CoHandle then
        CoHandle = { }
    end
    CoHandle.co = co
    CoHandle.type = CoHandle_WaitFrames
    CoHandle.status = WAITING
    CoHandle.frames = frames
    CoHandle.start_frame_count = CS.UnityEngine.Time.frameCount
    CoHandle.cancelToken = cancelToken
    table.insert(CoHandle_list, CoHandle)
    return CoHandle
end

---@param seconds number
function LuaAwaiter.WaitSeconds(seconds, cancelToken)
    local co = coroutine.running() or error ('this function must be run in coroutine')

    local CoHandle = table.remove(CoHandle_pool)
    if not CoHandle then
        CoHandle = { }
    end
    CoHandle.co = co
    CoHandle.type = CoHandle_WaitSeconds
    CoHandle.status = WAITING
    CoHandle.seconds = seconds
    CoHandle.elapsed_seconds = 0
    CoHandle.cancelToken = cancelToken
    table.insert(CoHandle_list, CoHandle)
    
    return CoHandle
end

function LuaAwaiter.WaitWhile(predicate, cancelToken)
    assert(type(predicate) == 'function')
    local co = coroutine.running() or error ('this function must be run in coroutine')
    
    local CoHandle = table.remove(CoHandle_pool)
    if not CoHandle then
        CoHandle = { }
    end
    CoHandle.co = co
    CoHandle.type = CoHandle_WaitWhile
    CoHandle.status = WAITING
    CoHandle.predicate = predicate
    CoHandle.cancelToken = cancelToken
    table.insert(CoHandle_list, CoHandle)
    
    return CoHandle
end

---@param predicate fun() : boolean
function LuaAwaiter.WaitUntil(predicate, cancelToken)
    assert(type(predicate) == 'function')
    local co = coroutine.running() or error ('this function must be run in coroutine')
    
    local CoHandle = table.remove(CoHandle_pool)
    if not CoHandle then
        CoHandle = { }
    end
    CoHandle.co = co
    CoHandle.type = CoHandle_WaitUntil
    CoHandle.status = WAITING
    CoHandle.predicate = predicate
    CoHandle.cancelToken = cancelToken
    table.insert(CoHandle_list, CoHandle)
    
    return CoHandle
end

function LuaAwaiter.WhenAll(...) 
    local co = coroutine.running() or error ('this function must be run in coroutine')

    local childCoHandlelist = {}
    
    local arg_count = select("#", ...) 
    if arg_count == 0 then
        error("WhenAll: arg_count == 0")
    end
    for i = 1, arg_count do
        local CoHandleWaiting = select(i, ...)
        if LuaAwaiter.is_CoHandle_valid(CoHandleWaiting) then
            if CoHandleWaiting.type == CoHandle_Async then
                error("WhenAll: arg " .. i .. " is invalid, because it is a async function, you should use async_Task function instead")
            end
            table.insert(childCoHandlelist, CoHandleWaiting)
        else
            error("WhenAll: arg " .. i .. " is invalid")
        end
    end
    
    local CoHandle = table.remove(CoHandle_pool)
    if not CoHandle then
        CoHandle = { }
    end
    CoHandle.co = co
    CoHandle.type = CoHandle_WhenAll
    CoHandle.status = WAITING
    for i = 1, arg_count do
        local CoHandleWaiting = childCoHandlelist[i]
        assert(CoHandleWaiting.parentCoHandle == nil)
        CoHandleWaiting.parentCoHandle = CoHandle
        
        if CoHandleWaiting.type == CoHandle_AsyncTask then
            CoHandleWaiting.StartTask()
        end
    end
    CoHandle.childCoHandlelist = childCoHandlelist
    table.insert(CoHandle_list, CoHandle)
    
    return CoHandle
end

function LuaAwaiter.WhenAny(...) 
    local co = coroutine.running() or error ('this function must be run in coroutine')

    local childCoHandlelist = {}
    
    local arg_count = select("#", ...) 
    if arg_count == 0 then
        error("WhenAll: arg_count == 0")
    end
    for i = 1, arg_count do
        local CoHandleWaiting = select(i, ...)
        if LuaAwaiter.is_CoHandle_valid(CoHandleWaiting) then
            if CoHandleWaiting.type == CoHandle_Async then
                error("WhenAll: arg " .. i .. " is invalid, because it is a async function, you should use async_Task function instead")
            end
            table.insert(childCoHandlelist, CoHandleWaiting)
        else
            error("WhenAny: arg " .. i .. " is invalid")
        end
    end
    
    local CoHandle = table.remove(CoHandle_pool)
    if not CoHandle then
        CoHandle = { }
    end
    CoHandle.co = co
    CoHandle.type = CoHandle_WhenAny
    CoHandle.status = WAITING
    for i = 1, arg_count do
        local CoHandleWaiting = childCoHandlelist[i]
        assert(CoHandleWaiting.parentCoHandle == nil)
        CoHandleWaiting.parentCoHandle = CoHandle
        
        if CoHandleWaiting.type == CoHandle_AsyncTask then
            CoHandleWaiting.StartTask()
        end
    end
    CoHandle.childCoHandlelist = childCoHandlelist
    table.insert(CoHandle_list, CoHandle)
    
    return CoHandle
end

local function CoHandle_IsFinised(CoHandle)
    if CoHandle.type == CoHandle_WhenAll then
        local all_finished = true
        for i = 1, #CoHandle.childCoHandlelist do
            local CoHandleWaiting = CoHandle.childCoHandlelist[i]
            if not CoHandle_IsFinised(CoHandleWaiting) then
                all_finished = false
                break
            end
        end
        if all_finished then
            return true
        end
    elseif CoHandle.type == CoHandle_WhenAny then
        local any_finished = false
        for i = 1, #CoHandle.childCoHandlelist do
            local CoHandleWaiting = CoHandle.childCoHandlelist[i]
            if CoHandle_IsFinised(CoHandleWaiting) then
                any_finished = true
                break
            end
        end
        if any_finished then
            return true
        end
    else
        if CoHandle.status == FINISHED then
            return true
        end
    end
    
    return false
end

local function CoHandle_OnFinished(CoHandle, ...)
    if CoHandle.parentCoHandle then
        if (CoHandle_IsFinised(CoHandle.parentCoHandle)) then
            CoHandle_OnFinished(CoHandle.parentCoHandle, ...)
        end
    else
        if CoHandle.type == CoHandle_Async then
            -- do nothing
            if CoHandle.onCompleted then
                CoHandle:onCompleted()
            end
        elseif CoHandle.type == CoHandle_AsyncTask then
            local status = coroutine.status(CoHandle.calling_co)
            if status ~= 'suspended' then
                assert(status == 'suspended', debug.traceback() .. "\n" .. "status == " .. tostring(status))
            end
            coroutine.resume(CoHandle.calling_co, ...)
        else
            local status = coroutine.status(CoHandle.co)
            if status ~= 'suspended' then
                assert(status == 'suspended', debug.traceback() .. "\n" .. "status == " .. tostring(status))
            end
            coroutine.resume(CoHandle.co, ...)
        end
    end
end

local function CoHandle_IsAllParentFinised(CoHandle)
    if CoHandle.status ~= FINISHED then
        return false
    end
    if CoHandle.parentCoHandle then
        if not CoHandle_IsAllParentFinised(CoHandle.parentCoHandle) then
            return false
        end
    end
    return true
end

local function CoHandle_IsAllChildFinised(CoHandle)
    if CoHandle.status ~= FINISHED then
        return false
    end
    if CoHandle.childCoHandlelist then
        for i = 1, #CoHandle.childCoHandlelist do
            local CoHandleWaiting = CoHandle.childCoHandlelist[i]
            if not CoHandle_IsAllChildFinised(CoHandleWaiting) then
                return false
            end
        end
    end
    return true
end

local function CoHandle_IsAllFinised(CoHandle)
    if CoHandle.status == FINISHED then
        if CoHandle.parentCoHandle then
            if not CoHandle_IsAllParentFinised(CoHandle.parentCoHandle) then
                return false
            end
        end

        if CoHandle.childCoHandlelist then
            for i = 1, #CoHandle.childCoHandlelist do
                local CoHandleWaiting = CoHandle.childCoHandlelist[i]
                if not CoHandle_IsAllChildFinised(CoHandleWaiting) then
                    return false
                end
            end
        end
        
        return true
    else
        return false
    end
end

local CancelToken = class("CancelToken")
CancelToken.isCanceled = false
CancelToken._cancellationTokenSource = nil
CancelToken._registerEvents = nil
CancelToken.Cancel = function(self)
    self.isCanceled = true
    if self._cancellationTokenSource then
        self._cancellationTokenSource:Cancel()
    end
    if self._registerEvents then
        for i = 1, #self._registerEvents do
            local func = self._registerEvents[i]
            local result, errorMessage = pcall(func)
            if not result then
                printError(errorMessage, "\n", debug.traceback())
            end
        end
    end
end
CancelToken.IsCancellationRequested = function(self)
    return self.isCanceled
end
CancelToken.ThrowIfCancellationRequested = function(self)
    if self.isCanceled then
        RaiseCancelError()
    end
end
CancelToken.GetCSharpCancelToken = function(self)
    if not self._cancellationTokenSource then
        self._cancellationTokenSource = CS.System.Threading.CancellationTokenSource()
    end
    return self._cancellationTokenSource
end
CancelToken.Register = function(self, func) 
    assert(type(func) == 'function')
    if not self._registerEvents then
        self._registerEvents = {}
    end
    table.insert(self._registerEvents, func)
end

function LuaAwaiter.createCancellationToken()
    local cancelToken = CancelToken.new()
    return cancelToken
end

---@param deltaTime number
---@param unscaledDeltaTime number
---@param frameCount number
function LuaAwaiter.update(deltaTime, unscaledDeltaTime, frameCount)
    local curr_frameCount = CS.UnityEngine.Time.frameCount

    for i = 1, #CoHandle_list do
        local CoHandle = CoHandle_list[i]

        if CoHandle.status == FINISHED then
            goto continue
        end
        
        if CoHandle.type == CoHandle_WaitFrames then
            if CoHandle.cancelToken and CoHandle.cancelToken.isCanceled then
                assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
                CoHandle.status = FINISHED

                CoHandle.isCanceled = true

                CoHandle_OnFinished(CoHandle)
            else
                if curr_frameCount >= CoHandle.start_frame_count + CoHandle.frames then
                    assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
                    CoHandle.status = FINISHED

                    CoHandle_OnFinished(CoHandle)
                end
                
            end
        elseif CoHandle.type == CoHandle_WaitSeconds then
            if CoHandle.cancelToken and CoHandle.cancelToken.isCanceled then
                assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
                CoHandle.status = FINISHED 
                
                CoHandle.isCanceled = true

                CoHandle_OnFinished(CoHandle)
            else
                CoHandle.elapsed_seconds = CoHandle.elapsed_seconds + deltaTime
                if CoHandle.elapsed_seconds >= CoHandle.seconds then
                    assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
                    CoHandle.status = FINISHED

                    CoHandle_OnFinished(CoHandle)
                end
            end
            
        elseif CoHandle.type == CoHandle_WaitUntil then
            if CoHandle.cancelToken and CoHandle.cancelToken.isCanceled then
                assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
                CoHandle.status = FINISHED

                CoHandle.isCanceled = true

                CoHandle_OnFinished(CoHandle)
            else
                local result, isOn = pcall(CoHandle.predicate)
                if result then
                    if isOn then
                        assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
                        CoHandle.status = FINISHED

                        CoHandle_OnFinished(CoHandle)
                    end
                else
                    local errorMessage = isOn
                    if not IsCancelError(errorMessage) then
                        printError(errorMessage, "\n", debug.traceback())
                    end
                end
            end
        elseif CoHandle.type == CoHandle_WaitWhile then
            if CoHandle.cancelToken and CoHandle.cancelToken.isCanceled then
                assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
                CoHandle.status = FINISHED

                CoHandle.isCanceled = true

                CoHandle_OnFinished(CoHandle)
            else
                local result, isOn = pcall(CoHandle.predicate)
                if result then
                    if not isOn then
                        assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
                        CoHandle.status = FINISHED

                        CoHandle_OnFinished(CoHandle)
                    end
                else
                    local errorMessage = isOn
                    if not IsCancelError(errorMessage) then
                        printError(errorMessage, "\n", debug.traceback())
                    end
                end
            end
        end
        
        ::continue::
    end

    for i = #CoHandle_list, 1, -1 do
        local CoHandle = CoHandle_list[i]
        if CoHandle_IsAllFinised(CoHandle) then
            table.insert(CoHandle_RemoveList, i)
        end
    end

    local needRemoveCount = #CoHandle_RemoveList
    if needRemoveCount > 0 then
        for i = 1, needRemoveCount do
            local idx = CoHandle_RemoveList[i]

            local CoHandle = CoHandle_list[idx]
            table.remove(CoHandle_list, idx)

            table.clear(CoHandle)

            table.insert(CoHandle_pool, CoHandle)
        end

        table.clear(CoHandle_RemoveList)
    end
end

---@param deltaTime number
---@param unscaledDeltaTime number
---@param frameCount number
function LuaAwaiter.late_update(deltaTime, unscaledDeltaTime, frameCount)
    
end

function LuaAwaiter.is_async_block_valid(async_block)
    if not async_block then
        return false
    end
    if not type(async_block) == 'table' then
        return false
    end
    if type(async_block.func) ~= 'function' then
        return false
    end
    return true
end

function async(func) 
    assert(type(func) == 'function')
    
    local mt = {}
    mt.__call = function(self, ...)
        return LuaAwaiter.call_async(self, ...)
    end
    
    local async_block = {}
    setmetatable(async_block, mt)
    async_block.func = func
    
    return async_block
end

function async_Task(func)
    assert(type(func) == 'function')

    local mt = {}
    mt.__call = function(self, ...)
        return LuaAwaiter.create_async_task(self, ...)
    end

    local async_block = {}
    setmetatable(async_block, mt)
    async_block.func = func
    
    async_block.isTask = true

    return async_block
end

function await(CoHandle)
    if CoHandle.type == CoHandle_CSharpAsyncCallback then
        if CoHandle.status == WAITING then
            CoHandle.rets = { coroutine.yield() }
        end

        if CoHandle.isCanceled then
            RaiseCancelError()
        end
        
        return table.unpack(CoHandle.rets)
    elseif CoHandle.type == CoHandle_Async then
        error("could not await a async function, you should use async_Task function instead")
    elseif CoHandle.type == CoHandle_AsyncTask then
        if CoHandle.status == WAITING then
            CoHandle.StartTask()

            CoHandle.rets = { coroutine.yield() }
        end
        return table.unpack(CoHandle.rets)
    else
        coroutine.yield()

        if CoHandle.isCanceled then
            RaiseCancelError()
        end
    end
end

function LuaAwaiter.is_CoHandle_valid(CoHandle) 
    if not CoHandle then
        return false
    end
    if not type(CoHandle) == 'table' then
        return false
    end
    if type(CoHandle.co) ~= 'thread' then
        return false
    end
    if type(CoHandle.type) ~= 'number' then
        return false
    end
    if type(CoHandle.status) ~= 'number' then
        return false
    end
    return true
end

---@param businessFunction fun()
function LuaAwaiter.call_async(async_block, ...)
    assert(LuaAwaiter.is_async_block_valid(async_block), 'async_block is invalid')

    if async_block.isTask then
        error('async_Task can not be called by call_async')
    end

    local function func(CoHandle, async_block, ...)
        local result, errorMessage = pcall(async_block.func, ...)
        if not result then
            if not IsCancelError(errorMessage) then
                printError(errorMessage, "\n", debug.traceback())
            end
        end

        if CoHandle.cancelToken and CoHandle.cancelToken.isCanceled then
            assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
            CoHandle.status = FINISHED
    
            CoHandle.isCanceled = true
    
            CoHandle_OnFinished(CoHandle)
        else
            assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
            CoHandle.status = FINISHED
    
            CoHandle_OnFinished(CoHandle)
        end
    end

    local co = coroutine.create(func)

    local CoHandle = table.remove(CoHandle_pool)
    if not CoHandle then
        CoHandle = { }
    end
    CoHandle.co = co
    CoHandle.type = CoHandle_Async
    CoHandle.status = WAITING
    table.insert(CoHandle_list, CoHandle)

    coroutine.resume(co, CoHandle, async_block, ...)
    
    return CoHandle
end

function LuaAwaiter.create_async_task(async_block, ...)
    assert(LuaAwaiter.is_async_block_valid(async_block), 'async_block is invalid')
    
    if not async_block.isTask then
        error('async_Task can not be called by call_async')
    end
    
    local function func(CoHandle, async_block, ...)
        local rets = table.pack(pcall(async_block.func, ...))
        local result = rets[1]
        if result then
            table.remove(rets, 1)
        else
            local errorMessage = rets[2]
            if not IsCancelError(errorMessage) then
                printError(errorMessage, "\n", debug.traceback())
            end
        end

        assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
        CoHandle.status = FINISHED

        CoHandle_OnFinished(CoHandle, table.unpack(rets))
    end
    
    local co = coroutine.create(func)
    
    local CoHandle = table.remove(CoHandle_pool)
    if not CoHandle then
        CoHandle = { }
    end
    CoHandle.co = co
    CoHandle.type = CoHandle_AsyncTask
    CoHandle.status = WAITING
    table.insert(CoHandle_list, CoHandle)
    
    CoHandle.calling_args = {...}
    CoHandle.calling_co = coroutine.running()
    
    CoHandle.StartTask = function()
        coroutine.resume(CoHandle.co, CoHandle, async_block, table.unpack(CoHandle.calling_args))
    end
    
    return CoHandle
end

---@param async_func fun(..., fun(...))
---@param callback_pos number
function LuaAwaiter.sync_callback(async_func, self, callback_pos)
    return function(...)

        local rets = nil
        local waiting = false
        local CoHandle = nil

        local function cb_func(...)
            if waiting then
                if CoHandle.cancelToken and CoHandle.cancelToken.isCanceled then
                    assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
                    CoHandle.status = FINISHED

                    CoHandle.isCanceled = true
                    
                    CoHandle_OnFinished(CoHandle, ...)
                else
                    assert(CoHandle.status == WAITING, "CoHandle.status == " .. tostring(CoHandle.status))
                    CoHandle.status = FINISHED

                    CoHandle.rets = {...}
                    
                    CoHandle_OnFinished(CoHandle, ...)
                end
            else
                rets = {...}
            end
        end

        local argsCount = select("#", ...)

        local cancelToken
        if argsCount > 0 then
            local arg = select(argsCount, ...)
            if arg and type(arg) == "table" and arg.__cname == "CancelToken" then
                cancelToken = arg
            end
        end

        local params <close> = TablePool.Get()
        
        if callback_pos then
            table.packinto(params, ...)
            if cancelToken then
                params[#params] = cancelToken:GetCSharpCancelToken()
            end
            table.insert(params, callback_pos or (#params + 1), cb_func)
            if self then
                async_func(self, table.unpack(params))
            else
                async_func(table.unpack(params))
            end
        else
            if argsCount > 0 then
                table.packinto(params, ...)
                if cancelToken then
                    params[#params] = cancelToken:GetCSharpCancelToken()
                end
                table.insert(params, cb_func)
                if self then
                    async_func(self, table.unpack(params))
                else
                    async_func(table.unpack(params))
                end
            else
                if self then
                    async_func(self, cb_func)
                else
                    async_func(cb_func)
                end
            end
        end

        if rets == nil then
            waiting = true
            --rets = {coroutine.yield()}
        end

        local co = coroutine.running() or error ('this function must be run in coroutine')
        
        CoHandle = table.remove(CoHandle_pool)
        if not CoHandle then
            CoHandle = {}
        end
        CoHandle.co = co
        CoHandle.type = CoHandle_CSharpAsyncCallback
        if waiting then
            CoHandle.status = WAITING
        else
            CoHandle.status = FINISHED    
        end
        CoHandle.cancelToken = cancelToken
        CoHandle.rets = rets
        table.insert(CoHandle_list, CoHandle)

        --return table.unpack(rets)
        return CoHandle
    end
end

LuaAwaiter.WaitEndOfFrame = LuaAwaiter.sync_callback(CS.Framework.CoroutineHelper.Instance.WaitForEndOfFrame, CS.Framework.CoroutineHelper.Instance)
LuaAwaiter.WaitNextFrame = LuaAwaiter.sync_callback(CS.Framework.CoroutineHelper.Instance.WaitForNextFrame, CS.Framework.CoroutineHelper.Instance)

return LuaAwaiter