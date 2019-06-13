using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasPanels : EditorWindow
{
    //
    // Prefab stuff - under the hood
    //
    public TMP_FontAsset header_font;
    public TMP_FontAsset body_font;

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
    private string header_txt;
    private string body_txt;


    [MenuItem("Window/OSU Tools/Canvas and Panels")]
    public static void ShowWindow()
    {
        GetWindow<CanvasPanels>("Canvas and Panels");
    }


    //
    // Horizontal Line setup
    //
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

    private void OnGUI()
    {
        OSUTools = new GUIStyle();

        GUIStyle gstyle = new GUIStyle();
        gstyle.wordWrap = true;
        gstyle.padding = new RectOffset(10, 10, 0, 5);

        GUILayout.Label("",OSUTools);
        GUILayout.Label("Project Colors:", EditorStyles.boldLabel);
        GUILayout.Label("Select the main colors for your project.", gstyle);
        bg_color = EditorGUILayout.ColorField("Background Color", bg_color);
        text_color = EditorGUILayout.ColorField("Text Color", text_color);

        HorizontalLine(Color.grey);

        if (GameObject.Find("Canvas"))
        {

        }
        else
        {
            GUILayout.Label("Generate Canvas:", EditorStyles.boldLabel);
            GUILayout.Label("Get started by creating a canvas object. It will be created with a navigation script attached. Use those methods with any created buttons to control the slides.", gstyle);

            width = EditorGUILayout.IntField("Width", width);
            height = EditorGUILayout.IntField("Height", height);


            if (GUILayout.Button("Generate Canvas"))
            {
                GenerateCanvas();
            }

            HorizontalLine(Color.grey);
        }


        GUILayout.Label("Generate Panels:", EditorStyles.boldLabel);
        GUILayout.Label("Create as many panels as you would like. Run 'Panel Cleanup' function if you remove any along the way.", gstyle);

        if (GUILayout.Button("Generate Panel")) GeneratePanel();


        if (GUILayout.Button("Panel Cleanup"))
        {
            if (new_canvas != null)
            {
                var nav = new_canvas.GetComponent<Navigation>();
                nav.ListCleanup();
            }
        }

        HorizontalLine(Color.grey);

        GUILayout.Label("Formatted Text Components:", EditorStyles.boldLabel);
        GUILayout.Label(
            "Use this portion of the utility to generate text objects with the pre-determined styling. Make sure to select the parent object in the hierarchy before generating.",
            gstyle);

        header_txt = EditorGUILayout.TextField("Header Text", header_txt);

        if (GUILayout.Button("Generate Header Text"))
        {
            GenerateHeader();
        }

        GUILayout.Label("");

        body_txt = EditorGUILayout.TextArea(body_txt, GUILayout.Height(100));

        if (GUILayout.Button("Generate Body Text"))
        {
            GenerateBody();
        }


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
        var opening_panel = new GameObject("panel_0");
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
        var panel = new GameObject("panel_" + nav.panels.Count);

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