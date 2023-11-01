using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using XLua;
using UnityEngine.Networking;

namespace Framework
{
    public static class XLuaExporter
    {
        [CSharpCallLua]
        public static List<Type> CSharpCallLua = new List<Type>()
        {
            typeof(Action),
            typeof(Action<int>),
            typeof(Action<float>),
            typeof(Action<float, float>),
            typeof(System.Action<float, float, int>),
            typeof(Action<UnityWebRequest.Result, DownloadHandler>),
            typeof(Action<float, double>),
            typeof(Action<int, LuaTable>),
            typeof(Action<int, float, float, float, float>),
            typeof(Action<int, int, float, float, int, int, float, float>),
            typeof(Action<SceneInstance>),
            typeof(Action<AsyncOperationHandle>),
            typeof(Action<AsyncOperationHandle<SceneInstance>>),
            typeof(Action<AsyncOperationHandle<GameObject>>),
            typeof(Action<AsyncOperationHandle<TextAsset>>),
            typeof(Action<string,AsyncOperationHandle<TextAsset>>),
            typeof(Action<string, string>),
            typeof(Action<bool, bool, bool>),
            typeof(Func<bool>),
            typeof(System.Action<float, float>),
            typeof(System.Action<int, float, float>),
            typeof(System.Action<int, bool>),
        };

        [LuaCallCSharp] public static List<Type> LuaCallCSharp = new List<Type>()
        {
            typeof(System.ValueType),
            typeof(System.Reflection.BindingFlags),
            typeof(UnityEngine.Vector2Int),
            
            typeof(System.Threading.CancellationTokenSource),
            typeof(System.Threading.CancellationToken),

            typeof(PlayerPrefs),
            typeof(UnityEngine.Events.UnityEventBase),
            typeof(UnityEngine.Events.UnityEvent),
            typeof(UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>),
            typeof(UnityEngine.Events.UnityEvent<System.Boolean>),
            typeof(UnityEngine.Events.UnityEvent<System.Int32>),
            typeof(UnityEngine.UI.Button.ButtonClickedEvent),
            typeof(System.Collections.Generic.List<UnityEngine.Camera>),
            typeof(System.Collections.Generic.List<UnityEngine.Collider>),
            typeof(System.Collections.Generic.Dictionary<UnityEngine.GameObject, System.Boolean>),
            typeof(System.Collections.Generic.List<System.Single>),
            typeof(System.Collections.Generic.List<UnityEngine.GameObject>),
            typeof(System.Collections.Generic.List<UnityEngine.Vector2Int>),
        };

        [BlackList] public static List<List<string>> BlackList = new List<List<string>>()
        {
            new List<string>(){ typeof(UnityEngine.Resources).FullName, "InstanceIDsToValidArray", "System.ReadOnlySpan`1[System.Int32]", "System.Span`1[System.Boolean]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "TransformDirections", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "TransformDirections", "System.ReadOnlySpan`1[UnityEngine.Vector3]", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "InverseTransformDirections", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "InverseTransformDirections", "System.ReadOnlySpan`1[UnityEngine.Vector3]", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "TransformVectors", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "TransformVectors", "System.ReadOnlySpan`1[UnityEngine.Vector3]", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "InverseTransformVectors", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "InverseTransformVectors", "System.ReadOnlySpan`1[UnityEngine.Vector3]", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "TransformPoints", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "TransformPoints", "System.ReadOnlySpan`1[UnityEngine.Vector3]", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "InverseTransformPoints", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(UnityEngine.Transform).FullName, "InverseTransformPoints", "System.ReadOnlySpan`1[UnityEngine.Vector3]", "System.Span`1[UnityEngine.Vector3]"},
            new List<string>(){ typeof(System.Threading.CancellationToken).FullName, "Register", "System.Action`1[System.Object]", "System.Object", "System.Boolean", "System.Boolean"},
        };
        
        [XLua.DoNotGen]
        public static Dictionary<Type, List<string>> doNotGenDic = new Dictionary<Type, List<string>>()
        {

        };

    }
}