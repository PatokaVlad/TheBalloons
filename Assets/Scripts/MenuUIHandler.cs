using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private Text totalScore;
    [SerializeField]
    private Text bestScore;

    private void Start()
    {
        LoadBestPoints();
    }

    private void LoadBestPoints()
    {
        bestScore.text = SaveHandler.GetBestScore().ToString();
        totalScore.text = SaveHandler.GetTotalScore().ToString();
    }
}
