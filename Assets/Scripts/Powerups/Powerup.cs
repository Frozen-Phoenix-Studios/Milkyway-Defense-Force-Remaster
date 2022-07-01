using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Powerup : MonoBehaviour, IHaveAudio
{
    [SerializeField] private PowerupType _powerupType;
    [SerializeField] private StatModifier _statModifier;
    [SerializeField] private WeaponSO _weaponPowerup;
    [SerializeField] private SupplyBox _supplyBox;
    [SerializeField] public Attachable _attachable;
    [FormerlySerializedAs("_speed")] [SerializeField] private float _startingSpeed = 3.0f;
    [SerializeField] private float _tractorSpeed = 3.0f;
    private float _currentSpeed;

    [SerializeField] private AudioType _audioType;
    public AudioType AudioType => _audioType;
    [SerializeField] private AudioClip _audioClip;
    public AudioClip AudioClip => _audioClip;
    private static bool _tractorBeamActive = false;
    private static GameObject _target;

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
        AudioManager.Instance.PlayPowerupAudioClip(this);
    }

    public void DestroySelf()
    {
        PlayAudio();
        Destroy(gameObject);
    }
}