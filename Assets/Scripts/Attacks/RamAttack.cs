using System;
using UnityEngine;

public class RamAttack : MonoBehaviour
{
    public Action<Transform> OnTargetAcquired;
    private Transform _targetTransform;
    private AttackConditionManager _attackConditionManager;
    private Transform _player;

    [SerializeField] private bool _canAttack = true;
    [SerializeField] private float _maxTargetingDistance = 5.5f;
 
    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        if (_player == null)
            Debug.LogError($"The player is can not be found on {transform.name}");

        _attackConditionManager = GetComponent<AttackConditionManager>();
        if (_attackConditionManager == null)
            Debug.LogError($"The attack condition manage us null on the {transform.name}");

        GetComponent<EnemyRamMovement>().OnTargetReached += delegate(bool targetReached)
        {
            Debug.Log($"Target reached {targetReached}");
            _canAttack = targetReached;
        };
    }

    private void Update()
    {
        if (_attackConditionManager.CanAttack() && _canAttack)
            CheckTargetDistance();
    }

    private void CheckTargetDistance()
    {
        var distance = Vector2.Distance(transform.position, _player.position);
        if (distance <= _maxTargetingDistance)
            Attack();
    }

    private void Attack()
    {
        _canAttack = false;
        _targetTransform = _player;
        OnTargetAcquired?.Invoke(_targetTransform);
    }
}