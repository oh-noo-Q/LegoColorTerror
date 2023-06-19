using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILegoManager : MonoBehaviour
{
    public static UILegoManager Instance;

    [SerializeField] MainMenuUIManager mainMenu;
    [SerializeField] InGameUIManager inGameUI;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void ShowMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
    }
}
