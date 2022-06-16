using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(IMove))]
public class Enemy : MonoBehaviour, IRespawn, IDoDamage
{
    private IMove _movement;
    private MovementConstraints _constraints;
    private EnemyHealth _health;

    [SerializeField] private int _damageAmount = 1;

    public int DamageAmount => _damageAmount;

    [SerializeField] private string[] _damageableTags;
    public string[] DamageableTags => _damageableTags;

    [SerializeField] private float _respawnHeight;
    public float RespawnHeight => _respawnHeight;

    private void Start()
    {
        AssignComponents();
    }

    private void AssignComponents()
    {
        _constraints = GetComponent<MovementConstraints>();
        if (_constraints == null)
            Debug.LogError("The movement constraints are null on the enemy");

        _movement = GetComponent<IMove>();
        if (_movement == null)
            Debug.LogError("The movement is null on the enemy");

        _health = GetComponent<EnemyHealth>();
        if (_health == null)
            Debug.LogError("The enemy health is null on the enemy");
    }

    private void Update()
    {
        _movement.Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var damageableTag in _damageableTags)
        {
            if (other.CompareTag(damageableTag))
            {
                var damageable = other.GetComponent<ITakeDamage>();
                if (damageable != null)
                {
                    DealDamage(_damageAmount, damageable);
                    if (_health.TakesCollisionDamage)
                        _health.TakeDamage(_health.CollisionDamage);
                }
                return;
            }
        }
    }

    public void Respawn()
    {
        transform.position = CreateNewRandomSpawnPosition();
    }

    private Vector3 CreateNewRandomSpawnPosition()
    {
        var x = Random.Range(_constraints.XMinRange, _constraints.XMaxRange);
        var y = _respawnHeight;
        var z = transform.position.z;
        return new Vector3(x, y, z);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void DealDamage(int damageAmount, ITakeDamage damageable)
    {
        damageable.TakeDamage(damageAmount);
    }
}