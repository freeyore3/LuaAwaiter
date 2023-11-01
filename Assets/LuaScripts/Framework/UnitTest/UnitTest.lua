
local LuaAwaiter = require "Utils.LuaAwaiter"

local UnitTest = {}

local TablePool = require "Utils.TablePool"

UnitTest.TestFixtures = {}

TEST_IDLE = 0
TEST_RUNING = 1
TEST_FINISHED = 2

UnitTest.status = TEST_IDLE

UnitTest.running_testFixtureName = nil
UnitTest.running_testCaseName = nil

UnitTest.onRunningTestCaseStarted = nil
UnitTest.onRunningTestCaseFinished = nil

UnitTest.TestAll = async(function()
    UnitTest.status = TEST_RUNING

    for testFixtureName, testFixture in ipairs(UnitTest.TestFixtures) do
        local unitTestObj = testFixture

        for testCaseName, v in pairs(unitTestObj.Test) do
            UnitTest._RunTestCase(testFixtureName, testFixture, testCaseName, v)
        end
    end

    UnitTest.status = TEST_FINISHED
end)

UnitTest.Test = async(function(targetTestFixtureName, targetTestCaseName)    
    for testFixtureName, testFixture in ipairs(UnitTest.TestFixtures) do
        if testFixture.clsname == targetTestFixtureName then
            local unitTestObj = testFixture
            
            for testCaseName, v in pairs(unitTestObj.Test) do
                if testCaseName == targetTestCaseName then
                    UnitTest._RunTestCase(testFixtureName, testFixture, testCaseName, v)
                    
                    return
                end
            end
        end
    end
    error("Test case not found: " .. testFixtureName .. "." .. testCaseName)
end)

UnitTest._RunTestCase = function(testFixtureName, testFixture, testCaseName, v)
    UnitTest.status = TEST_RUNING
    UnitTest.running_testFixtureName = testFixtureName
    UnitTest.running_testCaseName = testCaseName

    if UnitTest.onRunningTestCaseStarted then
        UnitTest.onRunningTestCaseStarted()
    end

    print("[Test]=============: " .. testCaseName)
    local result, CoHandle = pcall(v)
    if result then
        if CoHandle and CoHandle.status == LuaAwaiter.WAITING then
            local co = coroutine.running()
            CoHandle.onCompleted = function()
                coroutine.resume(co)
            end
            coroutine.yield()

            assert(CoHandle.status == LuaAwaiter.FINISHED)
        end
    else
        local errorMessage = CoHandle
        printError(errorMessage, "\n", debug.traceback())
    end

    if UnitTest.onRunningTestCaseFinished then
        UnitTest.onRunningTestCaseFinished()
    end
    
    UnitTest.status = TEST_FINISHED
end

function TestFixture(clsname) 
    local testFixture = {
        clsname = clsname,
        Test = {}
    }
    table.insert(UnitTest.TestFixtures, testFixture)
    return testFixture
end

return UnitTest