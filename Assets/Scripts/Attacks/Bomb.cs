using Unity.Mathematics;
using UnityEngine;

public class Bomb : Attack
{
    [SerializeField] private Explosion _shrapnelPrefab;

    private void Update()
    {
        Movement();
    }

    protected override void HandleDamageDealing(int damageAmount, ITakeDamage damageable)
    {
        Instantiate(_shrapnelPrefab, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}