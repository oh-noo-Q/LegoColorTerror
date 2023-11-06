using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager 
{
    public const string PREFS_COIN = "coin";
    public const string PREFS_SOUND = "sound";
    public const string PREFS_MUSIC = "music";
    public const string PREFS_HIGH_SCORE = "high_score";
    public const string PREFS_RANKING_NAME = "ranking_names";
    public const string PREFS_AMOUNT_ENERGY = "amount_energy";
    public const string PREFS_TIME_REFILL_ENERGY = "time_refill_energy";
    public const string PREFS_COUNT_TIME_ENERGY = "count_time_energy";

    private const int DEFAULT_COIN = 0;
    public const int DEFAULT_AMOUNT_HIGH_SCORE = 3;
    public const int DEFAULT_MAX_ENERGY = 6;
    public static int Coin
    {
        get => PlayerPrefs.GetInt(PREFS_COIN, DEFAULT_COIN);
        set => PlayerPrefs.SetInt(PREFS_COIN, value);
    }

    public static bool Sound
    {
        get => PlayerPrefs.GetInt(PREFS_SOUND, 1) == 1;
        set => PlayerPrefs.SetInt(PREFS_SOUND, value ? 1 : 0);
    }

    public static bool Music
    {
        get => PlayerPrefs.GetInt(PREFS_MUSIC, 1) == 1;
        set => PlayerPrefs.SetInt(PREFS_MUSIC, value ? 1 : 0);
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

    public static float TimeCountEnergy
    {
        get => PlayerPrefs.GetFloat(PREFS_COUNT_TIME_ENERGY, 0);
        set => PlayerPrefs.SetFloat(PREFS_COUNT_TIME_ENERGY, value);
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

    private static List<string> _rankingNames;

    public static List<string> RankingNames
    {
        get
        {
            if (_rankingNames == null)
            {
                try
                {
                    _rankingNames = new List<string>();
                    string[] temp = PlayerPrefsElite.GetStringArray(PREFS_RANKING_NAME);
                    _rankingNames.AddRange(temp);

                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                    return _rankingNames;
                }
            }
            return _rankingNames;
        }
        set
        {
            _rankingNames = value;
            PlayerPrefsElite.SetStringArray(PREFS_RANKING_NAME, value.ToArray());
        }
    }

    public static void AddHighScore(int value, string name)
    {
        List<int> highScore = HighScore;
        List<string> rankingNames = RankingNames;
        bool haveName = false;
        for(int i = 0; i < rankingNames.Count; i++)
        {
            if(rankingNames[i] == name && highScore[i] < value)
            {
                highScore[i] = value;
                haveName = true;
                break;
            }
        }
        if (!haveName)
        {
            if (highScore.Count > 0)
            {
                for (int i = 0; i < DEFAULT_AMOUNT_HIGH_SCORE; i++)
                {
                    if (highScore[i] < value)
                    {
                        highScore[i] = value;
                        rankingNames[i] = name;
                        break;
                    }
                    else
                    {
                        if (i + 1 == highScore.Count && i + 1 <= DEFAULT_AMOUNT_HIGH_SCORE)
                        {
                            highScore.Add(value);
                            rankingNames.Add(name);
                            break;
                        }
                    }
                }
            }
            else
            {
                highScore.Add(value);
                rankingNames.Add(name);
            }
        }
        HighScore = highScore;
        RankingNames = rankingNames;
    }
}
