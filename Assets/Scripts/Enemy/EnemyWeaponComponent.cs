using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyWeaponComponent : MonoBehaviour
{
    [SerializeField] private WeaponSO _weapon;

    [SerializeField] private float _attackSpeedMax = 10.0f;

    [SerializeField] private float _attackSpeedMin = 5.0f;
    private float _nextAttackTime;
    private AttackConstraints _constraints;

    private void Start()
    {
        _nextAttackTime = Time.time + GetRandomAttackCoolDown();
        _constraints = GetComponent<AttackConstraints>();
        if (_constraints == null)
            Debug.LogError($"The movement constraints are null on the {transform.name}");
    }

    private void Update()
    {
        if (CanAttack())
            Attack();
    }

    private bool CanAttack() => Time.time >= _nextAttackTime && InAttackPosition();

    private bool InAttackPosition()
    {
        var position = transform.position;
        Debug.Log($"{position}");

        if
        (
            position.y >= _constraints.YMinRange
            && position.y <= _constraints.YMaxRange
            && position.x >= _constraints.XMinRange
            && position.x <= _constraints.XMaxRange
        ) 
            return true;
        return false;
    }

    private float GetRandomAttackCoolDown() => Random.Range(_attackSpeedMin, _attackSpeedMax);

    private void Attack()
    {
        var position = transform.position;
        position.y += _weapon.Offset;

        Instantiate(_weapon.AttackPrefab, position, Quaternion.identity);
        _nextAttackTime = Time.time + GetRandomAttackCoolDown();
    }
}