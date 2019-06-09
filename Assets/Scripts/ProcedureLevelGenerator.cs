using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ProcedureLevelGenerator : MonoBehaviour
{
    [Inject] private GameConfig _config;

    [SerializeField] private List<Chunk> chunks = new List<Chunk>();
    [SerializeField] private Transform player;
    [SerializeField] private int platformsOnSceen;
    
    private Chunk _nextChunk;
    private List<Chunk> _spawnedChunks = new List<Chunk>();
    private float _upPosition = 11.78f;
    private bool _isEndWithSpaceNull;

    private void Awake()
    {
        if (platformsOnSceen > 0) return;
        platformsOnSceen = _config.platformsOnScreen;
    }

    private void Start()
    {
        if (chunks.Count == 0)
        {
            Debug.LogError("Chunks list is empty!");
            gameObject.SetActive(false);
            return;
        }

        if (player == null)
        {
            player = FindObjectOfType<Player>().transform;
        }

        _nextChunk = Instantiate(GetRandomChunk(), transform.position, transform.rotation);
        _isEndWithSpaceNull = _nextChunk.EndWithSpace == null;
        _spawnedChunks.Add(_nextChunk);
    }

    private void Update()
    {
        if (player.position.x > _spawnedChunks[_spawnedChunks.Count - 1].transform.position.x - 30)
        {
            GenerateNextPlatform();
        }
    }

    private void GenerateNextPlatform()
    {
            _nextChunk = Instantiate(GetRandomChunk());
            PlaceChunkInRandomPosition();
            //_nextChunk.transform.position =
            //    _spawnedChunks[_spawnedChunks.Count - 1].End.position - _nextChunk.Begin.localPosition;
            _spawnedChunks.Add(_nextChunk);

            if (_spawnedChunks.Count >= platformsOnSceen)
            {
                Destroy(_spawnedChunks[0].gameObject);
                _spawnedChunks.RemoveAt(0);
            }
    }

    private void PlaceChunkInRandomPosition()
    {
        if (_isEndWithSpaceNull)
        {
            _nextChunk.transform.position =
                _spawnedChunks[_spawnedChunks.Count - 1].End.position - _nextChunk.Begin.localPosition;
            return;
        }

        List<Vector3> tempPositions = new List<Vector3>
        {
            new Vector3(_spawnedChunks[_spawnedChunks.Count - 1].GetComponent<Collider2D>().bounds.max.x,
                _spawnedChunks[_spawnedChunks.Count - 1].transform.position.y,
                35),

            _spawnedChunks[_spawnedChunks.Count - 1].EndWithSpace.position - _nextChunk.Begin.localPosition
        };


        var value = Random.Range(0, tempPositions.Count);

        _nextChunk.transform.position = tempPositions[value];
    }

    private Chunk GetRandomChunk()
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < chunks.Count; i++)
        {
            chances.Add(chunks[i].ChanceFromDistance.Evaluate(player.transform.position.x));
        }

        float value = Random.Range(0, chances.Sum());
        float sum = 0;

        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value < sum)
            {
                return chunks[i];
            }
        }

        return chunks[chunks.Count - 1];
    }
}
