using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreSystem
{
    [Inject] private UIController _uIController;
    [Inject] private DataManager _dataManager;

    public int CurrentScore { get; private set; }
    public int HighScore { get; set; }
    

    public void AddScore(int value)
    {
        CurrentScore += value;
        _uIController.UpdateScore(CurrentScore);
    }

    public void UpdateHighScore()
    {
        if (CurrentScore <= HighScore) return;
        HighScore = CurrentScore;
        _dataManager.Save();
    }

    public void GetHighScore()
    {
        _dataManager.Load();
    }
}
