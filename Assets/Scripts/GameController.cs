using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public delegate void OnEventAction();

public class GameController : MonoBehaviour
{
    [Inject] private GameConfig _config;
    [Inject] private UIController _uiController;
    [Inject] private ScoreSystem _scoreSystem;

    private OnEventAction _onEventAction;

    public bool IsDoubleScoreActivated { get; private set; }
    public bool IsJumpActivated { get; private set; }
    public bool IsPaused { get; private set; }

    private void Start()
    {
        _scoreSystem.GetHighScore();
    }

    public void ActivateBonus(Bonuses bonus)
    {
        switch (bonus)
        {
            case DoubleCoins doubleCoins:
                IsDoubleScoreActivated = true;
                _uiController.SetBonusIconActive(true);
                _uiController.SetBonusIconType(_config.doubleCoinsScoreIcon);
                StartCoroutine(RunBonusDuration(_config.doubleScoreDuration));
                StartCoroutine(RunTimer(_config.doubleScoreDuration));
                _onEventAction = DeactivateJumpBonus;
                break;
            case JumpBonus jumpBonus:
                IsJumpActivated = true;
                _uiController.SetBonusIconActive(true);
                _uiController.SetBonusIconType(_config.jumpModeIcon);
                StartCoroutine(RunBonusDuration(_config.jumpModeDuration));
                StartCoroutine(RunTimer(_config.jumpModeDuration));
                _onEventAction = DeactivateJumpBonus;
                break;
        }
    }

    IEnumerator RunBonusDuration(float duration)
    {
        float value = 1;
        float share = 1 / (duration * 10);

        while (value > 0)
        {
            value -= share;
            _uiController.UpdateBonusIconFill(value);
            yield return new WaitForSeconds(0.1f);
        }
        _onEventAction?.Invoke();
    }

    IEnumerator RunTimer(float duration)
    {
        while (duration > 0)
        {
            _uiController.UpdateTimer(duration);
            duration--;

            yield return new WaitForSeconds(1);
        }
    }

    private void DeactivateJumpBonus()
    {
        IsJumpActivated = false;
        _uiController.SetBonusIconActive(false);
    }

    private void DeactivateDoubleCoinsBonus()
    {
        IsDoubleScoreActivated = false;
        _uiController.SetBonusIconActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PauseGame()
    {
        IsPaused = !IsPaused;
        Time.timeScale = Time.timeScale < 1 ? 1 : 0;
        _uiController.SetPausePanelActive(IsPaused);
    }

    public void GameOver()
    {
        foreach (var procedureLevelGenerator in FindObjectsOfType<ProcedureLevelGenerator>())
        {
            procedureLevelGenerator.enabled = false;
        }

        _scoreSystem.UpdateHighScore();
        
        _uiController.SetGameOverPanelActive(true);
    }
}
