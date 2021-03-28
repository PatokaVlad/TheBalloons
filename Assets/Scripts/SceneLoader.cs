using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static int lastLevelIndex = 1;

    public void LoadLevelScene(int index)
    {
        if (index >= 0 && index <= SceneManager.sceneCountInBuildSettings)
        {
            if (index != 0)
            {
                lastLevelIndex = index;
            }

            SceneManager.LoadScene(index, LoadSceneMode.Single);
        }
    }

    public void LoadNextScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings >= sceneIndex)
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void LoadPreviousScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (sceneIndex > 0)
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
