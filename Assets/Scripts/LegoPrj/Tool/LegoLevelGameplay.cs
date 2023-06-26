using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoLevelGameplay : MonoBehaviour
{
    public int level;
    public float speedMapMoving;
    public float timeSpawn;
    public float speedNormal;
    public float flyBuffSpeed;
    public DetailEnemyLevel[] detailNormalEnemy;
    public DetailEnemyLevel[] detailFlyEnemy;


    [Space(30)]
    public LegoLevelSpecs levelSpecs;
}
