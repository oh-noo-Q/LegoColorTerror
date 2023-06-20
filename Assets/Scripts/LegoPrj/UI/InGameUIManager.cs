using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public Text health;
    public Text roundTxt;
    public Text blockTxt;

    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnUpdateHealth, UpdateHealthTxt);
        EventDispatcher.Instance.RegisterListener(EventID.UpdateRoundEndLess, UpdateRound);
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
}
