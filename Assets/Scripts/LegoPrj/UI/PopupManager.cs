using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;

    [SerializeField] GameObject dimpObj;
    public SettingController settingController;
    public PauseController pauseController;
    public NotEnoughtEnergyPopup notEnoughEnergy;

    private float timeToHideNoti = 1f;
    private Coroutine hideNotiCoroutine;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowSettings()
    {
        ActiveDimp(true);
        settingController.Show();
    }

    public void HideSettings()
    {
        ActiveDimp(false);
        settingController.Hide();
    }

    public void ShowPause()
    {
        ActiveDimp(true);
        pauseController.Show();
    }

    public void HidePause()
    {
        ActiveDimp(false);
        pauseController.Hide();
    }

    public void ShowNotEnoughEnergy()
    {
        notEnoughEnergy.Show();
        if (hideNotiCoroutine != null)
        {
            StopCoroutine(hideNotiCoroutine);
        }
        hideNotiCoroutine = StartCoroutine(DelayHidePopup(notEnoughEnergy));
    }

    void ActiveDimp(bool active)
    {
        dimpObj.SetActive(active);
    }


    IEnumerator DelayHidePopup(UIContainer popup)
    {
        yield return new WaitForSeconds(timeToHideNoti);
        popup.Hide();
    }
}
