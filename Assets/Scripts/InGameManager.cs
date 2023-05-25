using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public Text health, coinNumber, score, level;
    public Text processText;
    public List<Button> buttonManager;
    public Slider processLevel;
    public float scoreNumber;
    public int coinInGame;
    public GameObject pausePopup;
    private int maxEnemy;

    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnUpdateHealth, UpdateHealthText);
        EventDispatcher.Instance.RegisterListener(EventID.UnlockButtonInGame, UnlockButton);
        EventDispatcher.Instance.RegisterListener(EventID.UpdateCoinKillEnemy, UpdateCoinText);
        EventDispatcher.Instance.RegisterListener(EventID.OnUpdateCurrentLevel, UpdateCurrentLevel);
        EventDispatcher.Instance.RegisterListener(EventID.OnChangeProcessLevel, changeProcess);
        EventDispatcher.Instance.RegisterListener(EventID.OnChangeMaxEnemy, changeMaxEnemy);
        UnlockButton(2);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener(EventID.OnUpdateHealth, UpdateHealthText);
        EventDispatcher.Instance.RemoveListener(EventID.UnlockButtonInGame, UnlockButton);
        EventDispatcher.Instance.RemoveListener(EventID.UpdateCoinKillEnemy, UpdateCoinText);
        EventDispatcher.Instance.RemoveListener(EventID.OnUpdateCurrentLevel, UpdateCurrentLevel);
        EventDispatcher.Instance.RemoveListener(EventID.OnChangeProcessLevel, changeProcess);
        EventDispatcher.Instance.RemoveListener(EventID.OnChangeMaxEnemy, changeMaxEnemy);
    }

    private void Update()
    {
        scoreNumber += Time.deltaTime;
        score.text = $"{(int)scoreNumber}";
    }

    private void OnEnable()
    {
        scoreNumber = 0;
        coinInGame = 0;
        coinNumber.text = $"{coinInGame}";
    }

    private void UpdateCurrentLevel(object obj)
    {
        level.text = (string)obj;
    }

    public void UpdateCoinText(object obj)
    {
        coinInGame += (int)obj;
        coinNumber.text = $"{coinInGame}";
    }

    void UpdateHealthText(object obj)
    {
        health.text = (string)obj;
    }

    void UnlockButton(object obj)
    {
        for(int i = 0; i < buttonManager.Count; i++)
        {
            if (i < (int)obj)
                buttonManager[i].interactable = true;
            else
                buttonManager[i].interactable = false;
        }
    }

    void changeProcess(object obj)
    {
        processLevel.value = (int)obj / (float)maxEnemy;
        processText.text = $"{(int)obj}/{maxEnemy}";
    }

    public void changeMaxEnemy(object obj)
    {
        maxEnemy = (int)obj;
        processText.text = $"0/{maxEnemy}";
        processLevel.value = 0;
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
        pausePopup.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pausePopup.SetActive(false);
    }
}
