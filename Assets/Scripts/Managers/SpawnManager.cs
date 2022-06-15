using System;
using System.Collections;
using FrozenPhoenixStudiosUtilities;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [Header("Enemy Values")]
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _enemySpawnFrequency = 3.0f;
    private WaitForSeconds _enemySpawnDelay;
    
    [Header("Spawn Range values")]
    [SerializeField] private float _xMinSpawn = -8.0f;
    [SerializeField] private float _xMaxSpawn = 8.0f;
    [SerializeField] private float _spawnHeight = 9.0f;


    private WaitForSeconds _powerupSpawnDelay;

    [Header("Powerup Values")]
    [SerializeField] private float _powerupSpawnFrequency = 3.0f;

    [SerializeField] private bool _gameOver;


    private void Start()
    {
        _gameOver = false;
        _enemySpawnDelay = new WaitForSeconds(_enemySpawnFrequency);
        _powerupSpawnDelay = new WaitForSeconds(_powerupSpawnFrequency);
        StartCoroutine(SpawnRoutine());
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += OnGameOver; 
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= OnGameOver; 

    }

    private Vector2 CreateRandomSpawnPoint()
    {
        var x = Random.Range(_xMinSpawn, _xMaxSpawn);
        var y = _spawnHeight;

        return new Vector2(x, y);
    }

    private IEnumerator SpawnRoutine()
    {
        yield return _enemySpawnDelay;
        while (!_gameOver)
        {
            SpawnEnemy();
            yield return _enemySpawnDelay;
        }
    }
    private void SpawnEnemy() => Instantiate(_enemyPrefab, CreateRandomSpawnPoint(), Quaternion.identity, _enemyContainer);

    private void OnGameOver() => _gameOver = true;
}