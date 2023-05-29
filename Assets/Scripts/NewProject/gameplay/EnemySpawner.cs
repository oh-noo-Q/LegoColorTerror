using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public MapSpawner mapSpawner;
    public GameObject[] enemys;

    [Space(10)]
    public int spaceSize;
    public Transform leftLimit, rightLimit;

    public float speed;

    public float delaySpawn = 0.6f;
    float deltaTime = 0;
    float xLeftLimit, xRightLimit;

    private void Awake()
    {
        xLeftLimit = leftLimit.position.x;
        xRightLimit = rightLimit.position.x;
    }

    private void Update()
    {
        deltaTime += Time.deltaTime;

        if(deltaTime > delaySpawn)
        {
            deltaTime = 0;

            int index = Random.Range(0, enemys.Length);
            GameObject newEnemy = Instantiate(enemys[index]);
            newEnemy.SetActive(true);
            switch(mapSpawner.axis)
            {
                case AXIS.Zpositive:
                    newEnemy.transform.position = new Vector3(Random.Range(xLeftLimit, xRightLimit), 0, -spaceSize);
                    break;
                case AXIS.Znegative:
                    newEnemy.transform.position = new Vector3(Random.Range(xLeftLimit, xRightLimit), 0, spaceSize);
                    break;
            }

            LegoEnemy legoEnemy = newEnemy.GetComponent<LegoEnemy>();
            legoEnemy.moveDirection = mapSpawner.moveDirection;
            legoEnemy.speed = speed;
        }
    }
}
