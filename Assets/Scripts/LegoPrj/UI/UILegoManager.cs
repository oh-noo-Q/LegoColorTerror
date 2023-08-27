using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILegoManager : MonoBehaviour
{
    public static UILegoManager Instance;

    public MainMenuUIManager mainMenu;
    public InGameUIManager inGameUI;
    public EndGameUIManager endGame;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        Invoke("ShowMainMenu", 0.2f);
    }

    public void ShowInGameUI()
    {
        inGameUI.Show();
        mainMenu.Hide();
        SoundManager.instance.PlayMusic(SoundType.BGIngameMusic);
    }
    public void ShowMainMenu()
    {
        mainMenu.Show();
        inGameUI.Hide();
        SoundManager.instance.PlayMusic(SoundType.BGMenuMusic);
    }

    public void ShowEndGame()
    {
        endGame.Show();
    }
}
