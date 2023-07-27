using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorMatDictionary : SerializableDictionaryBase<LegoColor, Material>
{

}
[System.Serializable]
public class InviColorDictionary : SerializableDictionaryBase<LegoColor, Material>
{

}
public class EnemySpawner : MonoBehaviour
{
    public MapSpawner mapSpawner;
    public LegoEnemyManager enemyManager;
    public GameObject[] enemyPrf;
    public GameObject[] flyEnemyPrf;
    public GameObject[] inviEnemyPrf;
    public GameObject[] mixColorEnemyPrf;

    [Space(10)]
    public int spaceSize;
    public Transform leftLimit, rightLimit;
    public Transform destination;

    [Space(10)]
    [Header("Level")]
    [SerializeField] private List<LegoEnemy> enemyCrtLevel;
    [SerializeField] private List<int> amountEnemy;
    private float delaySpawn = 0.6f;
    private float speed;

    float deltaTime = 0;
    float xLeftLimit, xRightLimit;
    private int[] values = { 0, 1, 2, 3, 4 };

    private void Awake()
    {
        enemyManager = GetComponent<LegoEnemyManager>();
        xLeftLimit = leftLimit.position.x;
        xRightLimit = rightLimit.position.x;

        enemyCrtLevel = new List<LegoEnemy>();
        amountEnemy = new List<int>();
    }

    private void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.waitTimeGame) return;
        deltaTime += Time.deltaTime;
        
        if(deltaTime >= delaySpawn)
        {
            int index = Random.Range(0, enemyCrtLevel.Count);
            if (amountEnemy[index] > 0)
            {
                deltaTime = 0;
                GenerateEnemyForLevel(index);
            }
            else
            {
                if(CheckEndLevel())
                {
                    GameManager.Instance.waitTimeGame = false;
                }
                else
                {
                    return;
                }
            }
        }
    }
    void GenerateEnemyForLevel(int index)
    {
        LegoEnemy newEnemy = Instantiate(enemyCrtLevel[index], transform);
        amountEnemy[index]--;
        newEnemy.gameObject.SetActive(true);
        newEnemy.speed = Random.Range(newEnemy.speed - 1f, newEnemy.speed + 1f);
        switch (mapSpawner.axis)
        {
            case AXIS.Zpositive:
                newEnemy.transform.position = new Vector3(Random.Range(xLeftLimit, xRightLimit), 0, -spaceSize);
                break;
            case AXIS.Znegative:
                newEnemy.transform.position = new Vector3(Random.Range(xLeftLimit, xRightLimit), 0, -spaceSize);
                break;
        }
        if (newEnemy.GetComponent<FlyLego>() != null)
        {
            newEnemy.transform.position = new Vector3(newEnemy.transform.position.x, 10, newEnemy.transform.position.z);
        }
        int ran = Random.Range(0, 5);
        int colorRan = values[ran];
        newEnemy.SetColor((LegoColor)colorRan);
        newEnemy.moveDirection = -mapSpawner.moveDirection;
        newEnemy.distanceDie = destination;

        if (enemyManager.currentTargetEnemy == null && newEnemy.GetComponent<InviLego>() == null)
        {
            enemyManager.SetTargetEnemy(newEnemy);
        }
        enemyManager.enemies.Add(newEnemy);
    }

    public bool CheckEndLevel()
    {
        int countCheck = 0;
        for(int i = 0; i < amountEnemy.Count; i++)
        {
            countCheck += amountEnemy[i];
        }
        if (countCheck > 0) return false;
        else return true;
    }

    public void LoadLevel(DetailEnemyLevel[] normalEnemy, DetailEnemyLevel[] flyEnemy, 
        DetailEnemyLevel[] inviEnemy, DetailEnemyLevel[] mixEnemy,
        float normalSpeed, float flySpeed, float _delaySpawn)
    {
        ShuffleArray(values);
        enemyCrtLevel.Clear();
        amountEnemy.Clear();
        if (normalEnemy != null)
        {
            for (int i = 0; i < normalEnemy.Length; i++)
            {
                enemyPrf[normalEnemy[i].id - 1].GetComponent<LegoEnemy>().speed = normalSpeed;
                enemyCrtLevel.Add(enemyPrf[normalEnemy[i].id - 1].GetComponent<LegoEnemy>());
                amountEnemy.Add(normalEnemy[i].quantity);
            }
        }

        if(inviEnemy != null)
        {
            for (int i = 0; i < inviEnemy.Length; i++)
            {
                LegoEnemy editEnemy = inviEnemyPrf[inviEnemy[i].id - 1].GetComponent<LegoEnemy>();
                editEnemy.speed = normalSpeed;
                enemyCrtLevel.Add(editEnemy);
                amountEnemy.Add(inviEnemy[i].quantity);
            }
        }

        if (mixEnemy != null)
        {
            for (int i = 0; i < mixEnemy.Length; i++)
            {
                LegoEnemy editEnemy = mixColorEnemyPrf[mixEnemy[i].id - 1].GetComponent<LegoEnemy>();
                editEnemy.speed = normalSpeed;
                enemyCrtLevel.Add(editEnemy);
                amountEnemy.Add(mixEnemy[i].quantity);
            }
        }

        if (flyEnemy != null)
        {
            for (int i = 0; i < flyEnemy.Length; i++)
            {
                LegoEnemy editEnemy = flyEnemyPrf[flyEnemy[i].id - 1].GetComponent<LegoEnemy>();
                editEnemy.speed = flySpeed;
                editEnemy.buffSpeed = flySpeed / normalSpeed;
                enemyCrtLevel.Add(editEnemy);
                amountEnemy.Add(flyEnemy[i].quantity);
            }
        }
        speed = normalSpeed;
        delaySpawn = _delaySpawn;
    }
}
