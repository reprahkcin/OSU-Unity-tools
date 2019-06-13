using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public enum OPTIONS
{
    OSUOrange = 0,
    Blue = 1,
    Red = 2,
    Yellow = 3,
    Green = 4,
    Purple = 5,
    Dark = 6,
    Light = 7
}

public class ButtonGenerator : EditorWindow
{
    public OPTIONS op;

    private GameObject btn;
    private String btn_txt;
    private GUIStyle horizontalLine;

    void InstantiatePrimitive(OPTIONS op)
    {
        switch (op)
        {
            case OPTIONS.OSUOrange:
                GameObject orange_btn = Resources.Load("orange_btn") as GameObject;
                GameObject btn_o = Instantiate(orange_btn);
                btn_o.GetComponentInChildren<TextMeshProUGUI>().text = btn_txt;
                btn_o.name = btn_txt.ToLower() + "_btn";
                btn_o.transform.SetParent(Selection.activeTransform,false);
                break;
            case OPTIONS.Blue:
                GameObject blue_btn = Resources.Load("blue_btn") as GameObject;
                GameObject btn_b = Instantiate(blue_btn);
                btn_b.GetComponentInChildren<TextMeshProUGUI>().text = btn_txt;
                btn_b.name = btn_txt.ToLower() + "_btn";
                btn_b.transform.SetParent(Selection.activeTransform, false);
                break;
            case OPTIONS.Red:
                GameObject red_btn = Resources.Load("red_btn") as GameObject;
                GameObject btn_r = Instantiate(red_btn);
                btn_r.GetComponentInChildren<TextMeshProUGUI>().text = btn_txt;
                btn_r.name = btn_txt.ToLower() + "_btn";
                btn_r.transform.SetParent(Selection.activeTransform, false);
                break;
            case OPTIONS.Yellow:
                GameObject yellow_btn = Resources.Load("yellow_btn") as GameObject;
                GameObject btn_y = Instantiate(yellow_btn);
                btn_y.GetComponentInChildren<TextMeshProUGUI>().text = btn_txt;
                btn_y.name = btn_txt.ToLower() + "_btn";
                btn_y.transform.SetParent(Selection.activeTransform, false);
                break;
            case OPTIONS.Green:
                GameObject green_btn = Resources.Load("green_btn") as GameObject;
                GameObject btn_g = Instantiate(green_btn);
                btn_g.GetComponentInChildren<TextMeshProUGUI>().text = btn_txt;
                btn_g.name = btn_txt.ToLower() + "_btn";
                btn_g.transform.SetParent(Selection.activeTransform, false);
                break;
            case OPTIONS.Purple:
                GameObject purple_btn = Resources.Load("purple_btn") as GameObject;
                GameObject btn_p = Instantiate(purple_btn);
                btn_p.GetComponentInChildren<TextMeshProUGUI>().text = btn_txt;
                btn_p.name = btn_txt.ToLower() + "_btn";
                btn_p.transform.SetParent(Selection.activeTransform, false);
                break;
            case OPTIONS.Dark:
                GameObject dark_btn = Resources.Load("dark_btn") as GameObject;
                GameObject btn_d = Instantiate(dark_btn);
                btn_d.GetComponentInChildren<TextMeshProUGUI>().text = btn_txt;
                btn_d.name = btn_txt.ToLower() + "_btn";
                btn_d.transform.SetParent(Selection.activeTransform, false);
                break;
            case OPTIONS.Light:
                GameObject light_btn = Resources.Load("light_btn") as GameObject;
                GameObject btn_l = Instantiate(light_btn);
                btn_l.GetComponentInChildren<TextMeshProUGUI>().text = btn_txt;
                btn_l.name = btn_txt.ToLower() + "_btn";
                btn_l.transform.SetParent(Selection.activeTransform, false);
                break;
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }
    }








    private void HorizontalLine(Color color)
    {
        horizontalLine = new GUIStyle();
        horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
        horizontalLine.margin = new RectOffset(0, 0, 0, 10);
        horizontalLine.fixedHeight = 4;
        Color c = GUI.color;
        GUI.color = color;
        GUILayout.Box(GUIContent.none, horizontalLine);
        GUI.color = c;
    }

    [MenuItem("Window/OSU Tools/Button Generator")]
    public static void ShowWindow()
    {
        GetWindow<ButtonGenerator>("Button Generator");
    }



    void OnGUI()
    {
        op = (OPTIONS)EditorGUILayout.EnumPopup("1. Select Button Color:", op);
        btn_txt = EditorGUILayout.TextField("2. Enter Button Text:", btn_txt);
        GUILayout.Label("3. Select Parent Canvas or Panel in Hierarchy");
        if (GUILayout.Button("4. Create Button"))
        {
            InstantiatePrimitive(op);
        }
            


        HorizontalLine(Color.grey);

        
    }
}
