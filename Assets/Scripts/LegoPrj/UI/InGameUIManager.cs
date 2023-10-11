using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : UIContainer
{
    [SerializeField] Button pauseBtn;
    [SerializeField] List<HealthView> healthList;
    [SerializeField] Transform healthParent;
    public Text roundTxt;
    public Image processMeteorImg;
    public Button meteorActive;

    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnUpdateHealth, UpdateHealthTxt);
        EventDispatcher.Instance.RegisterListener(EventID.UpdateRoundEndLess, UpdateRound);
        EventDispatcher.Instance.RegisterListener(EventID.UpdateMeteorProcess, UpdateProcessMeteor);
        meteorActive.onClick.AddListener(ActiveMeteorOnclick);
        pauseBtn.onClick.AddListener(PauseBtnOnclick);
    }

    void UpdateHealthTxt(object obj)
    {
        int currentHealth = (int)obj;
        if(currentHealth > healthList.Count)
        {
            for (int i = 0; i < currentHealth; i++)
            {
                if (i < healthList.Count)
                {
                    healthList[i].Active(true);
                }
                else
                {
                    HealthView newHealth = Instantiate(healthList[0], healthParent);
                    healthList.Add(newHealth);
                    newHealth.Active(false);
                }
            }
        }
        else
        {
            for(int i = 0; i < healthList.Count; i++)
            {
                if(i < currentHealth)
                {
                    healthList[i].Active(true);
                }
                else
                {
                    healthList[i].Active(false);
                }
            }
        }
    }

    void UpdateRound(object obj)
    {
        roundTxt.text = $"Round {(int)obj}";
    }

    private void UpdateProcessMeteor(object obj)
    {
        processMeteorImg.fillAmount = (float)obj;
    }

    private void ActiveMeteorOnclick()
    {
        EventDispatcher.Instance.PostEvent(EventID.ActiveMeteor);
    }

    void PauseBtnOnclick()
    {
        PopupManager.instance.ShowPause();
        GameManager.Instance.PauseGame();
    }
}
