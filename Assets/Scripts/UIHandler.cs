using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private Text pointsCount;

    [SerializeField]
    private bool loadBest = false;

    private void Start()
    {
        if (loadBest)
            LoadBestPoints();
    }

    public void UpdatePoitsCount(string count)
    {
        pointsCount.text = count;
    }

    public void LoadBestPoints()
    {
        if (ES3.KeyExists("Score"))
            pointsCount.text = ES3.Load<int>("Score").ToString();
    }
}
