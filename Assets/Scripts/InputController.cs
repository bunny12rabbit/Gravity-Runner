using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputController : MonoBehaviour
{
    [Inject] private Player _player;
    [Inject] private GameController _gameController;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && !_player.IsMoveNow)
        {
            _player.IsMoveNow = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.Up();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameController.PauseGame();
        }
    }
}
