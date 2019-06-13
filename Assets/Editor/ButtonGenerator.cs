using UnityEditor;
using UnityEngine;

public class ButtonGenerator : EditorWindow
{
    public GameObject prefab;

    [MenuItem("Window/OSU Button Generator")]
    public static void ShowWindow()
    {
        GetWindow<ButtonGenerator>("Button Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Test");
        if (GUILayout.Button("Instance Cube"))
        {
            var cube = Instantiate(prefab);
        }
    }
}
