local ArrOffset_IsDead = 1
local ArrOffset_NumOfUnitTargetingMe = 2
local ArrOffset_IsDestroy = 3
local ArrOffset_IsFeed = 4
local ArrOffset_IsHit = 5
local ArrOffset_End = ArrOffset_IsHit

---@param deltaTime number
---@param unscaledDeltaTime number
function OnUpdate(deltaTime, elapsedTime) 
    
end

local ArrOffset_IsDead = 1
local ArrOffset_NumOfUnitTargetingMe = 2
local ArrOffset_IsDestroy = 3
local ArrOffset_IsFeed = 4
local ArrOffset_IsHit = 5
local ArrOffset_End = ArrOffset_IsHit

local LuaTableAccess = require "util.LuaTableAccess"
self._tableaccess = LuaTableAccess.new(len)

obj:SetTableAccess(self._tableaccess, self._tableaccess._ptr, arrIndex)
arrIndex = arrIndex + Unit:LuaCSharpArrLength()

function Unit.LuaCSharpArrLength()
    return ArrOffset_End
end

---@type CS.UnityEngine.Vector2Int
local v
