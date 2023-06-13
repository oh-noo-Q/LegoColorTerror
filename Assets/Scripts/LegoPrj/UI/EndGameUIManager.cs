using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUIManager : MonoBehaviour
{
    [SerializeField] Button restart, mainMenu;

    private void Awake()
    {
        restart.onClick.AddListener(RestartOnclick);
        mainMenu.onClick.AddListener(MainMenuOnclick);
    }

    public void RestartOnclick()
    {
        gameObject.SetActive(false);
    }

    public void MainMenuOnclick()
    {
        gameObject.SetActive(false);
    }
}
