using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class MCLogic : MonoBehaviour
{
    public string question;
    public MCAnswer[] MCBank;
    public int width;
    public int height;
    private Color panelColor = new Color(255, 255, 255, 0.5f);
    public GameObject defaultBtn;
    public TMP_FontAsset btnFont;
    public TMP_FontAsset bodyFont;
    void Start()
    {
    defaultBtn = Resources.Load("orange-btn") as GameObject;
    btnFont = Resources.Load("Stratum-Bold") as TMP_FontAsset;
    bodyFont = Resources.Load("Kievit-Medium") as TMP_FontAsset;
    }

    public void GeneratePopUp(int btnNum)
    {
        GameObject panel = new GameObject("pop-up");
        GameObject qSetPanel = GameObject.Find("MCQuestionSet");
        panel.transform.SetParent(qSetPanel.transform,false);
        panel.AddComponent<RectTransform>();
        RectTransform rect = panel.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f,0.5f);
        rect.anchorMax = new Vector2(0.5f,0.5f);
        rect.pivot = new Vector2(0.5f,0.5f);
        rect.sizeDelta = new Vector2(width/2, height/2);
        panel.AddComponent<CanvasRenderer>();
        panel.AddComponent<Image>();
        panel.GetComponent<Image>().color = panelColor;

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
        TextMeshProUGUI btnTxt = btn.GetComponent<TextMeshProUGUI>();
        btnTxt.text = "OK";
        btn.GetComponent<Button>().onClick.AddListener(RemovePopup);
        RectTransform btnRect = btn.GetComponent<RectTransform>();
        btnRect.anchorMin = new Vector2(0.5f, 0.5f);
        btnRect.anchorMax = new Vector2(0.5f, 0.5f);
        btnRect.pivot = new Vector2(0.5f, 0.5f);

    }

    public void RemovePopup()
    {
        GameObject go = GameObject.Find("pop-up");
        GameObject.Destroy(go);
    }

}