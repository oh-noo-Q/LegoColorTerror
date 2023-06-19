using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelGameplayDataManager levelData;
    public PlayerManager playerManager;
    public EnemySpawner enemySpawner;
    public LegoEnemyManager enemyManager;
    public EffectController effectController;
    public ColorMatDictionary colorDic;

    public bool startEndLessMode;
    int roundEndLess;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void StartEndless()
    {
        roundEndLess = 0;
        ResumeGame();
        RestartEndless();
        LoadGameEndless(roundEndLess);
        startEndLessMode = true;
        
    }

    public void RestartEndless()
    {
        playerManager.health = 5;
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateHealth, playerManager.health);
        enemyManager.gameObject.DestroyAllChildren();
    }

    public void FinishGameEndless()
    {
        ReloadScene();
        UILegoManager.Instance.ShowMainMenu();
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
        LevelGameplay newRound = levelData.levelGameplayData[round];
  
        enemySpawner.LoadLevel(newRound.detailEnemies, newRound.detailFlyEnemies,
            newRound.speedEnemy, newRound.speedEnemy * (1.5f + newRound.xSpeedFly), newRound.delaySpawn);
    }

    public void NextRound()
    {
        roundEndLess++;
        if(roundEndLess > levelData.levelGameplayData.Length - 1)
        {
            roundEndLess = levelData.levelGameplayData.Length - 1;
        }
        startEndLessMode = true;
        LoadGameEndless(roundEndLess);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

}
