local TablePool = {}

require "Utils.functions"

TablePool._pool = nil

function TablePool.Get()
    if TablePool._pool == nil then
        TablePool._pool = {}
    end
    if #TablePool._pool > 0 then
        return table.remove(TablePool._pool)
    else
        local newtable = {}
        setmetatable(newtable,
                {
                    __close = function(t)
                        TablePool.Return(t)
                    end
                })
        return newtable
    end
end

function TablePool.Return(t)
    assert(type(t) == "table")
    
    table.clear(t)
    
    if TablePool._pool == nil then
        TablePool._pool = {}
    end
    table.insert(TablePool._pool, t)
end

return TablePool