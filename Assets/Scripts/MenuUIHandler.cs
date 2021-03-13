using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private List<Button> playButtons = new List<Button>();

    [SerializeField]
    private Text totalScore;
    [Header("Почерёдность как в сцен в Build Settings")]
    [SerializeField]
    private List<Text> bestScores = new List<Text>();

    private void Start()
    {
        LoadBestPoints();
        ActivateButtons();
    }

    private void ActivateButtons()
    {
        for (int i = 1; i < playButtons.Count; i++)
        {
            int total = System.Convert.ToInt32(totalScore.text);
            int needed = i * 200;
            if (total < 1) 
            {

                playButtons[i].GetComponent<Button>().enabled = false;
                bestScores[i].text = "Earn " + (needed - total).ToString() + " points!";
            }
        }
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
