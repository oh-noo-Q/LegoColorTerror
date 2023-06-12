using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelGameplay
{
    [Header("LegoColor")]
    public int level;
    public float speedEnemy;
    public float delaySpawnEnemy;

    [Space(20)]
    public int endLevelAmountEnemy;
    public int[] typeEnemy;
    public bool changeEnemy;
    public bool changeColor;
    public int amountColor;
}
