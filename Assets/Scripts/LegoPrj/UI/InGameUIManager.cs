using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : UIContainer
{
    public Text health;
    public Text roundTxt;
    public Text blockTxt;
    public Image processMeteorImg;
    public Button meteorActive;

    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnUpdateHealth, UpdateHealthTxt);
        EventDispatcher.Instance.RegisterListener(EventID.UpdateRoundEndLess, UpdateRound);
        EventDispatcher.Instance.RegisterListener(EventID.UpdateMeteorProcess, UpdateProcessMeteor);
        meteorActive.onClick.AddListener(ActiveMeteorOnclick);
    }

    void UpdateHealthTxt(object obj)
    {
        health.text = ((int)obj).ToString();
    }

    void UpdateRound(object obj)
    {
        roundTxt.text = $"Round {(int)obj}";
    }

    public void SetBlockText(int amount)
    {
        blockTxt.text = $"Block: {amount}";
    }

    private void UpdateProcessMeteor(object obj)
    {
        processMeteorImg.fillAmount = (float)obj;
    }

    private void ActiveMeteorOnclick()
    {
        EventDispatcher.Instance.PostEvent(EventID.ActiveMeteor);
    }
}
