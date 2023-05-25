using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    public List<StatusTab> tabs;
    public GameObject panel;
    public ScoreRow rowPrefab;

    public void ChangeTab()
    {

    }

    private void OnEnable()
    {
        foreach (Transform child in panel.transform)
            Destroy(child.gameObject);
        for(int i = 0; i < PlayerPrefsManager.DEFAULT_AMOUNT_HIGH_SCORE; i++)
        {
            ScoreRow row = Instantiate(rowPrefab, panel.transform);
            if(PlayerPrefsManager.HighScore[i] > 0)
                row.Init($"{i + 1}. {PlayerPrefsManager.HighScore[i]}");
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
