using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class JumpBonus : Bonuses
{
    [Inject] private GameController _gameController;

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (!target.CompareTag("Player") | _gameController.IsJumpActivated) return;
        _gameController.ActivateBonus(this);
        Destroy(gameObject);
    }
}
