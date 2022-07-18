using System;
using Unity.Mathematics;
using UnityEngine;

public class Powerup : MonoBehaviour, IHaveAudio, ITakeDamage, IExplode
{
    [SerializeField] private PowerupType _powerupType;
    [SerializeField] private StatModifier _statModifier;
    [SerializeField] private WeaponSO _weaponPowerup;
    [SerializeField] private SupplyBox _supplyBox;
    [SerializeField] public Attachable _attachable;

    [SerializeField] [Range(0f, 1f)] private float _spawnChance;
    public float SpawnChance => _spawnChance;
    
    [SerializeField]
    private float _startingSpeed = 3.0f;

    [SerializeField] private float _tractorSpeed = 3.0f;
    private float _currentSpeed;

    [SerializeField] private AudioType _audioType;
    public AudioType AudioType => _audioType;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _takesCollisionDamage;
    private int _health;
    private float _invulnerabilityLength;
    private int _collisionDamage;
    [SerializeField] private Explosion _explosion;
    public AudioClip AudioClip => _audioClip;
    private static bool _tractorBeamActive = false;
    private static GameObject _target;
    
    public bool TakesCollisionDamage => _takesCollisionDamage;
    public Explosion Explosion => _explosion;
    public int Health => _health;
    public float InvulnerabilityLength => _invulnerabilityLength;
    public int CollisionDamage => _collisionDamage;

    private void OnEnable()
    {
        _currentSpeed = _startingSpeed;
        MagneticFieldManager.OnTractorBeamActive += HandleTractorBeamChanged;
    }

    private void HandleTractorBeamChanged(bool isActive, GameObject tractorBeam)
    {
        _tractorBeamActive = isActive;
        _target = tractorBeam;
        _currentSpeed = _tractorBeamActive ? _tractorSpeed : _startingSpeed;
    }


    private void Update()
    {
        if (_tractorBeamActive)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _currentSpeed);
            return;
        }

        transform.Translate(Vector3.down * _currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                GivePowerup(player);
            }
        }
    }

    private void GivePowerup(Player player)
    {
        switch (_powerupType)
        {
            case PowerupType.Weapon:
                ChangeWeapon(player);
                break;
            case PowerupType.Stat:
                AdjustStat(player, _statModifier);
                break;
            case PowerupType.Resupply:
                Resupply(player, _supplyBox);
                break;
            case PowerupType.Attachable:
                AddAttachable(player, _attachable);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        PlayAudio();
        DestroySelf();
    }

    private void AddAttachable(Player player, Attachable attachable)
    {
        if (attachable != null)
            player.AddAttachable(attachable);
    }

    private void AdjustStat(Player player, StatModifier statModifier)
    {
        if (statModifier != null)
            player.AdjustStat(statModifier);
    }

    private void Resupply(Player player, SupplyBox supplyBox)
    {
        if (supplyBox != null)
            player.Resupply(supplyBox);
    }

    private void ChangeWeapon(Player player)
    {
        if (_weaponPowerup != null)
            player.ChangeWeapon(_weaponPowerup);
    }

    public void PlayAudio()
    {
        AudioManager.Instance.PlayAudio(this);
    }

    public void DestroySelf()
    {
       
        Destroy(gameObject);
    }


    public void TakeDamage(int damageAmount)
    {
        Explode();
    }


    public void Explode()
    {
        Instantiate(_explosion, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}