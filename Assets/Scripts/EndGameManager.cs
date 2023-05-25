using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] Text scoreGame;
    [SerializeField] Text coinCollect;
    [SerializeField] Text coinAdCollect;

    int coinInGame;

    public void Show(int score, int coin)
    {
        coinInGame = coin;
        scoreGame.text = $"{score}";
        coinCollect.text = $"+{coin}";
        coinAdCollect.text = $"+{coin * 5}";
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Collect()
    {
        PlayerPrefsManager.Coin += coinInGame;
        UIManager.Instance.RetryButton();
    }
}
