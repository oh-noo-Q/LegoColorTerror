using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelGameplay
{
    public int level;
    public int amountColor;
    public int endLevelAmountEnemy;
    public float speedEnemy;
    public float delaySpawnEnemy;
    public bool changeEnemy;
    public int[] typeEnemy;
    public bool changeColor;
}
