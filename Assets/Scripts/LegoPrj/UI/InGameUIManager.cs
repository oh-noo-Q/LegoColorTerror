using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public Text health;

    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnUpdateHealth, UpdateHealthTxt);
    }

    void UpdateHealthTxt(object obj)
    {
        health.text = ((int)obj).ToString();
    }
}
