using System.Linq;
using UnityEngine;

public class EnemyWeaponComponent : MonoBehaviour
{
    [SerializeField] private WeaponSO _weapon;
    private float _nextAttackTime;
    private AttackConstraints _constraints;
    private IAttackCondition[] _attackConditions;

    private void Start()
    {
        _constraints = GetComponent<AttackConstraints>();
        if (_constraints == null)
            Debug.LogError($"The movement constraints are null on the {transform.name}");

        _attackConditions = GetComponents<IAttackCondition>();

    }

    private void Update()
    {
        if (CanAttack())
            Attack();
    }

    private bool CanAttack()
    {
        foreach (var condition in _attackConditions)
        {
            if (condition.PrimeCondition() == false) return false;
        }

        return true;
    }

    private void Attack()
    {
        if (_attackConditions.Any(condition => condition.CheckIsMet() == false))
        {
            return;
        }
        var position = transform.position;
        position.y += _weapon.OffsetY;
        position.x += _weapon.OffsetX;

        Instantiate(_weapon.AttackPrefab, position, Quaternion.identity);
    }

}