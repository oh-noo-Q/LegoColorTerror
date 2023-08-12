using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
[CustomEditor(typeof(LegoLevelGameplay))]
public class ToolLevelEditor : Editor
{
    static string dataFilePath = "Assets/Data/LevelGameplayData.asset";

    public LegoLevelGameplay tool;

    private void OnEnable()
    {
        tool = target as LegoLevelGameplay;
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
        LevelGameplayDataManager levelData = AssetDatabase.LoadAssetAtPath<LevelGameplayDataManager>(dataFilePath);
        if (tool.level <= levelData.levelGameplayData.Length)
        {
            LevelGameplay newLevel = new LevelGameplay();
            newLevel.level = tool.level;
            newLevel.delaySpawn = tool.timeSpawn;
            newLevel.speedEnemy = tool.speedNormal;
            newLevel.xSpeedFly = tool.flyBuffSpeed - 1.5f;
            newLevel.detailEnemies = tool.detailNormalEnemy;
            newLevel.detailFlyEnemies = tool.detailFlyEnemy;
            newLevel.detailInviEnemies = tool.detailInviEnemy;
            newLevel.detailMixEnemies = tool.detailMixColorEnemy;

            newLevel.detailSlime = tool.detailSlime;

            levelData.levelGameplayData[tool.level - 1] = newLevel;
            EditorUtility.SetDirty(levelData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        else
        {
            LevelGameplay[] levels = new LevelGameplay[tool.level];
            for (int i = 0; i < levelData.levelGameplayData.Length; i++)
            {
                levels[i] = levelData.levelGameplayData[i];
            }
            LevelGameplay newLevel = new LevelGameplay();
            newLevel.level = tool.level;
            newLevel.delaySpawn = tool.timeSpawn;
            newLevel.speedEnemy = tool.speedNormal;
            newLevel.xSpeedFly = tool.flyBuffSpeed - 1.5f;
            newLevel.detailEnemies = tool.detailNormalEnemy;
            newLevel.detailFlyEnemies = tool.detailFlyEnemy;
            newLevel.detailInviEnemies = tool.detailInviEnemy;
            newLevel.detailMixEnemies = tool.detailMixColorEnemy;

            newLevel.detailSlime = tool.detailSlime;

            levels[tool.level - 1] = newLevel;
            levelData.Init(levels);
            EditorUtility.SetDirty(levelData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
*/