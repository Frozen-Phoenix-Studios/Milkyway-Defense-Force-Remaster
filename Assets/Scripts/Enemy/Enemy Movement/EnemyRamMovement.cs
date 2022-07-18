using System.Collections;
using UnityEngine;

public class EnemyRamMovement : EnemyMovementBase, IHaveAudio
{
    [SerializeField] private Stat _chargeSpeedStat;
    [SerializeField] private float _chaseDelay = 1.5f;
    private Transform _player;
    [SerializeField] private float _chaseDistance = 4.0f;
    private bool _canChasePlayer;
    [SerializeField] private float _rotationSpeed;
    private Coroutine _facePlayerRoutine;
    [SerializeField] private float _chargeSpeed;
    private Quaternion _startingRotation;
    private bool _hasSeenPlayer;
    [SerializeField] private AudioType _audioType;
    [SerializeField] private AudioClip _audioClip;
    private float _audioPlayedTimeStamp;
    private float _audioPlayedDelay = 1.0f;
    public AudioType AudioType => _audioType;

    public AudioClip AudioClip => _audioClip;

    protected override void Initialize()
    {
        base.Initialize();
        _player = GameObject.Find("Player").transform;
        if (_player == null)
            Debug.LogError($"The player is null on the {transform.name}");


        _chargeSpeedStat = _statManager.BindStat(_chargeSpeedStat);
        _chargeSpeedStat.OnValueChanged += HandleChargeSpeedChanged;
        _chargeSpeed = _chargeSpeedStat.GetCurrentModifiedValue();

        _startingRotation = transform.rotation;
    }

    private void HandleChargeSpeedChanged(float value) => _chargeSpeed = value;


    private void Update()
    {
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        if (_facePlayerRoutine != null)
            return;

        var distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance > _chaseDistance)
        {
            _canChasePlayer = false;
            _hasSeenPlayer = false;
            ResetCoroutine();
        }
        else
        {
            if (!_hasSeenPlayer)
            {
                ResetCoroutine();
                _facePlayerRoutine = StartCoroutine(RotateToLookAtPlayerRoutine());
            }
        }

        _hasSeenPlayer = distance <= _chaseDistance;
    }

    private void ResetCoroutine()
    {
        if (_facePlayerRoutine != null)
        {
            StopCoroutine(_facePlayerRoutine);
            _facePlayerRoutine = null;
        }
    }

    private IEnumerator RotateToLookAtPlayerRoutine()
    {
        PlayAudio();
        float timer = 0;
        while (timer < _chaseDelay)
        {
            var angle = GetAngle();
            var endRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, Time.deltaTime * _rotationSpeed);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }

        _canChasePlayer = true;
        _facePlayerRoutine = null;
    }

    private float GetAngle()
    {
        var position = transform.position;
        var direction = (_player.transform.position - position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        return angle;
    }


    public override void Move()
    {
        if (!_hasSeenPlayer)
        {
            var direction = _moveDirection * Time.deltaTime * MoveSpeed;
            transform.rotation =
                Quaternion.Lerp(transform.rotation, _startingRotation, Time.deltaTime * _rotationSpeed);
            transform.Translate(direction);
            return;
        }

        if (!_canChasePlayer) return;
        transform.position =
            Vector2.MoveTowards(transform.position, _player.position, _chargeSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, GetAngle());
    }


    public void PlayAudio()
    {
        AudioManager.Instance.PlayAudio(this);
        _audioPlayedTimeStamp = Time.time + _audioPlayedDelay;
    }
}