using System.Collections;
using UnityEngine;

public class EnemyZigZagMovement : EnemyMovementBase
{
    private int _direction;

    [SerializeField] private float _directionChangeFrequencyMin;
    [SerializeField] private float _directionChangeFrequencyMax;
    private float _directionChangeTimer;
    private bool _isDodging;
    private float _dodgeLength;

    public override void Move()
    {
        if (!_isDodging)
        {
            CheckDirectionTimer();
        }

        transform.Translate(_moveDirection * Time.deltaTime * MoveSpeed);
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
        _isDodging = true;
        _moveDirection.y = -_moveDirection.y;
        _moveDirection.y = GetSidewaysDirection();
        yield return new WaitForSeconds(_dodgeLength);
        _moveDirection.y = -_moveDirection.y;
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
        _directionChangeTimer = Random.Range(_directionChangeFrequencyMin, _directionChangeFrequencyMax) + Time.time;
    }
}