using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    [SerializeField] Text coin;
    [SerializeField] Text bestScore;
    [SerializeField] Text energy;

    private void OnEnable()
    {
        coin.text = $"{PlayerPrefsManager.Coin}";
        if (PlayerPrefsManager.HighScore.Count != 0)
        {
            bestScore.text = $"{PlayerPrefsManager.HighScore[0]}";
        }
        else
            bestScore.text = "0";
        
    }
}
