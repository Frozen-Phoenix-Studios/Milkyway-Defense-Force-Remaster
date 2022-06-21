using System;
using UnityEngine;

public class Powerup : MonoBehaviour, IHaveAudio
{
    [SerializeField] private PowerupType _powerupType;
    [SerializeField] private StatModifier _statModifier;
    [SerializeField] private WeaponSO _weaponPowerup;
    [SerializeField] private SupplyBox _supplyBox;
    [SerializeField] public Attachable _attachable;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private AudioClip _audioClip;
    public AudioClip AudioClip => _audioClip;

 
    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime );
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