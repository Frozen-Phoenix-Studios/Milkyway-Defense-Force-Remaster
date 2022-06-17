using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public static event Action<int> OnHealthChanged;
    
    private Player _player;
    [SerializeField] private int _health = 3;

    [field: SerializeField] public bool TakesCollisionDamage { get; private set; }
    public int Health => _health;
    public int CollisionDamage { get; private set; } = 1;


    private void Start()
    {
        _player = transform.GetComponent<Player>();
        if (_player == null)
            Debug.LogError("The Player is null on the player health component");
        OnHealthChanged?.Invoke(_health);
    }
    
    public void TakeDamage(int damageAmount)
    {
        _health = Mathf.Clamp(_health -= damageAmount, 0, 3);
        OnHealthChanged?.Invoke(_health);
        if (_health <= 0)
            _player.Die();
    }
}