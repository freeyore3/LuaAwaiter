using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using Framework;
using XLua;

namespace UnitTest
{
    public class UnitTestWindow : OdinEditorWindow
    {
        [MenuItem("Tools/Lua Unit Test")]
        private static UnitTestWindow OpenWindow()
        {
            var window = GetWindow<UnitTestWindow>();

            // Nifty little trick to quickly position the window in the middle of the editor.
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);

            return window;
        }

        class TestFixture
        {
            public string Name;
            public List<TestCase> TestCases = new List<TestCase>();
        }
        
        class TestCase
        {
            public string Name;
        }
        
        List<TestFixture> TestFixtures = new List<TestFixture>();

        private LuaTable luaUnitTest;

        void onRunningTestCaseStarted()
        {
            Repaint();   
        }

        void onRunningTestCaseFinished()
        {
            Repaint();
        }

        private bool IsAlreadyRunning => Application.isPlaying && XLuaManager.Instance.IsInited;
        private bool IsNotRunning => ! IsAlreadyRunning;
        
        [InfoBox("请先运行游戏，然后点击Refresh按钮", InfoMessageType.Warning, "IsNotRunning")]
        
        [EnableIf("@IsAlreadyRunning")]
        [Button]
        void Refresh()
        {
            if (! Application.isPlaying)
                return;
            
            if (! XLuaManager.Instance.IsInited)
                return;
            
            this.TestFixtures.Clear();
            
            var luaState = XLuaManager.Instance.LuaState;
            object[] rets = luaState.luaEnv.DoString(
@"require 'UnitTest.testmain'
local UnitTest = require ""Framework.UnitTest.UnitTest""
return UnitTest
");
            luaUnitTest = rets[0] as LuaTable;
            luaUnitTest.Set<string, Action>("onRunningTestCaseStarted", onRunningTestCaseStarted);
            luaUnitTest.Set<string, Action>("onRunningTestCaseFinished", onRunningTestCaseFinished);
            
            var luaTestFixtures = luaUnitTest["TestFixtures"] as LuaTable;
            luaTestFixtures.ForEach((int index, LuaTable luaTestFixture) =>
            {
                var clsname = luaTestFixture.Get<string>("clsname");
                
                TestFixture testFixture = new TestFixture();
                testFixture.Name = clsname;
                testFixture.TestCases = new List<TestCase>();
                this.TestFixtures.Add(testFixture);
                
                var test = luaTestFixture.Get<LuaTable>("Test");
                
                test.ForEach((string testName, LuaTable testCase) =>
                {
                    TestCase testcase = new TestCase();
                    testcase.Name = testName;
                    testFixture.TestCases.Add(testcase);
                });
            });
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            
            Refresh();
        }

        Vector2 scrollPososition;
        
        [OnInspectorGUI]
        void OnInspectorGUI()
        {
            if (luaUnitTest == null)
                return;

            if (IsNotRunning)
                return;
            
            if (this.TestFixtures.Count > 0)
            {
                if (GUILayout.Button("TestAll"))
                {
                    var luaState = XLuaManager.Instance.LuaState;
                    luaState.luaEnv.DoString(
                        @$"require 'UnitTest.testmain'
local UnitTest = require ""Framework.UnitTest.UnitTest""
UnitTest.TestAll()
");
                }
            }

            DrawGUITestFixtures();
        }

        void DrawGUITestFixtures()
        {
            scrollPososition = GUILayout.BeginScrollView(scrollPososition);
            {
                const int TEST_RUNING = 1;
                int status = luaUnitTest.Get<int>("status");
                string running_testFixtureName = luaUnitTest.Get<string>("running_testFixtureName");
                string running_testCaseName = luaUnitTest.Get<string>("running_testCaseName");
                
                foreach (var testFixture in this.TestFixtures)
                {
                    SirenixEditorGUI.BeginBox();
                    {
                        SirenixEditorGUI.BeginBoxHeader();
                        {
                            SirenixEditorGUI.Title(testFixture.Name, "", TextAlignment.Left, true);
                        }
                        SirenixEditorGUI.EndBoxHeader();
                        {
                            foreach (var testCase in testFixture.TestCases)
                            {
                                SirenixEditorGUI.BeginBox();
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    {
                                        GUILayout.Label(testCase.Name);

                                        if (status == TEST_RUNING)
                                        {
                                            if (testFixture.Name == running_testFixtureName &&
                                                testCase.Name == running_testCaseName)
                                            {
                                                GUILayout.Label("Running...");
                                            }
                                        }
                                        else
                                        {
                                            if (GUILayout.Button("Test"))
                                            {
                                                var luaState = XLuaManager.Instance.LuaState;
                                                luaState.luaEnv.DoString(
    @$"require 'UnitTest.testmain'
    local UnitTest = require ""Framework.UnitTest.UnitTest""
    UnitTest.Test('{testFixture.Name}', '{testCase.Name}')
    ");
                                            }
                                        }
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }
                                SirenixEditorGUI.EndBox();
                            }
                        }
                        SirenixEditorGUI.EndBox();
                    }
                }
            }
            GUILayout.EndScrollView();
        }
    }
}