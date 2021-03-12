using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    private SoundHandler _soundHandler;

    [SerializeField]
    private Text currentScore;
    [SerializeField]
    private Text bestScoreText;

    private int bestScore;
    private bool makeNewBest = false;

    private void Start()
    {
        _soundHandler = FindObjectOfType<SoundHandler>();

        bestScore = SaveHandler.GetBestScore(SceneManager.GetActiveScene().path);
        bestScoreText.text = bestScore.ToString();
    }

    public void UpdatePonitsCount(int count)
    {
        currentScore.text = count.ToString();

        UpdateBestScore(count);
    }

    private void UpdateBestScore(int count)
    {
        if(bestScore < count)
        {
            bestScoreText.text = count.ToString();

            if (!makeNewBest)
            {
                _soundHandler.PlayClip(_soundHandler.BitTheBestClip);
                makeNewBest = true;
            }
        }
    }
}
