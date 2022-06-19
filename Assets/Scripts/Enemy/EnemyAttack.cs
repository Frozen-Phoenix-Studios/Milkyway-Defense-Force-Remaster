using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private WeaponSO _weapon;

    [FormerlySerializedAs("_attackSpeed")] [SerializeField]
    private float _attackSpeedMax = 3.0f;

    [SerializeField] private float _attackSpeedMin = 1.0f;
    private float _nextAttackTime;

    private void Start() => _nextAttackTime = Time.time + GetRandomAttackCoolDown();

    private void Update()
    {
        if (Time.time >= _nextAttackTime)
            Attack();
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