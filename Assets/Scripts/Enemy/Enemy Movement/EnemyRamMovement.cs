using System;
using System.Collections;
using UnityEngine;

public class EnemyRamMovement : EnemyMovementBase, IHaveAudio
{
    public Action<bool> OnTargetReached;
    [Header("Stats")]
    [SerializeField] private Stat _chargeSpeedStat;
    [SerializeField] private float _chaseDelay = 1.5f;
    [SerializeField] private float _rotationSpeed;
    private float _chargeSpeed;

    private Transform _targetTransform;
    private bool _lockedOn;
    private Coroutine _targetLockRoutine;
    private Quaternion _startingRotation;
    private Vector3 _targetPosition;
    
    [Header("Audio")]
    [SerializeField] private AudioType _audioType;

    [SerializeField] private AudioClip _audioClip;
    private float _audioPlayedTimeStamp;
    private float _audioPlayedDelay = 1.0f;
    public AudioClip AudioClip => _audioClip;
    public AudioType AudioType => _audioType;

    [SerializeField] private GameObject _thruster;
    private bool _targetAcquired;

    protected override void Initialize()
    {
        base.Initialize();
        GetComponent<RamAttack>().OnTargetAcquired += SetTarget;
        _chargeSpeedStat = _statManager.BindStat(_chargeSpeedStat);
        _chargeSpeedStat.OnValueChanged += HandleChargeSpeedChanged;
        _chargeSpeed = _chargeSpeedStat.GetCurrentModifiedValue();

        _startingRotation = transform.rotation;
    }
    
    private void HandleChargeSpeedChanged(float value) => _chargeSpeed = value;

    private void SetTarget(Transform targetTransform)
    {
        _targetTransform = targetTransform;
        _targetLockRoutine = StartCoroutine(TargetLockRoutine());
    }

    private IEnumerator TargetLockRoutine()
    {
        _targetAcquired = true;
        PlayAudio();
        float timer = 0;
        while (timer < _chaseDelay)
        {
            _targetPosition = _targetTransform.position;
            var angle = GetAngle();
            var endRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, Time.deltaTime * _rotationSpeed);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        
        _lockedOn = true;
        _thruster.SetActive(_lockedOn);
    }

    public override void Move()
    {
        if (!_targetAcquired)
        {
            MoveDown();
            return;
        }

        if (!_lockedOn) return;
        MoveToTargetPosition();
    }

    private void MoveDown()
    {
        var direction = _moveDirection * Time.deltaTime * MoveSpeed;
        transform.rotation =
            Quaternion.Lerp(transform.rotation, _startingRotation, Time.deltaTime * _rotationSpeed);
        transform.Translate(direction);
    }

    private void MoveToTargetPosition()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, _targetPosition, _chargeSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, GetAngle());
        
        var distance = Vector2.Distance(transform.position, _targetPosition);
        if (distance <= 0.1f)
        {
            ResetTargeting();
            OnTargetReached?.Invoke(true);
        }
    }

    private void ResetTargeting()
    {
        _lockedOn = false;
        _targetAcquired = false;
        _thruster.SetActive(_lockedOn);
    }

    private float GetAngle()
    {
        var position = transform.position;
        var direction = (_targetPosition - position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        return angle;
    }
    
    private float GetAngle(Vector3 positionFrom, Vector3 positionTo)
    {
        var direction = (positionTo - positionFrom).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        return angle;
    }

    public void PlayAudio() => AudioManager.Instance.PlayAudio(this);
}