using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public Text health;
    public Text roundTxt;

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
}
