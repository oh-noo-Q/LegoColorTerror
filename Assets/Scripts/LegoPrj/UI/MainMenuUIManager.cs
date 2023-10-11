using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : UIContainer
{
    public Button startBtn, settingBtn;

    private void Awake()
    {
        startBtn.onClick.AddListener(StartOnClick);
        settingBtn.onClick.AddListener(SettingsOnClick);
    }

    void StartOnClick()
    {
        GameManager.Instance.StartEndless();
        Hide();
    }

    void SettingsOnClick()
    {
        PopupManager.instance.ShowSettings();
    }
}
