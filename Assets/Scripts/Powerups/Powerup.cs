using System;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private PowerupType _powerupType;
    [SerializeField] private StatModifier _statModifier;
    [SerializeField] private WeaponSO _weaponPowerup;
    [SerializeField] private float _speed = 3.0f;

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
            case PowerupType.Health:
                break;
            case PowerupType.Upgrade:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        DestroySelf();
    }

    private void AdjustStat(Player player, StatModifier statModifier)
    {
        if (statModifier != null)
            player.AdjustStat(statModifier);

    }

    private void ChangeWeapon(Player player)
    {
        if (_weaponPowerup != null)
            player.ChangeWeapon(_weaponPowerup);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}