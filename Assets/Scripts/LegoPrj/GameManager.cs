using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    public PlayerManager playerManager;
    public EnemySpawner enemySpawnerEnemy;
    public LegoEnemyManager enemyManager;
    public EffectController effectController;
    public ColorMatDictionary colorDic;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 0.5f;
    }

    public void StartEndless()
    {

    }

    public void RestartEndless()
    {
        playerManager.health = 5;
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateHealth, playerManager.health);
        enemyManager.gameObject.DestroyAllChildren();
    }

    public void FinishGameEndless()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void LoadGameEndless()
    {

    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

}
