using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class UnityUtils
{
    public static bool IsUnityEditor()
    {
#if UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }
    public static bool IsAndroid()
    {
#if UNITY_ANDROID
        return true;
#else
        return false;
#endif
    }

    public static bool IsIOS()
    {
#if UNITY_IOS
        return true;
#else
        return false;
#endif
    }

    public static bool IsWindows()
    {
#if UNITY_STANDALONE_WIN
        return true;
#else
        return false;
#endif
    }
    static bool _EnableLuaPanda;
    public static bool EnableLuaPanda
    {
        get
        {
#if UNITY_EDITOR
            return EditorPrefs.GetBool("EnableLuaPanda", true);
#else
            return _EnableLuaPanda;
#endif
        }
        set
        {
#if UNITY_EDITOR
            EditorPrefs.SetBool("EnableLuaPanda", value);
#else
            _EnableLuaPanda = value;
#endif
        }
    }
}
