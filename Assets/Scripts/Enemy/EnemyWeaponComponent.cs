using UnityEngine;

public class EnemyWeaponComponent : MonoBehaviour
{
    private AttackConditionManager _attackConditionManager;
    [SerializeField] private WeaponSO _weapon;
    private float _nextAttackTime;
    private AttackConstraints _constraints;
    private IAttackCondition[] _attackConditions;

    private void Start()
    {
        _attackConditionManager = GetComponent<AttackConditionManager>();
        if (_attackConditionManager == null)
            Debug.LogError($"The attack condition manage us null on the {transform.name}");
    }

    private void Update()
    {
        if (_attackConditionManager.CanAttack())
            Attack();
    }

    private void Attack()
    {
        Instantiate(_weapon.AttackPrefab, CalculateAttackOffset(), Quaternion.identity);
    }

    private Vector3 CalculateAttackOffset()
    {
        var position = transform.position;
        position.y += _weapon.OffsetY;
        position.x += _weapon.OffsetX;
        return position;
    }
}