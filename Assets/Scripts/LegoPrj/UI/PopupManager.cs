using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;

    [SerializeField] GameObject dimpObj;
    public SettingController settingController;
    public PauseController pauseController;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowSettings()
    {
        dimpObj.SetActive(true);
        settingController.Show();
    }

    public void HideSettings()
    {
        dimpObj.SetActive(false);
        settingController.Hide();
    }

    public void ShowPause()
    {
        dimpObj.SetActive(true);
        pauseController.Show();
    }

    public void HidePause()
    {
        dimpObj.SetActive(false);
        pauseController.Hide();
    }
}
