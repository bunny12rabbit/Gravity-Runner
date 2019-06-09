using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Coins : Bonuses
{
    [Inject] private ScoreSystem _scoreSystem;
    [Inject] private GameController _gameController;

    private int _score = 10;

    private void Start()
    {
        if (_config)
        {
            _score = _config.coinScore;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (!target.CompareTag("Player")) return;
        if (_gameController.IsDoubleScoreActivated)
        {
            _scoreSystem.AddScore(_score * 2);
        }
        else
        {
            _scoreSystem.AddScore(_score);
        }

        Destroy(gameObject);
    }
}
