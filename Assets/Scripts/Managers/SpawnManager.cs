using System.Collections;
using FrozenPhoenixStudiosUtilities;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _xMinSpawn = -8.0f;
    [SerializeField] private float _xMaxSpawn = 8.0f;
    [SerializeField] private float _spawnHeight = 9.0f;
    [SerializeField] private float _spawnSpeed = 3.0f;
    [SerializeField] private bool _gameOver;
    private WaitForSeconds _spawnDelay;

    private void Start()
    {
        _spawnDelay = new WaitForSeconds(_spawnSpeed);
        StartCoroutine(SpawnRoutine());
    }
    //spawn enemies every 5 seconds

    private Vector2 CreateRandomSpawnPoint()
    {
        var x = Random.Range(_xMinSpawn, _xMaxSpawn);
        var y = _spawnHeight;

        return new Vector2(x, y);
    }

    private IEnumerator SpawnRoutine()
    {
        yield return _spawnDelay;
        while (!_gameOver)
        {
            SpawnEnemy();
            yield return _spawnDelay;
        }
    }
    private void SpawnEnemy() => Instantiate(_enemyPrefab, CreateRandomSpawnPoint(), Quaternion.identity, _enemyContainer);
}