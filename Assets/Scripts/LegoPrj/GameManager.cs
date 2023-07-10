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
    public EffectController effectController;
    public ColorMatDictionary colorDic;

    public bool waitTimeGame;
    int roundEndLess;

    //Tool
    [Space(20)]
    [Header("Tool")]
    public bool isTool;
    public LegoLevelGameplay toolLevel;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        SoundManager.instance.PlayMusic(SoundType.backgroundMusic);
    }

    public void StartEndless()
    {
        roundEndLess = 0;
        ResumeGame();
        RestartEndless();
        LoadGameEndless(roundEndLess);
        waitTimeGame = true;
        
    }

    public void RestartEndless()
    {
        playerManager.health = 5;
        RevivePlayer();
    }

    public void RevivePlayer()
    {
        playerManager.health = 5;
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateHealth, playerManager.health);
        ResumeGame();
    }

    public void FinishGameEndless()
    {
        PauseGame();
        UILegoManager.Instance.ShowEndGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void LoadGameEndless(int round)
    {
        if(isTool)
        {
            enemySpawner.LoadLevel(toolLevel.detailNormalEnemy, toolLevel.detailFlyEnemy,
                toolLevel.speedNormal, toolLevel.speedNormal * toolLevel.flyBuffSpeed, toolLevel.timeSpawn);
            mapSpawner.speed = toolLevel.speedMapMoving;
            EventDispatcher.Instance.PostEvent(EventID.UpdateRoundEndLess, toolLevel.level);
            return;
        }
        LevelGameplay newRound = levelData.levelGameplayData[round];
        EventDispatcher.Instance.PostEvent(EventID.UpdateRoundEndLess, roundEndLess + 1);

        enemySpawner.LoadLevel(newRound.detailEnemies, newRound.detailFlyEnemies,
            newRound.speedEnemy, newRound.speedEnemy * (1.5f + newRound.xSpeedFly), newRound.delaySpawn);
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
        StartCoroutine(WaitTimeNextRound());
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator WaitTimeNextRound()
    {
        yield return new WaitForSeconds(1.0f);
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
}
