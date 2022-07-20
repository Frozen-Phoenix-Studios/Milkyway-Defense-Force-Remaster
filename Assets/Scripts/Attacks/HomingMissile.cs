using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomingMissile : Attack
{
    private static readonly HashSet<EnemyBase> _lockedOnEnemies = new HashSet<EnemyBase>();

    private EnemyBase _target;
    [SerializeField] private float _rotationSpeed;

    private readonly Quaternion _startingRotation = Quaternion.identity;

    protected override void Initialize()
    {
        base.Initialize();
        FindTarget();
    }

    private void Update()
    {
        if (_target == null)
        {
            if (_lockedOnEnemies.Count > 0)
            {
                FindTarget();
                return;
            }

            RotateToFaceForward();
            transform.Translate(Vector3.up * (Time.deltaTime * _movementSpeed));
        }
        else
        {
            RotateToFaceEnemy();
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position,
                _movementSpeed * Time.deltaTime);
        }
    }

    private void FindTarget()
    {
        var enemyList = FindObjectsOfType<EnemyBase>().ToList();
        var position = transform.position;

        if (enemyList.Count > 0)
        {
            enemyList.OrderBy(t => Vector2.Distance(position, t.transform.position));
            var closestFreeEnemy = enemyList.FirstOrDefault(t => _lockedOnEnemies.Contains(t) == false);
            if (closestFreeEnemy != null)
            {
                _lockedOnEnemies.Add(closestFreeEnemy);
                _target = closestFreeEnemy;
            }
            else
            {
                _target = enemyList[0];
            }
        }
    }

    private void RotateToFaceEnemy()
    {
        var direction = _target.transform.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var endRotation = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, Time.deltaTime * _rotationSpeed);
    }

    private void RotateToFaceForward()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _startingRotation, Time.deltaTime * _rotationSpeed);
    }

    protected override void HandleDamageDealing(int damageAmount, ITakeDamage damageable)
    {
        if (damageable.Health <= damageAmount)
        {
            _lockedOnEnemies.Remove(_target);
        }

        base.HandleDamageDealing(damageAmount, damageable);
    }
}