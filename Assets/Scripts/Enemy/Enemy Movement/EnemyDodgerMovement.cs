using System.Collections;
using UnityEngine;

public class EnemyDodgerMovement : EnemyMovementBase
{
    private int _direction;
    [SerializeField] private float _minDirectionChangeFrequency = 0.5f;
    [SerializeField] private float _maxDirectionChangeFrequency = 3f;
    private float _directionChangeTimer;
    private bool _isDodging;
    private float _dodgeLength;

    protected override void Initialize()
    {
        base.Initialize();
        GetComponentInChildren<LaserRadar>().OnDodge += ActivateDodge;
    }

    public override void Move()
    {
        if (!_isDodging)
        {
            CheckDirectionTimer();
        }

        var direction = _moveDirection.normalized * (Time.deltaTime * MoveSpeed);


        transform.Translate(direction);
    }

    private void CheckDirectionTimer()
    {
        if (Time.time >= _directionChangeTimer)
            PickDirection();
    }

    private void ActivateDodge(float dodgeLength)
    {
        _dodgeLength = dodgeLength;
        StartCoroutine(DodgeRoutine());
    }

    private IEnumerator DodgeRoutine()
    {
        var originalY = _moveDirection.y;
        _moveDirection.y = 0;
        _moveDirection.x = GetSidewaysDirection();
        _isDodging = true;
        yield return new WaitForSeconds(_dodgeLength);
        _moveDirection.y = originalY;
        PickDirection();
        _isDodging = false;
    }

    private int GetSidewaysDirection()
    {
        var direction = Random.Range(-1, 2);
        if (direction != 0)
            return direction;
        return GetSidewaysDirection();
    }

    private void PickDirection()
    {
        _direction = Random.Range(-1, 2);
        _moveDirection.x = _direction;
        _directionChangeTimer = Random.Range(_minDirectionChangeFrequency, _maxDirectionChangeFrequency) + Time.time;
    }
}