using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    private Enemy _enemy;
    private IExplode _explodable;
    [SerializeField] private int _health = 1;
    [SerializeField] private float _invulnerabilityPeriod;

    [SerializeField]  private float _invulnerabilityLength = 0.25f;
    public float InvulnerabilityLength => _invulnerabilityLength;

    [field: SerializeField] public int CollisionDamage { get; private set; } = 1;

    [field: SerializeField] public bool TakesCollisionDamage { get; private set; } = true;
    public int Health => _health;

    private void Start()
    {
        _explodable = GetComponent<IExplode>();
        if (_explodable == null)
            Debug.LogError("Explodable is null on the enemy health script");
        
        // _enemy = GetComponent<Enemy>();
        // if (_enemy == null)
        //     Debug.LogError("Enemy is null on the enemy health script");
    }

    public void TakeDamage(int damageAmount)
    {
        if (Time.time < _invulnerabilityPeriod)
            return;
        
        _invulnerabilityPeriod = Time.time + _invulnerabilityLength;
        
        _health -= damageAmount;
        if (_health <= 0)
        {
            _explodable.Explode();
        }
    }


}