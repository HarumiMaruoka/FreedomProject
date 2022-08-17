using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorExtension : EditorWindow
{
    [MenuItem("Test/Editor extention/Sample", false, 1)]
    private static void ShowWindow()
    {
        EditorExtension window = GetWindow<EditorExtension>();
        window.titleContent = new GUIContent("Sample Window");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
