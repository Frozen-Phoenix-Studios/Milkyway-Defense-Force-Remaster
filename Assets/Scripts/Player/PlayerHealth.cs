using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public static event Action<int> OnHealthChanged;

    private Player _player;
    [SerializeField] private int _health = 3;

    [field: SerializeField] public bool TakesCollisionDamage { get; private set; }
    public int Health => _health;
    public int CollisionDamage { get; private set; } = 1;

    [SerializeField] private GameObject[] _fireballs;


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
        HandleDamage();
        if (_health <= 0)
            _player.Die();
    }

    private void HandleDamage()
    {
        if (_health == 2)
        {
            _fireballs[Random.Range(0, _fireballs.Length)].SetActive(true);
        }

        if (_health == 1)
        {
            foreach (var fireball in _fireballs)
            {
                fireball.SetActive(true);
            }
        }
    }
}