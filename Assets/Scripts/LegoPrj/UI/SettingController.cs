using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : UIContainer
{
    [SerializeField] ToggleViewController musicToggle, soundToggle;
    [SerializeField] Button musicBtn, soundBtn, closeBtn, exitBtn;

    [SerializeField] bool musicStatus, soundStatus;

    private void Awake()
    {
        musicBtn.onClick.AddListener(MusicBtnOnclick);
        soundBtn.onClick.AddListener(SoundBtnOnclick);
        if(closeBtn != null) closeBtn.onClick.AddListener(CloseBtnOnclick);
        if(exitBtn != null) exitBtn.onClick.AddListener(CloseBtnOnclick);
        
    }

    private void OnEnable()
    {
        musicStatus = PlayerPrefsManager.Music;
        soundStatus = PlayerPrefsManager.Sound;
        musicToggle.Active(musicStatus);
        soundToggle.Active(soundStatus);
    }

    void MusicBtnOnclick()
    {
        musicStatus = !musicStatus;
        PlayerPrefsManager.Music = musicStatus;
        musicToggle.SetStatus(musicStatus);
        EventDispatcher.Instance.PostEvent(EventID.OnMusicChangeValue, (musicStatus) ? 1f : 0f);
        musicBtn.enabled = false;
        Invoke("EnableMusic", 0.4f);
    }

    void SoundBtnOnclick()
    {
        soundStatus = !soundStatus;
        PlayerPrefsManager.Sound = soundStatus;
        soundToggle.SetStatus(soundStatus);
        EventDispatcher.Instance.PostEvent(EventID.OnSoundChangeValue, (soundStatus) ? 1f : 0f);
        soundBtn.enabled = false;
        Invoke("EnableSound", 0.4f);
    }

    void CloseBtnOnclick()
    {
        if(PopupManager.instance.settingController.isActiveAndEnabled)
            PopupManager.instance.HideSettings();
        else if(PopupManager.instance.pauseController.isActiveAndEnabled)
        {
            PopupManager.instance.HidePause();
            GameManager.Instance.ResumeGame();
            GameManager.Instance.ExitGame();
            UILegoManager.Instance.ShowMainMenu();
        }
    }

    void EnableMusic()
    {
        musicBtn.enabled = true;
    }

    void EnableSound()
    {
        soundBtn.enabled = true;
    }
}
