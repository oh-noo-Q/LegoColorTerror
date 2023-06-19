using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public InGameManager InGameManager;
    public GateManager gateManager;
    public EnemyDataManager enemyData;
    public ColorDataManager colorData;
    public LevelGameplayDataManager gameplayData;
    public List<Color> currentColorEnemy;
    public List<Button> buttonManager;
    public Transform mainPlayer;
    public Bullet beamPrefab;
    public GameObject enemyExplosionPrefab;
    public Transform enemyParent;
    public GameObject bonusCoin;
    public Animator gru;

    private int currentLevel;
    private int amountEnemyNextLevel;
    private float timeSpawnWhiteBall;
    private float timeDelayWhiteBall;
    private int targetColor;
    private int amountColor;
    private float timeDelaySpawn;
    private float timeToSpawn;
    private float speedEnemy;
    [SerializeField] int[] typeEnemy;
    [SerializeField] int amountDieEnemy;
    [SerializeField] List<Enemy> enemyAlive;

    private void Start()
    {
        currentLevel = 0;
        ChangeLevelGameplay(currentLevel);
        
        enemyAlive = new List<Enemy>();
        timeDelayWhiteBall = Random.Range(8.0f, 15.0f);

        ChangeCurrentColor();

        EventDispatcher.Instance.RegisterListener(EventID.KillEnemy, DestroyEnemy);
        EventDispatcher.Instance.RegisterListener(EventID.ResetGame, ResetGame);
    }
    public void ResetGame(object obj)
    {
        timeSpawnWhiteBall = 0;
        currentLevel = 0;
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateCurrentLevel, $"{currentLevel + 1}");
        ChangeLevelGameplay(0);
        amountDieEnemy = 0;
        ChangeCurrentColor();
        EventDispatcher.Instance.PostEvent(EventID.UnlockButtonInGame, 2);
        ClearEnemy(null);
    }

    private void ChangeLevelGameplay(int level)
    {
        LevelGameplay levelGameplay = gameplayData.levelGameplayData[level];
        timeDelaySpawn = levelGameplay.delaySpawn;
        if (amountColor != levelGameplay.amountColor)
        {
            amountColor = levelGameplay.amountColor;
            EventDispatcher.Instance.PostEvent(EventID.UnlockButtonInGame, amountColor - 1);
        }
        speedEnemy = levelGameplay.speedEnemy;
        amountEnemyNextLevel = levelGameplay.endLevelAmountEnemy;
        InGameManager.changeMaxEnemy(amountEnemyNextLevel);
        //EventDispatcher.Instance.PostEvent(EventID.OnChangeMaxEnemy, amountEnemyNextLevel);
        if(levelGameplay.changeEnemy)
        {
            typeEnemy = levelGameplay.typeEnemy;
        }

        if(levelGameplay.changeColor)
        {
            ChangeCurrentColor();
        }
    }

    private void ChangeCurrentColor()
    {
        currentColorEnemy = new List<Color>();
        currentColorEnemy.Add(Color.white);
        int startColor = Random.Range(1, colorData.colorData.Length);
        for (int i = 1; i < 6; i++)
        {
            currentColorEnemy.Add(colorData.colorData[startColor].colorMain);
            startColor++;
            if (startColor > colorData.colorData.Length - 1)
                startColor = 1;
            buttonManager[i - 1].GetComponent<Image>().color = currentColorEnemy[i];
        }
    }

    private void Update()
    {
        timeSpawnWhiteBall += Time.deltaTime;
        timeToSpawn += Time.deltaTime;
        if (timeToSpawn < timeDelaySpawn) return;
        else
        {
            SpawnEnemy();
            timeToSpawn = 0;
        }
        if(amountDieEnemy >= amountEnemyNextLevel)
        {
            amountDieEnemy = 0;
            currentLevel++;
            EventDispatcher.Instance.PostEvent(EventID.OnUpdateCurrentLevel, $"{currentLevel + 1}");
            if(currentLevel <= gameplayData.levelGameplayData.Length)
                ChangeLevelGameplay(currentLevel);
        }
    }
    void SpawnEnemy()
    {
        int randomPos = Random.Range(0, gateManager.gate.Count);
        int randomEnemy = Random.Range(0, typeEnemy.Length);
        int randomColor = -1;
        if(timeDelayWhiteBall < timeSpawnWhiteBall)
        {
            randomColor = 0;
            timeSpawnWhiteBall = 0;
            timeDelayWhiteBall = Random.Range(7.0f, 10.0f);
        }
        else 
            randomColor = Random.Range(1, amountColor);
        Enemy newEnemy = Instantiate(enemyData.enemyData[typeEnemy[randomEnemy]], gateManager.gate[randomPos].position,
            gateManager.gate[randomPos].rotation);
        newEnemy.transform.SetParent(enemyParent);
        newEnemy.InitEnemy(enemyAlive.Count, randomColor, mainPlayer.position + new Vector3(0, 3, 0), 
            speedEnemy, currentColorEnemy[randomColor]);
        if (enemyAlive.Count == 0)
        {
            targetColor = randomColor;
            newEnemy.SetEnemyTarget();
        }
        enemyAlive.Add(newEnemy);
    }

    public void KillEnemy(int index)
    {
        gru.Play("Shooting_mixamo_com");
        if (targetColor == 0)
        {
            int randomColor = -1;
            if (amountColor < 6)
            {
                randomColor = amountColor;
                currentLevel++;
                ChangeLevelGameplay(currentLevel);
            }
            else
            {
                randomColor = Random.Range(1, amountColor);
                speedEnemy += 1f;
            }
            enemyAlive[0].InitWhiteEnemy(randomColor, currentColorEnemy[randomColor]);
            targetColor = randomColor;
        }
        else
        {
            if (index == targetColor)
            {
                enemyAlive[0].health--;
                if (enemyAlive[0].health == 0)
                {
                    if (currentLevel != 0)
                        bonusCoin.GetComponent<TextMesh>().text = "50+" + $"{currentLevel}";
                    else
                        bonusCoin.GetComponent<TextMesh>().text = "50";
                    GameObject cloneBonusCoin = Instantiate(bonusCoin, enemyAlive[0].transform.position + new Vector3(0, 3, 0), bonusCoin.transform.rotation);
                    
                    Destroy(cloneBonusCoin.gameObject, 0.5f);
                    EffectKillEnemy(enemyAlive[0]);
                    if (enemyAlive.Count > 0)
                    {
                        ResetTargetEnemy();
                    }
                    amountDieEnemy++;
                    EventDispatcher.Instance.PostEvent(EventID.OnChangeProcessLevel, amountDieEnemy);
                    EventDispatcher.Instance.PostEvent(EventID.UpdateCoinKillEnemy, 50 + currentLevel);
                }
            }
            else
            {
                enemyAlive[0].AngryEnemy();
            }
        }
    }

    public void DestroyEnemy(object index)
    {
        for(int i = 0; i < enemyAlive.Count; i++)
        {
            if (enemyAlive[i].id == (int)index)
            {
                EffectKillEnemy(enemyAlive[i]);
                if(enemyAlive.Count > 0)
                    ResetTargetEnemy();
                break;
            }
        }
    }

    private void ResetTargetEnemy()
    {
        targetColor = enemyAlive[0].currentColor;
        enemyAlive[0].SetEnemyTarget();
    }

    void EffectKillEnemy(Enemy enemy)
    {
        //GameObject explosion = Instantiate(enemyExplosionPrefab, enemy.transform.position,
        //    enemyExplosionPrefab.transform.rotation);
        //explosion.transform.SetParent(enemyParent);
        //Destroy(explosion, 1.0f);
        SoundManager.instance.PlaySingle(SoundType.balloonExplosion);
        enemy.arrow.SetActive(false);
        enemy.enemyAnim.Play("Flying_Back_Death_mixamo_com");
        enemy.Die();
        enemyAlive.Remove(enemy);
        Destroy(enemy.gameObject, 2.0f);
    }

    void ClearEnemy(object obj)
    {
        for(int i = 0; i < enemyParent.childCount; i++)
        {
            Destroy(enemyParent.GetChild(i).gameObject);
        }
        enemyAlive = new List<Enemy>();
    }

    public Enemy GetCurrentEnemy()
    {
        if (enemyAlive.Count == 0) return null;
        return enemyAlive[0];
    }
}
