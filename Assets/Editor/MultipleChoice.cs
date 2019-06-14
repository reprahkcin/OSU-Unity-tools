
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultipleChoice : EditorWindow
{
    private String question;
    private String panelName = "mc_panel";

    private String answer_c1;
    private String answer_i1;
    private String answer_i2;
    private String answer_i3;

    private String default_correct = "That is correct!";
    private String default_incorrect = "That is incorrect.";

    private GUIStyle horizontalLine;
    private GUIStyle OSUTools;
    private int width = 1024;
    private int height = 768;
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

        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);

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
            GenerateQuizPanel(width, height);
            GenerateAnswerPanel(width,height,"answer");
        }

    }
    void GenerateQuizPanel(int width, int height)
    {
        GameObject mcQuizPanel = new GameObject();
        mcQuizPanel.AddComponent<RectTransform>();
        mcQuizPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        mcQuizPanel.AddComponent<CanvasRenderer>();
        mcQuizPanel.AddComponent<Image>();
        mcQuizPanel.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        mcQuizPanel.AddComponent<VerticalLayoutGroup>();
        mcQuizPanel.transform.SetParent(Selection.activeTransform.transform, false);
    }
    void GenerateAnswerPanel(int width, int height, string name)
    {
        GameObject aPanel = new GameObject();
        aPanel.AddComponent<RectTransform>();
        aPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(width/2,height/4);
        aPanel.AddComponent<CanvasRenderer>();
        aPanel.name = name;
    }



    void GenerateButton(string label)
    {
        GameObject btn = Resources.Load("orange_btn") as GameObject;
        GameObject b = Instantiate(btn);
        b.GetComponentInChildren<TextMeshProUGUI>().text = label;
        b.name = label.ToLower() + "_btn";
        //b.transform.SetParent(GameObject.Find("working").transform, false);
    }



    void GenerateTextBoxes(string text, int answerNumber)
    {
        TMP_FontAsset font = Resources.Load("Kievit-Medium") as TMP_FontAsset;
        GameObject txt = new GameObject("answer_" + answerNumber);
        txt.AddComponent<RectTransform>();
        txt.GetComponent<RectTransform>().sizeDelta = new Vector2(width/2,height/4);
        txt.AddComponent<CanvasRenderer>();
        txt.AddComponent<TextMeshProUGUI>();
        txt.GetComponent<TextMeshProUGUI>().text = text;
        txt.GetComponent<TextMeshProUGUI>().font = font;
        txt.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
        //txt.transform.SetParent(GameObject.Find("working").transform, false);
    }

    void GenerateButtons()
    {

    }


}
