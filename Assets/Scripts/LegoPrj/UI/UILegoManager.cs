using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILegoManager : MonoBehaviour
{
    public static UILegoManager Instance;

    [SerializeField] MainMenuUIManager mainMenu;
    [SerializeField] InGameUIManager inGameUI;

    public void ShowMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
    }
}
