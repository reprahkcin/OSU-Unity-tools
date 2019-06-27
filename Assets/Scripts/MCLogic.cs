using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class MCLogic : MonoBehaviour
{
    private MCAnswer[] MCBank;

    public GameObject ParentPanel;
    private GameObject mainPanel;

    private Quaternion q = new Quaternion(0, 0, 0, 0);

    public int width;
    public int height;

    private Color panelColor = new Color(255, 255, 255, 0.5f);
    private Color32 answerPanelColor = new Color32(37, 37, 37, 50);

    private Vector3 btn_pos = new Vector3(-11, 11, 0);

    private GameObject defaultBtn;
    private TMP_FontAsset btnFont;
    private TMP_FontAsset bodyFont;

    [SerializeField] private string question = "Question";
    [SerializeField] private Sprite qSprite;

    [SerializeField] private string answer1 = "answer #1";
    [SerializeField] private string reason1 = "reason #1";
    [SerializeField] private bool isCorrect1 = false;

    [SerializeField] private string answer2 = "answer #2";
    [SerializeField] private string reason2 = "reason #2";
    [SerializeField] private bool isCorrect2 = false;

    [SerializeField] private string answer3 = "answer #3";
    [SerializeField] private string reason3 = "reason #3";
    [SerializeField] private bool isCorrect3 = false;

    [SerializeField] private string answer4 = "answer #4";
    [SerializeField] private string reason4 = "reason #1";
    [SerializeField] private bool isCorrect4 = false;

    private MCAnswer a1;
    private MCAnswer a2;
    private MCAnswer a3;
    private MCAnswer a4;

    private void CreateAnswerObjects()
    {
        MCBank = new MCAnswer[4];
        a1 = new MCAnswer(answer1, isCorrect1, reason1);
        a2 = new MCAnswer(answer2, isCorrect2, reason2);
        a3 = new MCAnswer(answer3, isCorrect3, reason3);
        a4 = new MCAnswer(answer4, isCorrect4, reason4);
        MCBank[0] = a1;
        MCBank[1] = a2;
        MCBank[2] = a3;
        MCBank[3] = a4;
    }

    public void TransferAnswers(MCAnswer[] bankToTransfer)
    {
        MCBank = bankToTransfer;
    }
    void Start()
    {
    defaultBtn = Resources.Load("orange_btn") as GameObject;
    btnFont = Resources.Load("Stratum-Bold") as TMP_FontAsset;
    bodyFont = Resources.Load("Kievit-Medium") as TMP_FontAsset;
    mainPanel = new GameObject();
    CreateAnswerObjects();
    CreateMainPanel();
    CreateQuestionPanel();
    CreateQuestion();
    CreateAnswerGroupPanel();
    CreateAnswers(MCBank);
    }

    public void GeneratePopUp(int btnNum)
    {
        GameObject panel = new GameObject("pop-up");
        panel.transform.SetParent(ParentPanel.transform,false);
        RectTransform rect = panel.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f,0.5f);
        rect.anchorMax = new Vector2(0.5f,0.5f);
        rect.pivot = new Vector2(0.5f,0.5f);
        rect.sizeDelta = new Vector2(width/2, height/2);
        panel.AddComponent<CanvasRenderer>();
        panel.AddComponent<Image>();
        panel.GetComponent<Image>().color = panelColor;
        panel.AddComponent<VerticalLayoutGroup>();
        panel.GetComponent<VerticalLayoutGroup>().childControlHeight = true;

        GameObject reasonText = new GameObject("reason-text");
        reasonText.transform.SetParent(panel.transform, false);
        var tempRect = reasonText.AddComponent<RectTransform>();
        tempRect.anchorMin = new Vector2(0.5f,0.5f);
        tempRect.anchorMax = new Vector2(0.5f,0.5f);
        tempRect.pivot = new Vector2(0.5f,0.5f);
        reasonText.AddComponent<CanvasRenderer>();
        var tempText = reasonText.AddComponent<TextMeshProUGUI>();
        tempText.alignment = TextAlignmentOptions.Center;
        tempText.font = bodyFont;
        tempText.color = Color.black;
        tempText.fontSize = 24f;
        tempText.text = MCBank[btnNum].reason;

        GameObject btn = Instantiate(defaultBtn);
        btn.transform.SetParent(panel.transform,false);
        TextMeshProUGUI btnTxt = btn.GetComponentInChildren<TextMeshProUGUI>();
        btnTxt.text = "OK";
        btn.GetComponent<Button>().onClick.AddListener(RemovePopup);
        //RectTransform btnRect = btn.GetComponent<RectTransform>();
        //btnRect.anchorMin = new Vector2(0.5f, 0.5f);
        //btnRect.anchorMax = new Vector2(0.5f, 0.5f);
        //btnRect.pivot = new Vector2(0.5f, 0.5f);
    }

    public void RemovePopup()
    {
        GameObject go = GameObject.Find("pop-up");
        GameObject.Destroy(go);
    }

    private void GenerateDataStore(MCAnswer[] bank)
    {
        GameObject go = new GameObject("QuestionAnswerBank");
        GameObject mainPanel = GameObject.Find("MCQuestionSet");
        go.transform.SetParent(mainPanel.transform, false);
        RectTransform rect = go.AddComponent<RectTransform>();
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = new Vector2(width, height);
        go.AddComponent<CanvasRenderer>();
        go.AddComponent<Image>();
        go.AddComponent<VerticalLayoutGroup>();
        go.GetComponent<VerticalLayoutGroup>().childControlHeight = true;

        for (int i = 0; i < bank.Length; i++)
        {
            GameObject ga = new GameObject("answer" + i);
            ga.AddComponent<RectTransform>();
            ga.AddComponent<CanvasRenderer>();
            Text txt = ga.AddComponent<Text>();
            txt.text = bank[i].value;
            txt.color = Color.black;
            ga.transform.SetParent(go.transform, false);

            GameObject gr = new GameObject("reason" + i);
            gr.AddComponent<RectTransform>();
            gr.AddComponent<CanvasRenderer>();
            Text txtr = gr.AddComponent<Text>();
            txtr.text = bank[i].reason;
            txtr.color = Color.black;
            gr.transform.SetParent(go.transform, false);

            GameObject isC = new GameObject("isCorrect" + i);
            isC.AddComponent<RectTransform>();
            isC.AddComponent<CanvasRenderer>();
            Text isText = isC.AddComponent<Text>();
            isText.color = Color.black;
            if (bank[i].isCorrect)
            { isText.text = "Correct"; }
            else
            { isText.text = "Incorrect"; }
            isC.transform.SetParent(go.transform, false);
        }
        go.SetActive(false);
    }

    private void CreateMainPanel()
    {
        mainPanel = new GameObject("MCQuestionSet");
        mainPanel.transform.SetParent(ParentPanel.transform, false);
        RectTransform rectTrans = mainPanel.AddComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(width, height);
        rectTrans.anchorMin = new Vector2(0.5f, 0.5f);
        rectTrans.anchorMax = new Vector2(0.5f, 0.5f);
        rectTrans.pivot = new Vector2(0.5f, 0.5f);
        mainPanel.AddComponent<CanvasRenderer>();
        Image img = mainPanel.AddComponent<Image>();
        img.color = new Color(255, 255, 255, 0);
        
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
        qRect.sizeDelta = new Vector2(width / 2, height / 2);
        qRect.anchorMax = new Vector2(0.5f, 0.5f);
        qRect.anchorMin = new Vector2(0.5f, 0.5f);
        qRect.pivot = new Vector2(0.5f, 0.5f);
        qImgObj.AddComponent<CanvasRenderer>();
        Image qImg = qImgObj.AddComponent<Image>();
        qImg.sprite = qSprite;

        qImg.transform.SetParent(mcQuestionPanel.transform, false);
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
        rect.transform.position = new Vector3(0, 25, 0);
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
            var txt = CreateTextBoxes(Answers[i].value);
            txt.GetComponent<RectTransform>().sizeDelta = new Vector2(width / 2 - 40, height / 5);
            txt.transform.SetParent(panel.transform, false);
        }
    }
}