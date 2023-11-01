using System;
using System.Collections;
using Framework;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Compilation;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using System.Diagnostics;

namespace UnitTest
{
    public class UnitTestMain : MonoBehaviour
    {
        public void Awake()
        {
            XLuaManager.Instance.InitLua("UnitTest.testmain");
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);
            
            XLuaManager.Instance.LuaGlobal.Call("CSharpeCallLuaMain");
            
#if UNITY_EDITOR
            // call assembly Editor's UnitTestWindow.OpenWindow
            // find assembly by name Editor
            // find type by name UnitTestWindow
            // find method by name OpenWindow
            System.Reflection.Assembly assembly = null;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (a.GetName().Name == "Editor")
                {
                    assembly = a;
                    break;
                }
            }
            Type type = null;
            foreach (var t in assembly.GetTypes())
            {
                UnityEngine.Debug.Log(t.Name);
                if (t.FullName == "UnitTest.UnitTestWindow")
                {
                    type = t;
                    break;
                }
            }
            var method = type.GetMethod("OpenWindow", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            object UnitTestWindow = method.Invoke(null, null);
            
            // call UnitTestWindow.Refresh
            method = type.GetMethod("Refresh", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            method.Invoke(UnitTestWindow, null);
#endif
        }

        private void Update()
        {
            XLuaManager.Instance.Update(Time.deltaTime, Time.time);
        }
    }
}