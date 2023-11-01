
local LuaAwaiter = require "Utils.LuaAwaiter"

local UnitTest = require "Framework.UnitTest.UnitTest"

local CSharpAsyncLib = require "Utils.CSharpAsyncLib"

local TestLua54 = TestFixture("TestLua54")

function TestLua54.Test.TestTableClear()
    local t = {}
    setmetatable(t,
            {
                __close = function(t, err)
                    print("t closed")
                    print(t)
                    if err then print(err) end
                end
            })
    table.clear(t) -- does table clear remove __close metamethod?

    local tt <close> = t

    print("TestTableClear finished")
end

function TestLua54.Test.TestTable__close_when_error()
    local t = {}
    setmetatable(t,
            {
                __close = function(t, err)
                    print("t closed")
                    print(t)
                    if err then print(err) end
                end
            })

    local tt <close> = t

    error("TestTable__close error")
    
    print("TestTable__close finished")
end

return TestLua54
