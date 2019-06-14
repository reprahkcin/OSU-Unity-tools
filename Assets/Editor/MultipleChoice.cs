
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoice : EditorWindow
{
    private String question;

    private String answer_c1;
    private String answer_i1;
    private String answer_i2;
    private String answer_i3;

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

        GUILayout.Label("1. Enter the question");
        question = GUILayout.TextArea(question, 200);
        HorizontalLine(Color.grey);
        GUILayout.Label("2. Enter the correct response");
        answer_c1 = GUILayout.TextArea(answer_c1, 50);
        GUILayout.Label("3. Enter an incorrect response");
        answer_i1 = GUILayout.TextArea(answer_i1, 50);
        GUILayout.Label("4. Enter an incorrect response");
        answer_i2 = GUILayout.TextArea(answer_i2, 50);
        GUILayout.Label("5. Enter an incorrect response");
        answer_i3 = GUILayout.TextArea(answer_i3, 50);

        if (GUILayout.Button("Generate Quiz"))
        {
            GenerateQuiz();
        }

    }

    void GenerateQuiz()
    {
        GameObject mcQuizPanel = new GameObject();
        mcQuizPanel.AddComponent<RectTransform>();
        mcQuizPanel.AddComponent<CanvasRenderer>();
        mcQuizPanel.AddComponent<Image>();

    }

    void GenerateTextBoxes()
    {

    }

    void GenerateButtons()
    {

    }


}
