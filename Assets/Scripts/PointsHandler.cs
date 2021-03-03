using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsHandler : MonoBehaviour
{
    private UIHandler _uiHandler;

    private int pointsEarned = 0;

    public int PointsEarned { get => pointsEarned; }

    private void Awake()
    {
        _uiHandler = FindObjectOfType<UIHandler>();
    }

    public void IncreaseEarnedPointsCount()
    {
        pointsEarned++;
        _uiHandler.UpdatePoitsCount(pointsEarned.ToString());
    }
}
