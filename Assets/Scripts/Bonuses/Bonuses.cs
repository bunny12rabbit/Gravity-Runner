using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bonuses : Item
{
    [Inject] internal GameConfig _config;

    internal int _timeToDestroy = 3;
    internal Collider2D _myColl;

    private void Start()
    {
        if (!_config) return;
        _timeToDestroy = _config.timeToDestroy;
        _myColl = GetComponent<Collider2D>();
        Destroy(gameObject, _timeToDestroy);
    }
}
