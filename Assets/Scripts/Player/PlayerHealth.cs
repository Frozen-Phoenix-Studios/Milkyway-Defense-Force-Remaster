using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int _health = 3;

    [field: SerializeField] public bool TakesCollisionDamage { get; private set; }
    public int Health => _health;
    public int CollisionDamage { get; private set; } = 1;

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            Debug.Log("Player Died");
            GameManager.Instance.SetGameOver();
            Destroy(gameObject);

        }
    }
}