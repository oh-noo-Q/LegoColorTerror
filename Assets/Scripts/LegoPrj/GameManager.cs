using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MapSpawner mapSpawner;
    public LevelGameplayDataManager levelData;
    public PlayerManager playerManager;
    public EnemySpawner enemySpawner;
    public LegoEnemyManager enemyManager;
    public TargetObjectManager targetManager;
    public SlimeSpawner slimeSpawner;
    public EffectController effectController;
    public BulletManager bulletManager;

    //dictionary
    public ColorMatDictionary colorDic;
    public InviColorDictionary inviColorDic;

    public bool waitTimeGame;
    public bool startGame;
    int roundEndLess;

    private string _namePlayer = "";
    private int _score;

    //Tool
    [Space(20)]
    [Header("Tool")]
    public bool isTool;
    public LegoLevelGameplay toolLevel;

    private Coroutine waitTimeCor;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        startGame = false;
    }

    public void StartEndless()
    {
        _score = 0;
        roundEndLess = 0;
        enemyManager.ResetLevel();
        ResumeGame();
        RestartEndless();
        LoadGameEndless(roundEndLess);
        UILegoManager.Instance.ShowInGameUI();
        waitTimeGame = true;
        startGame = true;
    }

    public void RestartEndless()
    {
        RevivePlayer();
    }

    public void RevivePlayer()
    {
        playerManager.SetupStart();
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateHealth, playerManager.health);
        ResumeGame();
    }

    public void FinishGameEndless()
    {
        PauseGame();
        UILegoManager.Instance.ShowEndGame();
        if (_score > 0 && _namePlayer != "" && !isTool)
        {
            PlayerPrefsManager.AddHighScore(_score, _namePlayer);
            _score = 0;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        waitTimeGame = false;
        enemyManager.ResetLevel();
        bulletManager.DestroyAllBullet();
    }

    public void LoadGameEndless(int round)
    {
        if(isTool)
        {
            enemySpawner.LoadLevel(toolLevel.detailNormalEnemy, toolLevel.detailFlyEnemy, 
                toolLevel.detailInviEnemy, toolLevel.detailMixColorEnemy,
                toolLevel.speedNormal, toolLevel.speedNormal * toolLevel.flyBuffSpeed, toolLevel.timeSpawn);
            mapSpawner.speed = toolLevel.speedMapMoving;

            slimeSpawner.LoadLevel(toolLevel.speedNormal * toolLevel.detailSlime.buffSpeedSlime, toolLevel.detailSlime.quantity,
                CountTimeSpawnSlime(toolLevel, null));
            EventDispatcher.Instance.PostEvent(EventID.UpdateRoundEndLess, toolLevel.level);
            return;
        }

        //Normal game
        LevelGameplay newRound = levelData.levelGameplayData[round];
        EventDispatcher.Instance.PostEvent(EventID.UpdateRoundEndLess, roundEndLess + 1);

        enemySpawner.LoadLevel(newRound.detailEnemies, newRound.detailFlyEnemies, 
            newRound.detailInviEnemies, newRound.detailMixEnemies,
            newRound.speedEnemy, newRound.speedEnemy * (1.5f + newRound.xSpeedFly), newRound.delaySpawn);
        slimeSpawner.LoadLevel(newRound.speedEnemy * newRound.detailSlime.buffSpeedSlime, newRound.detailSlime.quantity,
                CountTimeSpawnSlime(null, newRound));
    }

    int CountTimeSpawnSlime(LegoLevelGameplay levelGameplay, LevelGameplay level)
    {
        float count = 0;
        int amountSlime = 0;
        if(level != null)
        {
            foreach(DetailEnemyLevel detailEnemy in level.detailEnemies)
            {
                count += detailEnemy.quantity * level.delaySpawn;
            }
            foreach (DetailEnemyLevel detailEnemy in level.detailFlyEnemies)
            {
                count += detailEnemy.quantity * level.delaySpawn;
            }
            foreach (DetailEnemyLevel detailEnemy in level.detailInviEnemies)
            {
                count += detailEnemy.quantity * level.delaySpawn;
            }
            foreach (DetailEnemyLevel detailEnemy in level.detailMixEnemies)
            {
                count += detailEnemy.quantity * level.delaySpawn;
            }
            amountSlime = level.detailSlime.quantity;
        }
        if(levelGameplay != null)
        {
            foreach (DetailEnemyLevel detailEnemy in levelGameplay.detailNormalEnemy)
            {
                count += detailEnemy.quantity * levelGameplay.timeSpawn;
            }
            foreach (DetailEnemyLevel detailEnemy in levelGameplay.detailFlyEnemy)
            {
                count += detailEnemy.quantity * levelGameplay.timeSpawn;
            }
            foreach (DetailEnemyLevel detailEnemy in levelGameplay.detailInviEnemy)
            {
                count += detailEnemy.quantity * levelGameplay.timeSpawn;
            }
            foreach (DetailEnemyLevel detailEnemy in levelGameplay.detailMixColorEnemy)
            {
                count += detailEnemy.quantity * levelGameplay.timeSpawn;
            }
            amountSlime = levelGameplay.detailSlime.quantity;
        }

        return (int)(count / amountSlime);
    }

    public void NextRound()
    {
        if(isTool)
        {
            FinishGameEndless();
            return;
        }
        roundEndLess++;
        if(roundEndLess > levelData.levelGameplayData.Length - 1)
        {
            roundEndLess = levelData.levelGameplayData.Length - 1;
        }
        LoadGameEndless(roundEndLess);
        StartWaitTime(1.0f);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void StartWaitTime(float time)
    {
        if (waitTimeCor != null) StopCoroutine(waitTimeCor);
        waitTimeCor = StartCoroutine(WaitTimeCoroutine(time));
    }
    IEnumerator WaitTimeCoroutine(float waitTime)
    {
        waitTimeGame = false;
        yield return new WaitForSeconds(waitTime);
        waitTimeGame = true;
    }

    IEnumerator WaitTimeBeginRevive()
    {
        float waitTime = 0;
        WaitForSeconds seconds = new WaitForSeconds(0.05f);
        while(waitTime < 2.0f)
        {
            waitTime += Time.deltaTime;
            yield return seconds;
        }
    }

    public void EffectLegoExplosion(Transform posEffect, LegoColor colorFx)
    {
        effectController.GenExplosion(posEffect, colorDic[colorFx]);
    }

    public void SetNamePlayer(string name)
    {
        _namePlayer = name;
    }

    public void UpdateScore(int point)
    {
        _score += point;
        _score = Mathf.Clamp(_score, 0, int.MaxValue);
    }
}
