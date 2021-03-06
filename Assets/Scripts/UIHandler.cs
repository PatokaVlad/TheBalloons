using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private Text currentScore;
    [SerializeField]
    private Text bestScore;

    private void Start()
    {
        bestScore.text = SaveHandler.GetBestScore().ToString();
    }

    public void UpdatePonitsCount(int count)
    {
        currentScore.text = count.ToString();

        UpdateBestScore(count);
    }

    private void UpdateBestScore(int count)
    {
        if(SaveHandler.GetBestScore() < count)
        {
            bestScore.text = count.ToString();
        }
    }
}
