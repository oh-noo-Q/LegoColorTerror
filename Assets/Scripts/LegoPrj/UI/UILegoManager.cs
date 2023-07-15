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
    }

    public void ShowInGameUI()
    {
        inGameUI.Show();
    }
    public void ShowMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
    }

    public void ShowEndGame()
    {
        endGame.gameObject.SetActive(true);
    }
}
