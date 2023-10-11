using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILegoManager : MonoBehaviour
{
    public static UILegoManager Instance;

    public UIContainer mainMenu, inGameUI, endGame;

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
        SoundManager.instance.PlayMusic(SoundName.BGIngameMusic);
    }
    public void ShowMainMenu()
    {
        mainMenu.Show();
        inGameUI.Hide();
        SoundManager.instance.PlayMusic(SoundName.BGMenuMusic);
    }

    public void ShowEndGame()
    {
        endGame.Show();
    }
}
