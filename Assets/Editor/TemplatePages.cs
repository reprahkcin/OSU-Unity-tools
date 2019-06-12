using System;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TemplatePages : EditorWindow
{
    //
    // Prefab stuff - under the hood
    //
    public GameObject nav_btn;
    public TMP_FontAsset header_font;
    public TMP_FontAsset body_font;
    public TextMeshProUGUI header_prefab;

    //
    // Default values
    //
    private Color bg_color = new Color32(75,75,75,255);
    private Color text_color = new Color32(255,255,255,255);
    private int height = 768;
    private int width = 1024;

    //
    // Placeholders
    //
    private GameObject new_canvas;
    private string btn_txt;
    private string header_txt;
    private string body_txt;
    private Sprite img;



    [MenuItem("Window/OSU Template Pages")]
    public static void ShowWindow()
    {
        GetWindow<TemplatePages>("OSU Tools");
    }


    void OnGUI()
    {
        
        GUILayout.Label("Canvas Reference Dimensions:", EditorStyles.boldLabel);
        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);

        GUILayout.Label("Set Colors:", EditorStyles.boldLabel);
        bg_color = EditorGUILayout.ColorField("Background Color", bg_color);
        text_color = EditorGUILayout.ColorField("Text Color", text_color);

        if (GUILayout.Button("Generate Canvas"))
        {
            GenerateCanvas();
        }

        if (GameObject.Find("Canvas"))
        {
            //GUILayout.Label("Prefabs:", EditorStyles.boldLabel);
            //nav_btn = EditorGUILayout.ObjectField("button", nav_btn, typeof(GameObject), true) as GameObject;


            GUILayout.Label("Basic Elements:", EditorStyles.boldLabel);
            header_txt = EditorGUILayout.TextField("Header Text", "");
            if (GUILayout.Button("Generate Header"))
            {
                GenerateHeader();
            }

            body_txt = EditorGUILayout.TextField("Body Text", "");
            img = EditorGUILayout.ObjectField("Image", img, typeof(Sprite), true) as Sprite;

            if (GUILayout.Button("Generate Panel"))
            {
                GeneratePanel();
            }

            if (GUILayout.Button("Panel Cleanup"))
            {
                if (new_canvas != null)
                {
                    Navigation nav = new_canvas.GetComponent<Navigation>();
                    nav.ListCleanup();
                }
            }

            GUILayout.Label("Button Creator:", EditorStyles.boldLabel);
            GUILayout.Label("(Select panel first)");
            btn_txt = EditorGUILayout.TextField(btn_txt);

            if (GUILayout.Button("Generate Button"))
            {
                GenerateButton();
            }

        }
    }

    private void GenerateHeader()
    {

        TextMeshProUGUI hdr = Instantiate(header_prefab);
        
    }

    private void GenerateButton()
    {
        GameObject btn = Instantiate(nav_btn);
        btn.GetComponentInChildren<TextMeshProUGUI>().text = btn_txt;
        btn.name = btn_txt.ToLower() + "_btn";
        btn.transform.SetParent(Selection.activeTransform, false);
    }

    void GenerateCanvas()
    {
        // Create a Canvas
        new_canvas = new GameObject("Canvas");
        Canvas c = new_canvas.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        // Add the standard components to the new canvas
        new_canvas.AddComponent<CanvasScaler>();
        new_canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        new_canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
        new_canvas.AddComponent<GraphicRaycaster>();
        new_canvas.AddComponent<Navigation>();
        // Add the event system
        GameObject events = new GameObject("EventSystem");
        events.AddComponent<EventSystem>();
        events.AddComponent<StandaloneInputModule>();

        // Generate Opening Slide
        GameObject opening_panel = new GameObject("Panel_0");
        //opening_panel.tag = "panel";
        opening_panel.AddComponent<CanvasRenderer>();

        opening_panel.AddComponent<RectTransform>();
        opening_panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        Image i = opening_panel.AddComponent<Image>();
        i.color = bg_color;
        
        // Add Panel to list on navigation script
        Navigation nav = new_canvas.GetComponent<Navigation>();
        nav.panels = new List<GameObject>();
        nav.AddToPanels(opening_panel);

        // Add panel as a child of canvas
        opening_panel.transform.SetParent(new_canvas.transform,false);
    }

    void GeneratePanel()
    {
        // Create a Panel
        Navigation nav = new_canvas.GetComponent<Navigation>();
        GameObject panel = new GameObject("Panel_" + nav.panels.Count);
              
        // Add the standard components to the new panel
        panel.AddComponent<CanvasRenderer>();

        panel.AddComponent<RectTransform>();
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        Image i = panel.AddComponent<Image>();
        i.color = bg_color;
        
        // Add the panel as a child of the new canvas
        panel.transform.SetParent(new_canvas.transform, false);
        nav.AddToPanels(panel);      
    }
        
}

