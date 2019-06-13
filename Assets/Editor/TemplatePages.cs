﻿using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TemplatePages : EditorWindow
{
    //
    // Prefab stuff - under the hood
    //
    public GameObject button_prefab;
    public GameObject backwards_prefab;
    public GameObject forwards_prefab;
    public TMP_FontAsset header_font;
    public TMP_FontAsset body_font;
<<<<<<< HEAD
    private Navigation navigation;
=======
    public TextMeshProUGUI header_prefab;
>>>>>>> parent of 328a5cc... everything works, with basic instructions

    //
    // Default values
    //
    private Color bg_color = new Color32(75, 75, 75, 255);
    private Color text_color = new Color32(255, 255, 255, 255);
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


<<<<<<< HEAD
=======

>>>>>>> parent of 328a5cc... everything works, with basic instructions
    [MenuItem("Window/OSU Template Pages")]
    public static void ShowWindow()
    {
        GetWindow<TemplatePages>("OSU Tools");
    }


<<<<<<< HEAD
    //
    // Horizontal Line setup
    //
    private GUIStyle horizontalLine;

    private void HorizontalLine(Color color)
    {
        horizontalLine = new GUIStyle();
        horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
        horizontalLine.margin = new RectOffset(0, 0, 25, 25);
        horizontalLine.fixedHeight = 4;
        var c = GUI.color;
        GUI.color = color;
        GUILayout.Box(GUIContent.none, horizontalLine);
        GUI.color = c;
    }

    private void OnGUI()
    {
        var marginTop = new GUIStyle();
        marginTop.margin = new RectOffset(0, 0, 0, 0);

        var gstyle = new GUIStyle();
        gstyle.wordWrap = true;
        gstyle.padding = new RectOffset(10, 10, 0, 5);

        GUILayout.Label("", marginTop);
        GUILayout.Label("Project Colors:", EditorStyles.boldLabel);
        GUILayout.Label("Select the main colors for your project.", gstyle);
=======
    void OnGUI()
    {
        
        GUILayout.Label("Canvas Reference Dimensions:", EditorStyles.boldLabel);
        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);
>>>>>>> parent of 328a5cc... everything works, with basic instructions

        GUILayout.Label("Set Colors:", EditorStyles.boldLabel);
        bg_color = EditorGUILayout.ColorField("Background Color", bg_color);
        text_color = EditorGUILayout.ColorField("Text Color", text_color);

        if (GUILayout.Button("Generate Canvas"))
        {
<<<<<<< HEAD
        }
        else
        {
            GUILayout.Label("Generate Canvas:", EditorStyles.boldLabel);
            GUILayout.Label(
                "Get started by creating a canvas object. It will be created with a navigation script attached. Use those methods with any created buttons to control the slides.",
                gstyle);
=======
            GenerateCanvas();
        }

        if (GameObject.Find("Canvas"))
        {
            //GUILayout.Label("Prefabs:", EditorStyles.boldLabel);
            //nav_btn = EditorGUILayout.ObjectField("button", nav_btn, typeof(GameObject), true) as GameObject;
>>>>>>> parent of 328a5cc... everything works, with basic instructions


<<<<<<< HEAD
            if (GUILayout.Button("Generate Canvas")) GenerateCanvas();
        }


        if (GameObject.Find("Canvas"))
        {
            GUILayout.Label("Generate Panels:", EditorStyles.boldLabel);
            GUILayout.Label(
                "Create as many panels as you would like. Run 'Panel Cleanup' function if you remove any along the way.",
                gstyle);

            if (GUILayout.Button("Generate Panel")) GeneratePanel();
=======
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

>>>>>>> parent of 328a5cc... everything works, with basic instructions
            if (GUILayout.Button("Panel Cleanup"))
                if (new_canvas != null)
                {
                    var nav = new_canvas.GetComponent<Navigation>();
                    nav.ListCleanup();
                }

<<<<<<< HEAD
            HorizontalLine(Color.grey);

            GUILayout.Label("Formatted Text Components:", EditorStyles.boldLabel);
            GUILayout.Label(
                "Use this portion of the utility to generate text objects with the pre-determined styling. Make sure to select the parent object in the hierarchy before generating.",
                gstyle);

            header_txt = EditorGUILayout.TextField("Header Text", header_txt);
            if (GUILayout.Button("Generate Header Text")) GenerateHeader();
            GUILayout.Label("", marginTop);
            body_txt = EditorGUILayout.TextArea(body_txt, GUILayout.Height(100));
            if (GUILayout.Button("Generate Body Text")) GenerateBody();
            HorizontalLine(Color.grey);


            GUILayout.Label("Button Creator:", EditorStyles.boldLabel);
            GUILayout.Label(
                "Type the text you would like to appear on your button in the box below, select the parent panel in the hierarchy, then generate.",
                gstyle);

            btn_txt = EditorGUILayout.TextField(btn_txt);

            if (GUILayout.Button("Generate Button")) GenerateButton();

            HorizontalLine(Color.grey);

            GUILayout.Label("Generate Navigation:", EditorStyles.boldLabel);
            GUILayout.Label(
                "The button below will generate a panel that will overlay basic navigtion controls on your project",
                gstyle);
=======
            GUILayout.Label("Button Creator:", EditorStyles.boldLabel);
            GUILayout.Label("(Select panel first)");
            btn_txt = EditorGUILayout.TextField(btn_txt);

            if (GUILayout.Button("Generate Button"))
            {
                GenerateButton();
            }
>>>>>>> parent of 328a5cc... everything works, with basic instructions

            if (GUILayout.Button("Generate Navigation")) GenerateNavigation();
        }
    }

<<<<<<< HEAD
    private void GenerateNavigation()
    {
        var nav_panel = new GameObject("navigation_panel");
        nav_panel.AddComponent<CanvasRenderer>();
        nav_panel.AddComponent<Image>();
        nav_panel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        nav_panel.GetComponent<Image>().raycastTarget = false;
        nav_panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        nav_panel.AddComponent<HorizontalLayoutGroup>();
        nav_panel.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.LowerCenter;
        nav_panel.GetComponent<HorizontalLayoutGroup>().padding = new RectOffset(0, 0, 0, 15);
        nav_panel.transform.SetParent(GameObject.Find("Canvas").transform, false);

        var last_button = Instantiate(backwards_prefab);
        last_button.name = "prev_btn";
        last_button.GetComponentInChildren<TextMeshProUGUI>().text = "PREVIOUS";
        last_button.GetComponentInChildren<TextMeshProUGUI>().font = header_font;
        last_button.transform.SetParent(nav_panel.transform, true);

        var next_button = Instantiate(forwards_prefab);
        next_button.name = "next_btn";
        next_button.GetComponentInChildren<TextMeshProUGUI>().text = "NEXT";
        next_button.GetComponentInChildren<TextMeshProUGUI>().font = header_font;
        next_button.transform.SetParent(nav_panel.transform, true);
    }


    private void GenerateHeader()
    {
        var hdr_txt = new GameObject("header_text");
        hdr_txt.AddComponent<TextMeshProUGUI>();
        hdr_txt.GetComponent<TextMeshProUGUI>().text = header_txt;
        hdr_txt.GetComponent<TextMeshProUGUI>().font = header_font;
        hdr_txt.GetComponent<TextMeshProUGUI>().fontSize = 60f;
        hdr_txt.GetComponent<TextMeshProUGUI>().color = text_color;
        hdr_txt.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 68);
        hdr_txt.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        hdr_txt.transform.SetParent(Selection.activeTransform, false);
    }

    private void GenerateBody()
    {
        var bdy_txt = new GameObject("body_text");
        bdy_txt.AddComponent<TextMeshProUGUI>();
        bdy_txt.GetComponent<TextMeshProUGUI>().text = body_txt;
        bdy_txt.GetComponent<TextMeshProUGUI>().font = body_font;
        bdy_txt.GetComponent<TextMeshProUGUI>().fontSize = 36f;
        bdy_txt.GetComponent<TextMeshProUGUI>().color = text_color;
        bdy_txt.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        bdy_txt.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
        bdy_txt.transform.SetParent(Selection.activeTransform, false);
=======
    private void GenerateHeader()
    {

        TextMeshProUGUI hdr = Instantiate(header_prefab);
        
>>>>>>> parent of 328a5cc... everything works, with basic instructions
    }

    private void GenerateButton()
    {
        var btn = Instantiate(button_prefab);
        btn.GetComponentInChildren<TextMeshProUGUI>().text = btn_txt;
        btn.name = btn_txt.ToLower() + "_btn";
        btn.transform.SetParent(Selection.activeTransform, false);
    }

    private void GenerateCanvas()
    {
        // Create a Canvas
        new_canvas = new GameObject("Canvas");
        var c = new_canvas.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        // Add the standard components to the new canvas
        new_canvas.AddComponent<CanvasScaler>();
        new_canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        new_canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
        new_canvas.AddComponent<GraphicRaycaster>();
        new_canvas.AddComponent<Navigation>();
        // Add the event system
        var events = new GameObject("EventSystem");
        events.AddComponent<EventSystem>();
        events.AddComponent<StandaloneInputModule>();

        // Generate Opening Slide
        var opening_panel = new GameObject("Panel_0");
        //opening_panel.tag = "panel";
        opening_panel.AddComponent<CanvasRenderer>();

        opening_panel.AddComponent<RectTransform>();
        opening_panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        var i = opening_panel.AddComponent<Image>();
        i.color = bg_color;

        // Add Panel to list on navigation script
        var nav = new_canvas.GetComponent<Navigation>();
        nav.panels = new List<GameObject>();
        nav.AddToPanels(opening_panel);

        // Add panel as a child of canvas
        opening_panel.transform.SetParent(new_canvas.transform, false);
    }

    private void GeneratePanel()
    {
        // Create a Panel
        var nav = new_canvas.GetComponent<Navigation>();
        var panel = new GameObject("Panel_" + nav.panels.Count);

        // Add the standard components to the new panel
        panel.AddComponent<CanvasRenderer>();

        panel.AddComponent<RectTransform>();
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        var i = panel.AddComponent<Image>();
        i.color = bg_color;

        // Add the panel as a child of the new canvas
        panel.transform.SetParent(new_canvas.transform, false);
        nav.AddToPanels(panel);
    }
}