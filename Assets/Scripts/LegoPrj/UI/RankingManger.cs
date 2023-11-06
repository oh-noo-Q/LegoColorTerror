using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManger : MonoBehaviour
{
    public List<Text> rankTxt;
    public List<Color> colorRank;

    private void Awake()
    {
        for(int i = 0; i < rankTxt.Count; i++)
        {
            rankTxt[i].gameObject.SetActive(false);
            rankTxt[i].color = colorRank[i];
        }
    }

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        for (int i = 0; i < rankTxt.Count; i++)
        {
            rankTxt[i].gameObject.SetActive(false);
            rankTxt[i].color = colorRank[i];
        }
    }

    public void UpdateRanking()
    {
        for(int i = 0; i < PlayerPrefsManager.HighScore.Count; i++)
        {
            rankTxt[i].gameObject.SetActive(true);
            rankTxt[i].text = $"{i + 1}st: {PlayerPrefsManager.RankingNames[i]}";
        }
    }
}
