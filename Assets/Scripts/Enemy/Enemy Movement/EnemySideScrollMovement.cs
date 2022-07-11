using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySideScrollMovement : EnemyMovementBase
{
    private AttackConstraints _attackConstraints;
    private bool _targetHeightReached;
    private float _attackHeight;
    private float _direction;
    private bool _xTargetReached;

    protected override void Initialize()
    {
        base.Initialize();
        
        _attackConstraints = GetComponent<AttackConstraints>();
        if (_attackConstraints == null)
            Debug.LogError($"The attack constraints are null on {transform.name}");
        
        _attackHeight = Random.Range(_attackConstraints.YMinRange, _attackConstraints.YMaxRange);
        Debug.Log($"Attack height is {_attackHeight}");
    }


    private void MoveToAttackHeight()
    {
        var position = transform.position;
        var direction = _moveDirection * MoveSpeed * Time.deltaTime;
        transform.Translate(direction);
        if (position.y < _attackHeight)
        {
            _moveDirection.y = 0;
            _targetHeightReached = true;
        }
    }

    private void Strafe()
    {
        if (_direction == 0)
            _direction = GetStartingDirection();

        DetermineIfTargetReached();

        _moveDirection.x = _direction;
        var direction = (_moveDirection * MoveSpeed * Time.deltaTime);

        transform.Translate(direction);
    }

    private void DetermineIfTargetReached()
    {
        var position = transform.position;

        if (_direction < 0 & position.x <= MovementConstraints.XMinRange)
        {
            _xTargetReached = true;
        }

        if (_direction > 0 & position.x >= MovementConstraints.XMaxRange)
        {
            _xTargetReached = true;
        }

        if (_xTargetReached)
        {
            _direction = -_direction;
            _xTargetReached = false;
        }
    }

    private float GetStartingDirection()
    {
        var direction = Random.Range(-1, 2);
        if (direction != 0)
            return direction;
        return GetStartingDirection();
    }

    public override void Move()
    {
        if (_targetHeightReached)
            Strafe();
        else
            MoveToAttackHeight();
    }
}