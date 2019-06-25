using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultipleChoice : EditorWindow
{
    private Vector2 scrollPos = new Vector2(); // Used to help with scrolling on editor window 
    private GUIStyle horizontalLine; // Editor window separator bar 

    // Default Dimensions of Parent Canvas (and parent canvas)
    private int width = 1024;
    private int height = 768;

    private GameObject mainPanel;
    
    // Button and text positions and defaults within panels
    private Vector3 btn_pos = new Vector3(-11, 11, 0);
    private Vector3 que_btn_pos = new Vector3(11, 11, 0);
    private Vector3 txt_pos = new Vector3(11, -11, 0);
    private string btn_txt = "SUBMIT";
    private string back_btn_txt = "REVIEW";
    private Color32 answerPanelColor = new Color32(37, 37, 37, 50);
    private Color32 questionPanelColor = new Color32(37, 37, 37, 50);
    private Quaternion q;

    // Raw Data for question
    public static string defaultReason = "Incorrect";

    private string question;

    private MCAnswer a1;
    private string answer1;
    private string reason1 = defaultReason;
    private bool isCorrect1;

    private MCAnswer a2;
    private string answer2;
    private string reason2 = defaultReason;
    private bool isCorrect2;

    private MCAnswer a3;
    private string answer3;
    private string reason3 = defaultReason;
    private bool isCorrect3;

    private MCAnswer a4;
    private string answer4;
    private string reason4 = defaultReason;
    private bool isCorrect4;

    private MCAnswer[] answers;



    [MenuItem("Window/OSU Tools/Multiple Choice Question Builder")]
    public static void ShowWindow()
    {
        GetWindow<MultipleChoice>("Multiple Choice Question Builder");
    }
    

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, EditorGUIUtility.currentViewWidth, position.height));
        GUILayout.BeginVertical();

        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);

        // Set Dimensions of Canvas
        width = EditorGUILayout.IntField("Width", width); 
        height = EditorGUILayout.IntField("Height", height);

        HorizontalLine(Color.grey);

        GUILayout.Label("Enter the question");
        question = GUILayout.TextArea(question, 200);

        HorizontalLine(Color.grey);

        GUILayout.Label("Enter a response");
        answer1 = GUILayout.TextArea(answer1, 50);
        GUILayout.Label("Enter a reason");
        reason1 = GUILayout.TextArea(reason1, 50);
        isCorrect1 = GUILayout.Toggle(isCorrect1,"is Correct");

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

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.EndArea();

    }

    void CreateAnswerObjects()
    {
        answers = new MCAnswer[4];
        var a1 = new MCAnswer(answer1, isCorrect1, reason1);
        var a2 = new MCAnswer(answer2, isCorrect2, reason2);
        var a3 = new MCAnswer(answer3, isCorrect3, reason3);
        var a4 = new MCAnswer(answer4, isCorrect4, reason4);
        answers[0] = a1;
        answers[1] = a2;
        answers[2] = a3;
        answers[3] = a4;
    }



    void CreateMainPanel()
    {
        mainPanel = new GameObject("MCQuestionSet");
        RectTransform rectTrans = mainPanel.AddComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(width, height);
        rectTrans.anchorMin = new Vector2(0.5f,0.5f);
        rectTrans.anchorMax = new Vector2(0.5f,0.5f);
        rectTrans.pivot = new Vector2(0.5f,0.5f);
        mainPanel.AddComponent<CanvasRenderer>();
        Image img = mainPanel.AddComponent<Image>();
        img.color = new Color(255,255,255,0);
        mainPanel.transform.SetParent(Selection.activeTransform, false);
        MCLogic scr = mainPanel.AddComponent<MCLogic>();
        scr.MCBank = answers;
        scr.question = question;
        scr.bodyFont = Resources.Load("Kievit-Medium") as TMP_FontAsset;
        scr.btnFont = Resources.Load("Stratum-Bold") as TMP_FontAsset;
        scr.defaultBtn = Resources.Load("orange_btn") as GameObject;
        scr.height = height;
        scr.width = width;
    }

    void CreateQuestionPanel()
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
    }

    void CreateQuestion()
    {
        TMP_FontAsset font = Resources.Load("Kievit-Medium") as TMP_FontAsset;
        GameObject question = new GameObject("question");
        question.transform.SetParent(GameObject.Find("mc_questionPanel").transform, false);
        RectTransform rect = question.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f,0.5f);
        rect.anchorMax = new Vector2(0.5f,0.5f);
        rect.pivot = new Vector2(0.5f,0.5f);
        question.AddComponent<CanvasRenderer>();
        TextMeshProUGUI txt = question.AddComponent<TextMeshProUGUI>();
        txt.font = font;
        txt.fontSize = 36;
        txt.alignment = TextAlignmentOptions.Center;
        txt.text = this.question;


    }

    void CreateAnswerGroupPanel()
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
        vert.childAlignment = TextAnchor.UpperRight;
        vert.padding = new RectOffset(width / 128, width / 64, height / 48, height / 48);
        vert.spacing = height / 48;
        vert.childControlHeight = true;
        vert.childControlWidth = true;
        mcAnswerPanel.transform.SetParent(mainPanel.transform,false);
    }

    GameObject CreateAnswerPanel(string name)
    {
        GameObject aPanel = new GameObject(name);
        RectTransform rect = aPanel.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width / 2, height / 4);
        aPanel.AddComponent<CanvasRenderer>();
        Image img = aPanel.AddComponent<Image>();
        img.color = answerPanelColor;
        return aPanel;
    }

    GameObject CreateButton(string label, Vector3 pos)
    {
        GameObject btn = Resources.Load("orange_btn") as GameObject;
        GameObject b = Instantiate(btn, pos, q);
        b.GetComponentInChildren<TextMeshProUGUI>().text = label;
        b.name = label.ToLower() + "_btn";
        return b;
    }



    public GameObject CreateTextBoxes(string text)
    {
        TMP_FontAsset font = Resources.Load("Kievit-Medium") as TMP_FontAsset;
        GameObject txt = new GameObject("answer");
        RectTransform rect = txt.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width/2,height/4);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        txt.AddComponent<CanvasRenderer>();
        TextMeshProUGUI tmp = txt.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.font = font;
        tmp.alignment = TextAlignmentOptions.Center;

        return txt;
    }



    void CreateAnswers(MCAnswer[] Answers)
    {
        for (int i = 0; i < Answers.Length; i++)
        {
            GameObject panel = CreateAnswerPanel("response_" + i);
            panel.transform.SetParent(GameObject.Find("mc_answerPanel").transform, false);
            GameObject btn = CreateButton(btn_txt, btn_pos);
            btn.transform.SetParent(panel.transform, false);
            //btn.GetComponent<Button>().onClick.AddListener();
            RectTransform rect = btn.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(1, 0);
            var txt = CreateTextBoxes(answers[i].value);
            txt.GetComponent<RectTransform>().sizeDelta = new Vector2(width/2 - 40,height/5);
            txt.transform.SetParent(panel.transform, false);
        }
    }

    //void GenerateQuizPanel(int width, int height)
    //{

    //    string[] answers = new string[4];
    //    //answers = Shuffle();


    //    GameObject mcAnswerPanel = new GameObject("mc_answerPanel");
    //    mcAnswerPanel.AddComponent<RectTransform>();
    //    mcAnswerPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(width/2, height);
    //    mcAnswerPanel.AddComponent<CanvasRenderer>();
    //    mcAnswerPanel.AddComponent<Image>();
    //    mcAnswerPanel.GetComponent<Image>().color = new Color(255, 255, 255, 0);
    //    mcAnswerPanel.AddComponent<VerticalLayoutGroup>();
    //    mcAnswerPanel.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperRight;
    //    mcAnswerPanel.GetComponent<VerticalLayoutGroup>().padding = new RectOffset(width/128,width/64,height/48,height/48);
    //    mcAnswerPanel.GetComponent<VerticalLayoutGroup>().spacing = height/48;
    //    mcAnswerPanel.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
    //    mcAnswerPanel.GetComponent<VerticalLayoutGroup>().childControlWidth = true;
    //    mcAnswerPanel.transform.SetParent(Selection.activeTransform.transform, false);
    //    mcAnswerPanel.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0.5f);
    //    mcAnswerPanel.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.5f);
    //    mcAnswerPanel.GetComponent<RectTransform>().pivot = new Vector2(1, 0.5f);

    //    GameObject mcQuestionPanel = new GameObject("mc_questionPanel");
    //    mcQuestionPanel.AddComponent<RectTransform>();
    //    mcQuestionPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(width / 2, height);
    //    mcQuestionPanel.AddComponent<CanvasRenderer>();
    //    mcQuestionPanel.AddComponent<Image>();
    //    mcQuestionPanel.GetComponent<Image>().color = new Color(255, 255, 255, 0);
    //    mcQuestionPanel.AddComponent<VerticalLayoutGroup>();
    //    mcQuestionPanel.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.LowerLeft;
    //    mcQuestionPanel.GetComponent<VerticalLayoutGroup>().padding = new RectOffset(width / 64, width / 128, height / 48, height / 48);
    //    mcQuestionPanel.GetComponent<VerticalLayoutGroup>().spacing = height / 48;
    //    mcQuestionPanel.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
    //    mcQuestionPanel.GetComponent<VerticalLayoutGroup>().childControlWidth = true;
    //    mcQuestionPanel.transform.SetParent(Selection.activeTransform.transform, false);
    //    mcQuestionPanel.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
    //    mcQuestionPanel.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
    //    mcQuestionPanel.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);


    //    GameObject question_panel = GenerateQuestionPanel("QuestionPanel_1");
    //    question_panel.transform.SetParent(mcQuestionPanel.transform, false);
    //    GameObject que_btn1 = GenerateButton(back_btn_txt,que_btn_pos);
    //    que_btn1.transform.SetParent(question_panel.transform, false);
    //    que_btn1.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
    //    que_btn1.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
    //    que_btn1.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
    //    GameObject q_text = GenerateTextBoxes(answers[0], 1);
    //    q_text.transform.SetParent(question_panel.transform, false);
    //    q_text.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
    //    q_text.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
    //    q_text.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);


    //    GameObject panel1 = GenerateAnswerPanel(width, height, "answerPanel_1");
    //    panel1.transform.SetParent(mcAnswerPanel.transform, false);
    //    GameObject ans_btn1 = GenerateButton(btn_txt,btn_pos);
    //    ans_btn1.transform.SetParent(panel1.transform, false);
    //    ans_btn1.GetComponent<RectTransform>().anchorMin = new Vector2(1,0);
    //    ans_btn1.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
    //    ans_btn1.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
    //    GameObject ans_txt1 = GenerateTextBoxes(answers[0], 1);
    //    ans_txt1.transform.SetParent(panel1.transform, false);
    //    ans_txt1.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.65f);
    //    ans_txt1.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.65f);
    //    ans_txt1.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);


    //    GameObject panel2 = GenerateAnswerPanel(width, height, "answerPanel_2");
    //    panel2.transform.SetParent(mcAnswerPanel.transform, false);
    //    GameObject ans_btn2 = GenerateButton(btn_txt,btn_pos);
    //    ans_btn2.transform.SetParent(panel2.transform, false);
    //    ans_btn2.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
    //    ans_btn2.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
    //    ans_btn2.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
    //    GameObject ans_txt2 = GenerateTextBoxes(answers[1], 2);
    //    ans_txt2.transform.SetParent(panel2.transform, false);
    //    ans_txt2.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.65f);
    //    ans_txt2.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.65f);
    //    ans_txt2.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);


    //    GameObject panel3 = GenerateAnswerPanel(width, height, "answerPanel_3");
    //    panel3.transform.SetParent(mcAnswerPanel.transform, false);
    //    GameObject ans_btn3 = GenerateButton(btn_txt,btn_pos);
    //    ans_btn3.transform.SetParent(panel3.transform, false);
    //    ans_btn3.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
    //    ans_btn3.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
    //    ans_btn3.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
    //    GameObject ans_txt3 = GenerateTextBoxes(answers[2], 3);
    //    ans_txt3.transform.SetParent(panel3.transform, false);
    //    ans_txt3.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.65f);
    //    ans_txt3.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.65f);
    //    ans_txt3.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);


    //    GameObject panel4 = GenerateAnswerPanel(width, height, "answerPanel_4");
    //    panel4.transform.SetParent(mcAnswerPanel.transform, false);
    //    GameObject ans_btn4 = GenerateButton(btn_txt,btn_pos);
    //    ans_btn4.transform.SetParent(panel4.transform, false);
    //    ans_btn4.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
    //    ans_btn4.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
    //    ans_btn4.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
    //    GameObject ans_txt4 = GenerateTextBoxes(answers[3], 4);
    //    ans_txt4.transform.SetParent(panel4.transform, false);
    //    ans_txt4.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.65f);
    //    ans_txt4.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.65f);
    //    ans_txt4.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

    //}

    //GameObject GenerateQuestionPanel(string name)
    //{
    //    GameObject qPanel = new GameObject();
    //    qPanel.AddComponent<RectTransform>();
    //    qPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(width/2, height);
    //    qPanel.AddComponent<CanvasRenderer>();
    //    qPanel.AddComponent<Image>();
    //    qPanel.GetComponent<Image>().color = questionPanelColor;
    //    return qPanel;
    //}










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
