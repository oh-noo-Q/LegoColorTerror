using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelGameplay
{
    [HideInInspector]
    public string name;

    [Header("LegoColor")]
    public int level;
    public float speedEnemy;
    public float xSpeedFly = 1;
    public float delaySpawn;
    public DetailEnemyLevel[] detailEnemies;
    [Space(20)]
    public DetailEnemyLevel[] detailFlyEnemies;
    [Space(20)]
    public DetailEnemyLevel[] detailInviEnemies;

    [Space(20)]
    [HideInInspector] 
    public int endLevelAmountEnemy;
    [HideInInspector]
    public int[] typeEnemy;
    [HideInInspector]
    public bool changeEnemy;
    [HideInInspector]
    public bool changeColor;
    [HideInInspector]
    public int amountColor;
}

[System.Serializable]
public class TypesEnemy
{
    public DetailEnemyLevel[] detailEnemies;
}

[System.Serializable]
public class DetailEnemyLevel
{
    public int id;
    public int quantity;
}
