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

    [SerializeField] private Enemy[] _enemyArray;
    [SerializeField] private float _enemySpawnFrequency = 3.0f;
    private WaitForSeconds _enemySpawnDelay;
    private Coroutine _enemySpawnRoutine;

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
    private Coroutine _powerupSpawnRoutine;


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
        _enemySpawnRoutine = StartCoroutine(EnemySpawnRoutine());
        _powerupSpawnRoutine = StartCoroutine(PowerupSpawnRoutine());
    }

    private IEnumerator PowerupSpawnRoutine()
    {
        yield return _powerupSpawnDelay;
        while (!_gameOver)
        {
            Instantiate(PickRandomPowerup(), CreateRandomSpawnPoint(), Quaternion.identity,
                _powerupContainer);
            yield return _powerupSpawnDelay;
        }
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
                SpawnEnemy();
                yield return _enemySpawnDelay;
            }

            yield return StartCoroutine(PrepNextWaveRoutine());
        }
    }

    private IEnumerator PrepNextWaveRoutine()
    {
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
    }

    private void SpawnEnemy() =>
        Instantiate(PickRandomEnemy(), CreateRandomSpawnPoint(), Quaternion.identity, _enemyContainer);


    private Enemy PickRandomEnemy()
    {
        var randomChance = Random.Range(0f, 1f);
        var randomNumber = Random.Range(0, _enemyArray.Length);
        var enemy = _enemyArray[randomNumber];

        while (enemy.SpawnChance < randomChance)
        {
            randomNumber = Random.Range(0, _enemyArray.Length);
            enemy = _enemyArray[randomNumber];
        }

        return enemy;
    }

    private Powerup PickRandomPowerup()
    {
        var randomChance = Random.Range(0f, 1f);
        var randomNumber = Random.Range(0, _powerupArray.Length);
        var powerup = _powerupArray[randomNumber];
        
        while (powerup.SpawnChance < randomChance)
        {
            randomNumber = Random.Range(0, _powerupArray.Length);
            powerup = _powerupArray[randomNumber];
        }

        return powerup;
    }

    private void OnGameOver(bool state)
    {
        _gameOver = state;
        if (!_gameOver) return;
        StopCoroutine(_enemySpawnRoutine);
        StopCoroutine(_powerupSpawnRoutine);
    }
}