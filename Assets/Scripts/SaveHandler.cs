using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveHandler
{
    private const string bestScoreKey = "BestScore";
    private const string totalScoreKey = "TotalScore";

    public static void SaveBestScore(int score)
    {
        if (PlayerPrefs.HasKey(bestScoreKey))
        {
            if (PlayerPrefs.GetInt(bestScoreKey) < score)
            {
                PlayerPrefs.SetInt(bestScoreKey, score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(bestScoreKey, score);
        }

        SaveTotalScore(score);
    }

    public static void SaveTotalScore(int score)
    {
        if (PlayerPrefs.HasKey(totalScoreKey))
        {
            PlayerPrefs.SetInt(totalScoreKey, PlayerPrefs.GetInt(totalScoreKey) + score);
        }
        else
        {
            PlayerPrefs.SetInt(totalScoreKey, score);
        }
    }

    public static int GetBestScore()
    {
        if (PlayerPrefs.HasKey(bestScoreKey))
        {
            return PlayerPrefs.GetInt(bestScoreKey);
        }
        else
        {
            return 0;
        }
    }

    public static int GetTotalScore()
    {
        if (PlayerPrefs.HasKey(totalScoreKey))
        {
            return PlayerPrefs.GetInt(totalScoreKey);
        }
        else
        {
            return 0;
        }
    }
}
