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
    public DetailSlime detailSlime;
    public DetailEnemyLevel[] detailNormalEnemy;
    public DetailEnemyLevel[] detailFlyEnemy;
    public DetailEnemyLevel[] detailInviEnemy;
    public DetailEnemyLevel[] detailMixColorEnemy;


    [Space(30)]
    public LegoLevelSpecs levelSpecs;
}
