using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System;
using System.Reflection;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.Networking;
using System.Threading.Tasks;

interface IProgress
{
    void ShowTips(string title, string tips);
    void ShowTipsProgress(string title, string tips, float percent);
    void Clear();
}

public class EditorWindowEx : EditorWindow, IProgress
{
    public void Clear()
    {
        EditorUtility.ClearProgressBar();
    }

    public void ShowTips(string title, string tips)
    {
        EditorUtility.DisplayProgressBar(title, tips, 0);
    }

    public void ShowTipsProgress(string title, string tips, float percent)
    {
        EditorUtility.DisplayProgressBar(title, tips, percent);
    }
}
