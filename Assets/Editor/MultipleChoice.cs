using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoice : EditorWindow
{
    private int width = 1024;
    private int height = 768;

    private MCAnswer a1 = new MCAnswer();
    private MCAnswer a2 = new MCAnswer();
    private MCAnswer a3 = new MCAnswer();
    private MCAnswer a4 = new MCAnswer();

    private string question = "Question";
    private Sprite qSprite;

    private string answer1 = "answer #1";
    private string answer2 = "answer #2";
    private string answer3 = "answer #3";
    private string answer4 = "answer #4";
    private bool isCorrect1 = false;
    private bool isCorrect2 = false;
    private bool isCorrect3 = false;
    private bool isCorrect4 = false;
    private string reason1 = "reason #1";
    private string reason2 = "reason #1";
    private string reason3 = "reason #1";
    private string reason4 = "reason #1";

    private Color32 answerPanelColor = new Color32(37, 37, 37, 50);
    private MCAnswer[] answers;


    // Button and text positions and defaults within panels
    private Vector3 btn_pos = new Vector3(-11, 11, 0);



    private GUIStyle horizontalLine; // Editor window separator bar 



    private GameObject mainPanel;
    private Quaternion q = new Quaternion(0, 0, 0, 0);
    private Vector3 que_btn_pos = new Vector3(11, 11, 0);


    private Color32 questionPanelColor = new Color32(37, 37, 37, 50);

    private Vector3 txt_pos = new Vector3(11, -11, 0);

    //// Default Dimensions of Parent Canvas (and parent canvas)



    [MenuItem("Window/OSU Tools/Multiple Choice Question Builder")]
    public static void ShowWindow()
    {
        GetWindow<MultipleChoice>("Multiple Choice Question Builder");
    }


    private void OnGUI()
    {
        // Set Dimensions of Canvas
        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);

        HorizontalLine(Color.grey);

        GUILayout.Label("Enter the question");
        question = GUILayout.TextArea(question, 200);
        GUILayout.Label("Insert Image");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Source Image");
        qSprite = (Sprite)EditorGUILayout.ObjectField(qSprite, typeof(Sprite), allowSceneObjects: true);
        EditorGUILayout.EndHorizontal();

        HorizontalLine(Color.grey);

        GUILayout.Label("Enter a response");
        answer1 = GUILayout.TextArea(answer1, 50);
        GUILayout.Label("Enter a reason");
        reason1 = GUILayout.TextArea(reason1, 50);
        isCorrect1 = GUILayout.Toggle(isCorrect1, "is Correct");

        HorizontalLine(Color.grey);

        GUILayout.Label("Enter a response");
        answer2 = GUILayout.TextArea(answer2, 50);
        GUILayout.Label("Enter a reason");
        reason2 = GUILayout.TextArea(reason2, 50);
        isCorrect2 = GUILayout.Toggle(isCorrect2, "is Correct");

        HorizontalLine(Color.grey);

        GUILayout.Label("Enter a response");
        answer3 = GUILayout.TextArea(answer3, 50);
        GUILayout.Label("Enter a reason");
        reason3 = GUILayout.TextArea(reason3, 50);
        isCorrect3 = GUILayout.Toggle(isCorrect3, "is Correct");

        HorizontalLine(Color.grey);

        GUILayout.Label("Enter a response");
        answer4 = GUILayout.TextArea(answer4, 50);
        GUILayout.Label("Enter a reason");
        reason4 = GUILayout.TextArea(reason4, 50);
        isCorrect4 = GUILayout.Toggle(isCorrect4, "is Correct");

        if (GUILayout.Button("Generate Quiz"))
        {
            CreateAnswerObjects();
            CreateMainPanel();
            CreateQuestionPanel();
            CreateQuestion();
            CreateAnswerGroupPanel();
            CreateAnswers(answers);
        }
    }

    private void CreateAnswerObjects()
    {
        answers = new MCAnswer[4];
        a1 = new MCAnswer(answer1, isCorrect1, reason1);
        a2 = new MCAnswer(answer2, isCorrect2, reason2);
        a3 = new MCAnswer(answer3, isCorrect3, reason3);
        a4 = new MCAnswer(answer4, isCorrect4, reason4);
        answers[0] = a1;
        answers[1] = a2;
        answers[2] = a3;
        answers[3] = a4;
        
    }


    private void CreateMainPanel()
    {
        mainPanel = new GameObject("MCQuestionSet");
        RectTransform rectTrans = mainPanel.AddComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(width, height);
        rectTrans.anchorMin = new Vector2(0.5f, 0.5f);
        rectTrans.anchorMax = new Vector2(0.5f, 0.5f);
        rectTrans.pivot = new Vector2(0.5f, 0.5f);
        mainPanel.AddComponent<CanvasRenderer>();
        Image img = mainPanel.AddComponent<Image>();
        img.color = new Color(255, 255, 255, 0);
        mainPanel.transform.SetParent(Selection.activeTransform, false);
        //MCLogic scr = mainPanel.AddComponent<MCLogic>();
        //scr.TransferAnswers(answers);
        //scr.bodyFont = Resources.Load("Kievit-Medium") as TMP_FontAsset;
        //scr.btnFont = Resources.Load("Stratum-Bold") as TMP_FontAsset;
        //scr.defaultBtn = Resources.Load("orange_btn") as GameObject;
        //scr.height = height;
        //scr.width = width;
    }

    private void CreateQuestionPanel()
    {
        GameObject mcQuestionPanel = new GameObject("mc_questionPanel");
        RectTransform rect = mcQuestionPanel.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0.5f);
        rect.anchorMax = new Vector2(0, 0.5f);
        rect.pivot = new Vector2(0, 0.5f);
        rect.sizeDelta = new Vector2(width / 2, height);
        mcQuestionPanel.AddComponent<CanvasRenderer>();
        Image img = mcQuestionPanel.AddComponent<Image>();
        img.color = new Color(255, 255, 255, 0);
        VerticalLayoutGroup vert = mcQuestionPanel.AddComponent<VerticalLayoutGroup>();
        vert.childAlignment = TextAnchor.LowerLeft;
        vert.padding = new RectOffset(width / 64, width / 128, height / 48, height / 48);
        vert.spacing = height / 48;
        vert.childControlHeight = true;
        vert.childControlWidth = true;
        mcQuestionPanel.transform.SetParent(mainPanel.transform, false);

        GameObject qImgObj = new GameObject();
        RectTransform qRect = qImgObj.AddComponent<RectTransform>();
        qRect.sizeDelta = new Vector2(width/2,height/2);
        qRect.anchorMax = new Vector2(0.5f,0.5f);
        qRect.anchorMin = new Vector2(0.5f,0.5f);
        qRect.pivot = new Vector2(0.5f,0.5f);
        qImgObj.AddComponent<CanvasRenderer>();
        Image qImg = qImgObj.AddComponent<Image>();
        qImg.sprite = qSprite;

        qImg.transform.SetParent(mcQuestionPanel.transform,false);
    }

    private void CreateQuestion()
    {
        TMP_FontAsset font = Resources.Load("Kievit-Medium") as TMP_FontAsset;
        GameObject question = new GameObject("question");
        question.transform.SetParent(GameObject.Find("mc_questionPanel").transform, false);
        RectTransform rect = question.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        question.AddComponent<CanvasRenderer>();
        TextMeshProUGUI txt = question.AddComponent<TextMeshProUGUI>();
        txt.font = font;
        txt.fontSize = 36;
        txt.alignment = TextAlignmentOptions.Center;
        txt.text = this.question;
    }

    private void CreateAnswerGroupPanel()
    {
        GameObject mcAnswerPanel = new GameObject("mc_answerPanel");
        RectTransform rect = mcAnswerPanel.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width / 2, height);
        rect.anchorMin = new Vector2(1, 0.5f);
        rect.anchorMax = new Vector2(1, 0.5f);
        rect.pivot = new Vector2(1, 0.5f);
        mcAnswerPanel.AddComponent<CanvasRenderer>();
        Image img = mcAnswerPanel.AddComponent<Image>();
        img.color = new Color(255, 255, 255, 0);
        VerticalLayoutGroup vert = mcAnswerPanel.AddComponent<VerticalLayoutGroup>();
        vert.childAlignment = TextAnchor.MiddleCenter;
        vert.padding = new RectOffset(width / 128, width / 64, height / 48, height / 48);
        vert.spacing = height / 48;
        vert.childControlHeight = true;
        vert.childControlWidth = true;
        mcAnswerPanel.transform.SetParent(mainPanel.transform, false);
    }

    private GameObject CreateAnswerPanel(string name)
    {
        GameObject aPanel = new GameObject(name);
        RectTransform rect = aPanel.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width / 2, height / 4);
        
        aPanel.AddComponent<CanvasRenderer>();
        Image img = aPanel.AddComponent<Image>();
        img.color = answerPanelColor;
        return aPanel;
    }

    private GameObject CreateButton(string label, Vector3 pos, int btnNum)
    {
        GameObject btn = Resources.Load("orange_btn") as GameObject;
        GameObject b = Instantiate(btn, pos, q);
        b.GetComponentInChildren<TextMeshProUGUI>().text = label;
        b.name = label.ToLower() + "_" + btnNum;
        return b;
    }


    public GameObject CreateTextBoxes(string text)
    {
        TMP_FontAsset font = Resources.Load("Kievit-Medium") as TMP_FontAsset;
        GameObject txt = new GameObject("answer");
        RectTransform rect = txt.AddComponent<RectTransform>();
        //rect.sizeDelta = new Vector2(width / 2, height / 4-60);
        rect.transform.position = new Vector3(0, 25,0);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        txt.AddComponent<CanvasRenderer>();
        TextMeshProUGUI tmp = txt.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.font = font;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.raycastTarget = false;

        return txt;
    }


    private void CreateAnswers(MCAnswer[] Answers)
    {
        for (int i = 0; i < Answers.Length; i++)
        {
            GameObject panel = CreateAnswerPanel("response_" + i);
            panel.transform.SetParent(GameObject.Find("mc_answerPanel").transform, false);
            GameObject btn = CreateButton("SUBMIT", btn_pos, i);
            btn.transform.SetParent(panel.transform, false);
            RectTransform rect = btn.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(1, 0);
            var txt = CreateTextBoxes(answers[i].value);
            txt.GetComponent<RectTransform>().sizeDelta = new Vector2(width / 2 - 40, height / 5);
            txt.transform.SetParent(panel.transform, false);
        }
    }

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


}