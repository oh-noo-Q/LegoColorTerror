using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUIManager : MonoBehaviour
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
        gameObject.SetActive(false);
        GameManager.Instance.StartEndless();
    }

    public void MainMenuOnclick()
    {
        gameObject.SetActive(false);
        UILegoManager.Instance.ShowMainMenu();
    }

    public void ReviveOnclick()
    {
        gameObject.SetActive(false);
        GameManager.Instance.RevivePlayer();
    }
}
