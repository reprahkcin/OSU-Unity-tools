
using System;
using UnityEditor;
using UnityEngine;

public class MultipleChoice : EditorWindow
{
    private String question;

    private String answer_1;
    private String answer_2;
    private String answer_3;
    private String answer_4;

    private String default_correct = "That is correct!";
    private String default_incorrect = "That is incorrect.";







    private GUIStyle horizontalLine;
    private GUIStyle OSUTools;

    private void HorizontalLine(Color color)
    {
        horizontalLine = new GUIStyle();
        horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
        horizontalLine.margin = new RectOffset(0, 0, 10, 10);
        horizontalLine.fixedHeight = 4;
        Color c = GUI.color;
        GUI.color = color;
        GUILayout.Box(GUIContent.none, horizontalLine);
        GUI.color = c;
    }

    [MenuItem("Window/OSU Tools/Multiple Choice Question Builder")]
    public static void ShowWindow()
    {
        GetWindow<MultipleChoice>("Multiple Choice Question Builder");
    }
    

    private void OnGUI()
    {
        GUILayout.Label("1. Enter the Questions");
    }


}
