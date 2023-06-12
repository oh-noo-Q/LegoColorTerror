using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    public PlayerManager playerManager;
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

    public void RestartGame()
    {
        playerManager.health = 5;
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateHealth, playerManager.health);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
