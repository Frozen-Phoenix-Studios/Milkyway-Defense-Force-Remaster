using System;
using System.Collections;
using FrozenPhoenixStudiosUtilities;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public event Action<float> OnWaveComplete;

    [Header("Enemy Values")] [SerializeField]
    private Transform _enemyContainer;

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _enemySpawnFrequency = 3.0f;
    private WaitForSeconds _enemySpawnDelay;


    [Header("Spawn Range values")] [SerializeField]
    private float _xMinSpawn = -8.0f;

    [SerializeField] private float _xMaxSpawn = 8.0f;
    [SerializeField] private float _spawnHeight = 9.0f;
    [SerializeField] private int _enemiesPerWave = 3;
    [SerializeField] private int _extraEnemiesPerWave;

    [SerializeField]
    [Tooltip(
        "How many seconds per enemy does the player get before the next wave starts after the last enemy is spawned")]
    private float _delayBetweenWavesMultiplier = 0.25f;

    private float _delayBetweenWaves => _enemiesPerWave * _delayBetweenWavesMultiplier;
    private int _currentWave;


    [Header("Powerup Values")] [SerializeField]
    private Transform _powerupContainer;

    [SerializeField] private Powerup[] _powerupArray;
    [SerializeField] private float _powerupSpawnFrequency = 3.0f;
    private WaitForSeconds _powerupSpawnDelay;

    private bool _gameOver;
    private int _index;


    private void OnEnable()
    {
        GameStateManager.OnGameOver += OnGameOver;
        GameStateManager.OnGameStart += StartSpawning;
    }

    private void OnDisable()
    {
        GameStateManager.OnGameOver -= OnGameOver;
        GameStateManager.OnGameStart -= StartSpawning;
    }

    private void Start()
    {
        _gameOver = false;
        _enemySpawnDelay = new WaitForSeconds(_enemySpawnFrequency);
        _powerupSpawnDelay = new WaitForSeconds(_powerupSpawnFrequency);
    }

    private void StartSpawning()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    private IEnumerator PowerupSpawnRoutine()
    {
        yield return _powerupSpawnDelay;
        while (!_gameOver)
        {
            Instantiate(ReturnRandomPowerup().gameObject, CreateRandomSpawnPoint(), Quaternion.identity,
                _powerupContainer);
            yield return _powerupSpawnDelay;
        }
    }

    private Powerup ReturnRandomPowerup()
    {
        var max = _powerupArray.Length;
        return _powerupArray[Random.Range(0, max)];
    }

    private Vector2 CreateRandomSpawnPoint()
    {
        var x = Random.Range(_xMinSpawn, _xMaxSpawn);
        var y = _spawnHeight;

        return new Vector2(x, y);
    }

    private IEnumerator EnemySpawnRoutine()
    {
        while (!_gameOver)
        {
            for (int i = 0; i < _enemiesPerWave; i++)
            {
                Debug.Log($"Index: {i+1}/{_enemiesPerWave}");
                SpawnEnemy();
                yield return _enemySpawnDelay;
            }
            yield return StartCoroutine(PrepNextWaveRoutine());
        }
    }

    private IEnumerator PrepNextWaveRoutine()
    {
        Debug.Log("Prepping next wave");
        SetWaveComplete();
        yield return new WaitForSeconds(_delayBetweenWaves);
        IncreaseWaveIndex();
    }

    private void SetWaveComplete()
    {
        OnWaveComplete?.Invoke(_delayBetweenWaves);
    }

    private void IncreaseWaveIndex()
    {
        _enemiesPerWave += _extraEnemiesPerWave;
        _currentWave++;
        Debug.Log($"Increasing wave index to {_currentWave}");
    }

    private void SpawnEnemy() =>
        Instantiate(_enemyPrefab, CreateRandomSpawnPoint(), Quaternion.identity, _enemyContainer);

    private void OnGameOver(bool state) => _gameOver = state;
}