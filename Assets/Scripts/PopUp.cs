using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    private string headerMessage;
    private string bodyMessage;
    private Vector2 center = new Vector2(0.5f, 0.5f);
    private Color incorrectPanelColor = new Color(224,69,56,162);
    private Color correctPanelColor = new Color(124,217,70,162);

    public void kill(GameObject g)
    {
        Destroy(g);
    }
    void generatePanel(MCAnswer answer)
    {
        // Main pop-up panel
        GameObject go = new GameObject();
        RectTransform rect = go.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width*0.8f, GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height * 0.8f);
        rect.anchorMin = center;
        rect.anchorMax = center;
        rect.pivot = center;
        go.AddComponent<CanvasRenderer>();
        Image img = go.AddComponent<Image>();
        if (answer.isCorrect)
        {
            img.color = correctPanelColor;
        } else {
            img.color = incorrectPanelColor;
        }

        // "Okay" Button
        GameObject doneBtn = Resources.Load("orange_btn") as GameObject;
        doneBtn.GetComponent<TextMeshProUGUI>().text = "Okay";
        doneBtn.transform.SetParent(go.transform,false);
        doneBtn.GetComponent<Button>().onClick.AddListener(() => kill(go));
    }
}
