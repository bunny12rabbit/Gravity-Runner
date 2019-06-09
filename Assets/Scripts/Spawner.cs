using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{

    [Inject] private GameConfig _config;
    [Inject] private Player _player;
    [Inject] private GameController _gameController;

    [SerializeField] private List<Bonuses> bonuses = new List<Bonuses>();

    private float _spawnDelay = 3f;
    private int _bonusesAmount = 3;

    // Use this for initialization
    private void Start()
    {
        if (_config)
        {
            _spawnDelay = _config.spawnDelay;
            _bonusesAmount = _config.bonusesAmount;
            if (bonuses.Count == 0)
            {
                bonuses = _config.bonuses;
            }
        }

        StartCoroutine(Spawn(_spawnDelay));
    }

    private IEnumerator Spawn(float time)
    {
        int count = 0;
        float offset = 0;

        yield return new WaitForSeconds(time);

        while (count < _bonusesAmount)
        {
            InstantiateItems(offset);
            offset = Random.Range(-3f, 4f);
            count++;
            yield return new WaitForSeconds(0.5f);
        }

        if (!_player.IsDead)
        {
            StartCoroutine(Spawn(_spawnDelay));
        }
    }

    private void InstantiateItems(float offset)
    {
        var position = transform.position;

        if (_gameController.IsJumpActivated || _gameController.IsDoubleScoreActivated)
        {
            
            Instantiate(bonuses.Find(x => x.GetComponent<Coins>()),
                new Vector3(position.x, position.y + offset, 0), transform.rotation);
        }
        else
        {
            Instantiate(MyMethods<Bonuses>.GetRandomItem(bonuses, _player.transform),
                new Vector3(position.x, position.y + offset, 0), transform.rotation);
        }
    }
}
