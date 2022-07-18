using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(IMove))]
public class Enemy : MonoBehaviour, IRespawn, IDoDamage, IChangePoints, IExplode, ISpawnable
{
    public static event Action<int> OnPointsAction;

    private IMove _movement;
    private MovementConstraints _constraints;
    private EnemyHealth _health;
    private EnemyAnimationController _animationController;
    private Collider2D _collider;

    [SerializeField] private int _damageAmount = 1;
    [field: SerializeField] public int PointsOnAction { get; private set; } = 10;

    public int DamageAmount => _damageAmount;

    [SerializeField] [Tooltip("An array of all the tags this object can damage")]
    private string[] _damageableTags;

    public string[] DamageableTags => _damageableTags;

    [SerializeField] private float _respawnHeight;
    [SerializeField] private Explosion _explosion;
    [SerializeField] [Range(0f, 1f)] private float _spawnChance;
    public float SpawnChance => _spawnChance;

    public float RespawnHeight => _respawnHeight;
    public Explosion Explosion => _explosion;

    private void OnEnable() => GameStateManager.OnGameOver += HandleGameOverStatusChange;

    private void OnDisable() => GameStateManager.OnGameOver -= HandleGameOverStatusChange;

    private void HandleGameOverStatusChange(bool isGameOver)
    {
        if (isGameOver) Destroy(gameObject);
    }

    private void Start() => AssignComponents();

    private void AssignComponents()
    {
        _movement = GetComponent<IMove>();
        if (_movement == null)
            Debug.LogError("The movement is null on the enemy");

        if (transform.CompareTag("Container"))
            return;

        _constraints = GetComponent<MovementConstraints>();
        if (_constraints == null)
            Debug.LogError("The movement constraints are null on the enemy");


        _health = GetComponent<EnemyHealth>();
        if (_health == null)
            Debug.LogError("The enemy health is null on the enemy");

        _collider = GetComponent<Collider2D>();
        if (_collider == null)
            Debug.LogError("The enemy collider is null on");
    }

    private void Update() => _movement.Move();


    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var damageableTag in _damageableTags)
        {
            if (other.CompareTag(damageableTag))
            {
                var damageable = other.GetComponent<ITakeDamage>();
                if (damageable != null && damageable.TakesCollisionDamage)
                {
                    DealDamage(_damageAmount, damageable);
                    if (_health.TakesCollisionDamage)
                        _health.TakeDamage(_health.CollisionDamage);
                }

                return;
            }
        }
    }

    public void Respawn() => transform.position = CreateNewRandomSpawnPosition();

    private Vector3 CreateNewRandomSpawnPosition()
    {
        var x = Random.Range(_constraints.XMinRange, _constraints.XMaxRange);
        var y = _respawnHeight;
        var z = transform.position.z;
        return new Vector3(x, y, z);
    }

    public void Explode()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        OnPointsAction?.Invoke(PointsOnAction);

        GameObject objectToDestroy = transform.gameObject;

        if (transform.parent != null && transform.parent.CompareTag("Container"))
            objectToDestroy = transform.parent.gameObject;

        Destroy(objectToDestroy);
    }

    public void DealDamage(int damageAmount, ITakeDamage damageable)
    {
        damageable.TakeDamage(damageAmount);
    }
}