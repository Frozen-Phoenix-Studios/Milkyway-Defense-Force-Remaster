using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
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

    }

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            _player.Die();
        }
    }
}