using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public Button startBtn;

    private void Awake()
    {
        startBtn.onClick.AddListener(StartOnClick);
    }

    void StartOnClick()
    {
        GameManager.Instance.StartEndless();
        gameObject.SetActive(false);
    }
}
