using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour, ITakeDamage, ISuppliable
{
    public static event Action<int> OnHealthChanged;

    [SerializeField] private SupplyType _supplyType;
    public SupplyType SupplyType => _supplyType;

    private Player _player;
    private int _maxHealth = 3;
    [SerializeField] private int _health = 3;

    [field: SerializeField] public bool TakesCollisionDamage { get; private set; }
    public int Health => _health;

    [SerializeField] private float _invulnerabilityPeriod;
    [SerializeField] private float _invulnerabilityLength = 0.25f;
    public float InvulnerabilityLength => _invulnerabilityLength;

    public int CollisionDamage { get; private set; } = 1;

    [SerializeField] private GameObject[] _fireballs;
    private string _rechargeableName;


    private void Start()
    {
        _health = _maxHealth;
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
        {
            _player.ShakeTrigger.TriggerLongShake();
            _player.Die();
            return;
        }
        _player.ShakeTrigger.TriggerSmallShake();
    }

    private void HandleDamage()
    {
        switch (_health)
        {
            case 2:
                _fireballs[Random.Range(0, _fireballs.Length)].SetActive(true);
                break;
            case 1:
            {
                foreach (var fireball in _fireballs)
                {
                    fireball.SetActive(true);
                }

                break;
            }
        }
    }

    private void ExtinguishFire()
    {
        switch (_health)
        {
            case 3:
            {
                foreach (var fireball in _fireballs)
                {
                    fireball.SetActive(false);
                }

                break;
            }
            case 2:
                _fireballs[Random.Range(0, _fireballs.Length)].SetActive(false);
                break;
        }
    }


    public void Resupply(float amount)
    {
        _health = Mathf.Clamp(_health += (int) amount, 0, _maxHealth);
        ExtinguishFire();
        OnHealthChanged?.Invoke(_health);
    }
}