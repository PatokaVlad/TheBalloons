using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveHandler
{
    private const string bestScoreKey = "BestScore";
    private const string totalScoreKey = "TotalScore";

    public static void SaveBestScore(int score)
    {
        string sceneName = bestScoreKey + SceneManager.GetActiveScene().path;

        if (PlayerPrefs.HasKey(sceneName))
        {
            if (PlayerPrefs.GetInt(sceneName) < score)
            {
                PlayerPrefs.SetInt(sceneName, score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(sceneName, score);
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

    public static int GetBestScore(string name)
    {
        string sceneName = bestScoreKey + name;

        if (PlayerPrefs.HasKey(sceneName))
        {
            return PlayerPrefs.GetInt(sceneName);
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
