using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUIManager : UIContainer
{
    [SerializeField] Button restart, mainMenu, reviveBtn;

    private void Awake()
    {
        restart.onClick.AddListener(RestartOnclick);
        mainMenu.onClick.AddListener(MainMenuOnclick);
        reviveBtn.onClick.AddListener(ReviveOnclick);
    }

    public void RestartOnclick()
    {
        Hide();
        GameManager.Instance.StartEndless();
    }

    public void MainMenuOnclick()
    {
        Hide();
        UILegoManager.Instance.ShowMainMenu();
    }

    public void ReviveOnclick()
    {
        Hide();
        GameManager.Instance.RevivePlayer();
    }
}
