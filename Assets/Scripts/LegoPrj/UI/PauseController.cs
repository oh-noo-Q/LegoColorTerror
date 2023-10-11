using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : UIContainer
{
    [SerializeField] Button resumeBtn, restartBtn, closeBtn;


    private void Awake()
    {
        resumeBtn.onClick.AddListener(ResumeBtnOnclick);
        closeBtn.onClick.AddListener(ResumeBtnOnclick);
        restartBtn.onClick.AddListener(RestartBtnOnclick);
    }

    void ResumeBtnOnclick()
    {
        GameManager.Instance.ResumeGame();
        PopupManager.instance.HidePause();
    }

    void RestartBtnOnclick()
    {
        GameManager.Instance.StartEndless();
        PopupManager.instance.HidePause();
    }

    void CloseBtnOnclick()
    {
        
    }
}
