using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGUIEx
{
    public static void DrawLabelAtPosition(float x, float y, string text)
    {
        Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
        Rect rect = new Rect(x, y, size.x, size.y);
        GUI.Label(rect, text);
    }
    public static bool AutoWidthButton(string text)
    {
        Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
        float width = GUI.skin.button.padding.left + size.x + GUI.skin.button.padding.right;
        return GUILayout.Button(text, GUILayout.Width(width));
    }
    public static bool ToggleEx(string text, ref bool value)
    {
        bool b = GUILayout.Toggle(value, text);
        if (b != value)
        {
            value = b;
            return true;
        }
        return false;
    }
    public static Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
