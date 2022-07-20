using System;
using UnityEngine;

public class Explosion : Attack
{
    [SerializeField] private bool IsShrapnel;
    [SerializeField] private bool IsEnemy;

    protected override void HandleDamageDealing(int damageAmount, ITakeDamage damageable)
    {
        if (!IsShrapnel)
            return;
        damageable.TakeDamage(damageAmount);
    }

    private void FinishExplosion()
    {
        Destroy(gameObject);
    }
}