using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorMatDictionary : SerializableDictionaryBase<LegoColor, Material>
{

}
public class EnemySpawner : MonoBehaviour
{
    public MapSpawner mapSpawner;
    public LegoEnemyManager enemyManager;
    public GameObject[] enemyPrf;
    public ColorMatDictionary colorDic;

    [Space(10)]
    public int spaceSize;
    public Transform leftLimit, rightLimit;

    public float speed;
    public Transform distanceDie;

    public float delaySpawn = 0.6f;
    float deltaTime = 0;
    float xLeftLimit, xRightLimit;

    private void Awake()
    {
        enemyManager = GetComponent<LegoEnemyManager>();
        xLeftLimit = leftLimit.position.x;
        xRightLimit = rightLimit.position.x;
    }

    private void Update()
    {
        deltaTime += Time.deltaTime;

        if(deltaTime > delaySpawn)
        {
            deltaTime = 0;

            int index = Random.Range(0, enemyPrf.Length);
            GameObject newEnemy = Instantiate(enemyPrf[index], transform);
            newEnemy.SetActive(true);
            switch(mapSpawner.axis)
            {
                case AXIS.Zpositive:
                    newEnemy.transform.position = new Vector3(Random.Range(xLeftLimit, xRightLimit), 0, -spaceSize);
                    break;
                case AXIS.Znegative:
                    newEnemy.transform.position = new Vector3(Random.Range(xLeftLimit, xRightLimit), 0, -spaceSize);
                    break;
            }

            LegoEnemy legoEnemy = newEnemy.GetComponent<LegoEnemy>();
            int colorRan = Random.Range(0, 5);
            Debug.Log(colorRan +": time" + Time.time);
            for(int i = 0; i < legoEnemy.pieces.Count; i++)
            {
                legoEnemy.pieces[i].GetComponent<Renderer>().material = colorDic[(LegoColor)colorRan];
            }
            legoEnemy.mainColor = (LegoColor)colorRan;
            legoEnemy.moveDirection = -mapSpawner.moveDirection;
            legoEnemy.speed = speed;
            legoEnemy.distanceDie = distanceDie;

            if (enemyManager.enemies.Count == 0) enemyManager.currentTargetEnemy = legoEnemy;
            enemyManager.enemies.Add(legoEnemy);
        }
    }
}
