using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ToolLevelEditor))]
public class ToolLevelEditor : Editor
{
    ToolLevelEditor tool;
    public LegoLevelSpecs level;

    private void OnEnable()
    {
        tool = target as ToolLevelEditor;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveLevelDataButton();
    }

    void SaveLevelDataButton()
    {
        EditorGUILayout.Space(20);
        if (GUILayout.Button("Save level", GUILayout.Height(20)))
        { 
            SaveLevelData();
        }
    }

    void SaveLevelData()
    {

    }
}
