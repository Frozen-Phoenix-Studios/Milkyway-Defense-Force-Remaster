using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    public bool TakesCollisionDamage { get; }

    public int Health { get; }
    public int CollisionDamage { get; }
    public void TakeDamage(int damageAmount);
    
}