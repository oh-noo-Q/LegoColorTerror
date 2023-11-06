using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public PlayerController player;

    public StartGameManager startGamePanel;
    public InGameManager inGamePanel;
    public EndGameManager endGamePanel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateCoins()
    {
        
    }

    private void Start()
    {
        ShowStartGame();
        SoundManager.instance.PlayMusic(SoundName.BGMenuMusic);
    }

    public void ShowStartGame()
    {
        PauseGame();
        startGamePanel.gameObject.SetActive(true);
        inGamePanel.gameObject.SetActive(false);
    }

    public void ShowEndGame()
    {
        endGamePanel.Show((int) inGamePanel.scoreNumber, inGamePanel.coinInGame);
        inGamePanel.gameObject.SetActive(false);
        //PlayerPrefsManager.AddHighScore((int) inGamePanel.scoreNumber, "");
        PauseGame();
    }

    public void StartGame()
    {
        startGamePanel.gameObject.SetActive(false);
        inGamePanel.gameObject.SetActive(true);
        Time.timeScale = 1.0f;
    }
    public void RetryButton()
    {
        player.health = 5;
        EventDispatcher.Instance.PostEvent(EventID.ResetGame);
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateHealth, $"{player.health}");
        endGamePanel.Hide();
        ShowStartGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

}
