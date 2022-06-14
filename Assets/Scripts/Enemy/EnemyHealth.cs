using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    private Enemy _enemy;
    [SerializeField] private int _health = 1;
    [field: SerializeField] public int CollisionDamage { get; private set; } = 1;

    [field: SerializeField] public bool TakesCollisionDamage { get; private set; } = true;
    public int Health => _health;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        if (_enemy == null)
        {
            Debug.LogError("Enemy is null on the enemy health script");
        }
    }

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            _enemy.Die();
        }
    }


}