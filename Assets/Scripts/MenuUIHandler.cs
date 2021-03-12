using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private Text totalScore;
    [Header("Почерёдность как в сцен в Build Settings")]
    [SerializeField]
    private List<Text> bestScores = new List<Text>();

    private void Start()
    {
        LoadBestPoints();
    }

    private void LoadBestPoints()
    {
        for (int i = 0; i < bestScores.Count; i++) 
        {
            bestScores[i].text += SaveHandler.GetBestScore(SceneUtility.GetScenePathByBuildIndex(i+1)).ToString();
        }

        totalScore.text = SaveHandler.GetTotalScore().ToString();
    }
}
