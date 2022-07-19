using System;
using System.Linq;
using UnityEngine;

public class AttackConditionManager: MonoBehaviour
{
    private IAttackCondition[] _attackConditions;

    private void Start()
    {
        _attackConditions = GetComponents<IAttackCondition>();
    }

    private void PrimeConditions()
    {
        foreach (var condition in _attackConditions)
            condition.PrimeCondition();
    }

    private bool CheckConditions() => _attackConditions.All(condition => condition.CheckIsMet() != false);

    public bool CanAttack()
    {
        if (CheckConditions())
            return true;

        PrimeConditions();
        return false;
    }
}