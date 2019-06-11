using System;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

public class TemplatePages : EditorWindow
{
    public Color bg_color = new Color32(75,75,75,255);
    private Color text_color = new Color32(255,255,255,255);
    private Color nav_button_color = new Color32(215, 63, 9, 255);
    private TMP_FontAsset header_font;
    private TMP_FontAsset body_font;
    private string header_text;
    private string body_text;
    private Sprite img;

    private int height = 768;
    private int width = 1024;

    [MenuItem("Window/OSU Template Pages")]
    public static void ShowWindow()
    {
        GetWindow<TemplatePages>("Page Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Page Reference Dimensions:", EditorStyles.boldLabel);
        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);
        GUILayout.Label("Set Colors:", EditorStyles.boldLabel);
        bg_color = EditorGUILayout.ColorField("Background Color", bg_color);
        text_color = EditorGUILayout.ColorField("Text Color", text_color);
        nav_button_color = EditorGUILayout.ColorField("Button Color", nav_button_color);
        GUILayout.Label("Set Fonts:", EditorStyles.boldLabel);
        header_font = EditorGUILayout.ObjectField("Header Font", header_font, typeof(TMP_FontAsset), true) as TMP_FontAsset;
        body_font = EditorGUILayout.ObjectField("Body Font", body_font, typeof(TMP_FontAsset), true) as TMP_FontAsset;
        GUILayout.Label("Basic Elements:", EditorStyles.boldLabel);
        header_text = EditorGUILayout.TextField("Header Text", "");
        body_text = EditorGUILayout.TextField("Body Text", "");
        img = EditorGUILayout.ObjectField("Image", img, typeof(Sprite), true) as Sprite;

        if (GUILayout.Button("Generate Canvas"))
        {
            GenerateCanvas();
        }

    }

    void GenerateCanvas()
    {
        // Create a Canvas
        GameObject newCanvas = new GameObject("Canvas");
        Canvas c = newCanvas.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;

        // Add the standard components to the new canvas
        newCanvas.AddComponent<CanvasScaler>();
        newCanvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        newCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
        newCanvas.AddComponent<GraphicRaycaster>();

        // Create a Panel
        GameObject panel = new GameObject("Panel");
        // Add the standard components to the new panel
        panel.AddComponent<CanvasRenderer>();
        panel.AddComponent<RectTransform>();
        Image i = panel.AddComponent<Image>();
        i.color = bg_color;
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        // Add the panel as a child of the new canvas
        panel.transform.SetParent(newCanvas.transform, false);

        // Create Button
        GameObject next_button = new GameObject("Next Button");
        // Create Button text
        GameObject next_button_text = new GameObject("Text");
        next_button_text.AddComponent<CanvasRenderer>();
        next_button_text.AddComponent<TextMeshProUGUI>();
        next_button_text.GetComponent<TextMeshProUGUI>().text = "Next";
        next_button_text.GetComponent<TextMeshProUGUI>().font = header_font;
        next_button_text.transform.SetParent(next_button.transform, false);
        next_button.transform.SetParent(panel.transform, false);
        next_button.AddComponent<CanvasRenderer>();
        Image i_next = next_button.AddComponent<Image>();
        i_next.color = nav_button_color;
        next_button.AddComponent<Button>();





    }
}

