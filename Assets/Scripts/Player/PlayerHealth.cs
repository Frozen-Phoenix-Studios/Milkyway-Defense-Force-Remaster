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

    [SerializeField] private float _invulnerabilityPeriod;
    [SerializeField]  private float _invulnerabilityLength = 0.25f;
    public float InvulnerabilityLength => _invulnerabilityLength;

    public int CollisionDamage { get; private set; } = 1;

    [SerializeField] private GameObject[] _fireballs;
    private string _rechargeableName;


    private void Start()
    {
        _player = transform.GetComponent<Player>();
        if (_player == null)
            Debug.LogError("The Player is null on the player health component");
        OnHealthChanged?.Invoke(_health);
    }

    public void TakeDamage(int damageAmount)
    {
        if (Time.time < _invulnerabilityPeriod)
            return;
        
        _invulnerabilityPeriod = Time.time + _invulnerabilityLength;
        
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