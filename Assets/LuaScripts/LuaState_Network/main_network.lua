
local luastate_sharedata = require("luastate_sharedata")

local serialize = require "serialize"

function ReceiveNetworkMessagesLoop()
    ---@type number
    local i = 100

    while true do
        coroutine.yield()

        local addressbook = {
            name = "Alice",
            id = i,
            phone = {
                { number = "1301234567" },
                { number = "87654321", type = "WORK" },
            }
        }

        local a = serialize.pack(addressbook)

        local seri, length = serialize.serialize(a)

        luastate_sharedata.queue_enqueue(seri, length)

        i = i + 1
    end
end

---@type thread
local co = coroutine.create(function()
    local status, msg = pcall(ReceiveNetworkMessagesLoop)
    if not status then
        printError(msg)
    end
end)
coroutine.resume(co)

---@param deltaTime number
---@param elapsedTime number
function OnUpdate(deltaTime, elapsedTime) 
    if coroutine.status(co) == "suspended" then
        coroutine.resume(co)
    end
end

---@type CS.UnityEngine.GameObject
local go
