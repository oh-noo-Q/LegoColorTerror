using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager 
{
    public const string PREFS_COIN = "coin";
    public const string PREFS_HIGH_SCORE = "high_score";
    public const string PREFS_AMOUNT_ENERGY = "amount_energy";
    public const string PREFS_TIME_REFILL_ENERGY = "time_refill_energy";

    private const int DEFAULT_COIN = 0;
    public const int DEFAULT_AMOUNT_HIGH_SCORE = 5;
    public const int DEFAULT_MAX_ENERGY = 6;
    public static int Coin
    {
        get => PlayerPrefs.GetInt(PREFS_COIN, DEFAULT_COIN);
        set => PlayerPrefs.SetInt(PREFS_COIN, value);
    }

    public static int AmountEnergy
    {
        get => PlayerPrefs.GetInt(PREFS_AMOUNT_ENERGY, DEFAULT_MAX_ENERGY);
        set => PlayerPrefs.SetInt(PREFS_AMOUNT_ENERGY, value);
    }

    public static string TimeRefillEnergy
    {
        get => PlayerPrefs.GetString(PREFS_TIME_REFILL_ENERGY, "");
        set => PlayerPrefs.SetString(PREFS_TIME_REFILL_ENERGY, value);
    }

    private static List<int> _highScore;

    public static List<int> HighScore
    {
        get
        {
            if (_highScore == null)
            {
                try
                {
                    _highScore = new List<int>();
                    int[] temp = PlayerPrefsElite.GetIntArray(PREFS_HIGH_SCORE);
                    _highScore.AddRange(temp);

                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                    return _highScore;
                }
            }
            return _highScore;
        }
        set
        {
            _highScore = value;
            PlayerPrefsElite.SetIntArray(PREFS_HIGH_SCORE, value.ToArray());
        }
    }

    public static void AddHighScore(int value)
    {
        List<int> highScore = HighScore;
        if (highScore.Count < DEFAULT_AMOUNT_HIGH_SCORE)
        {
            highScore.Add(value);
            for (int i = 0; i < highScore.Count - 1; i++)
            {
                if (highScore[i] < value)
                {
                    highScore.Insert(i, value);
                    highScore.Remove(highScore.Count - 1);
                    HighScore = highScore;
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < DEFAULT_AMOUNT_HIGH_SCORE; i++)
            {
                if (highScore[i] < value)
                {
                    highScore.Insert(i, value);
                    highScore.Remove(DEFAULT_AMOUNT_HIGH_SCORE);
                    break;
                }
            }
            HighScore = highScore;
        }
    }
}
