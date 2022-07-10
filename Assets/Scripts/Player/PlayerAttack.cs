using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInputReader _input;

    [SerializeField] private WeaponSO _defaultWeapon;
    private WeaponSO _weapon;
    private AmmoManager _ammoManager;
    private StatManager _statManager;
    [SerializeField] private float _attackSpeed = 0.25f;
    private float _nextAttackTime;
    private Coroutine _temporaryWeaponRoutine;

    private void Start()
    {
        _weapon = _defaultWeapon;
        _nextAttackTime = 0;
        
        _input = GetComponent<PlayerInputReader>();
        if (_input == null)
            Debug.LogError("The player input reader is null on the player attack");
        
        _ammoManager = GetComponent<AmmoManager>();
        if (_ammoManager == null)
            Debug.LogError("The ammo manager is null on the player attack");

    }

    private void Update()
    {
        if (_input.shoot && CanShoot())
            Shoot();
    }

    private bool CanShoot()
    {
        if (!(Time.time >= _nextAttackTime && _ammoManager.UseAmmo())) return false;

        _nextAttackTime = Time.time + _attackSpeed;
        return true;
    }

   

    private void Shoot()
    {
        var position = transform.position;
        position.y += _weapon.OffsetY;

        Instantiate(_weapon.AttackPrefab, position, Quaternion.identity);
    }

    public void ChangeWeapon(WeaponSO weapon)
    {
        StartTemporaryWeaponActivation(weapon);
    }
    

    private void StartTemporaryWeaponActivation(WeaponSO weapon)
    {
        if (_temporaryWeaponRoutine != null)
        {
            StopCoroutine(_temporaryWeaponRoutine);
        }

        _temporaryWeaponRoutine = StartCoroutine(TemporaryWeaponRoutine(weapon));
    }

    private IEnumerator TemporaryWeaponRoutine(WeaponSO weapon)
    {
        _weapon = weapon;
        yield return new WaitForSeconds(weapon.ActiveTime);
        _weapon = _defaultWeapon;
        _temporaryWeaponRoutine = null;
    }
}