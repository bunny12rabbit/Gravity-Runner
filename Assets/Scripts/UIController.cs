using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIController : MonoBehaviour
{
    [Inject] private GameConfig _config;
    [Inject] private ScoreSystem _scoreSystem;

    [Header("Score Text")] [SerializeField]
    private TextMeshProUGUI scoreTmp;

    [SerializeField] private TextMeshProUGUI highScoreTmp;

    [Header("Bonus Icon")] [SerializeField]
    private GameObject bonusIconGameObject;

    [SerializeField] private Image bonusIconType;
    [SerializeField] private Image bonusIconFill;
    [SerializeField] private TextMeshProUGUI bonusTimerTmp;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;

    private string _scoreText = "Score:";
    private string _highScoreText = "HighScore:";

    private void Start()
    {
        UpdateScore(0);
        SetBonusIconActive(false);
        SetGameOverPanelActive(false);
        SetPausePanelActive(false);

        if (!_config) return;
        _scoreText = _config.scoreText;
        _highScoreText = _config.bestScoreText;
    }

    public void UpdateScore(int score)
    {
        scoreTmp.SetText(_scoreText + " " + score);
    }

    public void SetBonusIconType(Sprite sprite)
    {
        bonusIconType.sprite = sprite;
    }

    public void UpdateBonusIconFill(float value)
    {
        bonusIconFill.fillAmount = value;
    }

    public void SetBonusIconActive(bool value)
    {
        bonusIconGameObject.SetActive(value);
    }

    public void UpdateTimer(float value)
    {
        bonusTimerTmp.SetText(value.ToString("0"));
    }

    public void SetPausePanelActive(bool value) => pausePanel.SetActive(value);

    public void SetGameOverPanelActive(bool value)
    {
        if (_scoreSystem != null)
        {
            highScoreTmp.SetText(_highScoreText + " " + _scoreSystem.HighScore);
        }

        gameOverPanel.SetActive(value);
    }
}