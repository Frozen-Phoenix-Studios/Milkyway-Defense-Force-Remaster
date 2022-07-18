using System.Collections;
using UnityEngine;

public class ThrusterManager : MonoBehaviour, IHaveAudio
{
    private PlayerInputReader _input;
    [SerializeField] private GameObject[] _thrustersArray;
    private float _speed;
    private int _index;
    private bool _turboEngaged;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _currentThrusterCharge;
    [SerializeField] [Range(0f, 1f)] private float _thrusterPercentUsedPerSecond = 0.25f;
    [SerializeField] [Range(0f, 1f)] private float _thrusterPercentChargedPerSecond = 0.15f;
    [SerializeField] private float _delayBeforeRecharge = 1.0f;
    private float _chargeDelayEnd;
    private bool _charging;

    private bool _boostActive;

    [SerializeField] private StatModifier StatModifier;
    private StatManager _statManager;

    private Coroutine _chargeRoutine;
    private Coroutine _boostRoutine;
    
    [SerializeField] private AudioType _audioType;
    public AudioType AudioType => _audioType;
    public AudioClip AudioClip => _audioClip;

    private void OnEnable()
    {
        PlayerInputReader.OnTurboChanged += SetTurboThruster;
    }

    private void OnDisable()
    {
        PlayerInputReader.OnTurboChanged -= SetTurboThruster;
    }

    private void Start()
    {
        _currentThrusterCharge = 1;
        _input = GetComponentInParent<PlayerInputReader>();
        if (_input == null)
            Debug.LogError("The player input is null on the thruster manager");
        _statManager = GetComponentInParent<StatManager>();
        if (_statManager == null)
            Debug.LogError("The stat manager is null on the booster component");
    }

    private void Update()
    {
        if (_turboEngaged)
            return;

        _speed = _input.move.y;

        _index = _speed switch
        {
            < 0 => 0,
            0 => 1,
            _ => 2
        };

        SetThrusterActive(_index);
    }

    private void SetThrusterActive(int index)
    {
        for (int i = 0; i < _thrustersArray.Length; i++)
            _thrustersArray[i].SetActive(i == index);
    }

    private IEnumerator ReduceThrusterCharge()
    {
        if (_currentThrusterCharge > 0)
        {
            SetThrusterActive(3);
            _statManager.AddStatModifier(StatModifier);
            _charging = false;
        }

        while (_turboEngaged && _currentThrusterCharge > 0)
        {
            _currentThrusterCharge -= Mathf.Clamp(_thrusterPercentUsedPerSecond * Time.deltaTime, 0, 1);
            ThrusterUIManager.Instance.UpdateThrusterFillAmount(GetThrusterChargePercentage());
            _turboEngaged = _currentThrusterCharge > 0;
            yield return new WaitForEndOfFrame();
        }

        _statManager.RemoveStatModifier(StatModifier);
        _chargeDelayEnd = Time.time + _delayBeforeRecharge;
        _charging = true;
        _chargeRoutine = StartCoroutine(IncreaseThrusterCharge());
        _boostRoutine = null;
    }

    private IEnumerator IncreaseThrusterCharge()
    {
        _turboEngaged = false;
        if (!_charging)
        {
            _charging = true;
            _chargeDelayEnd = Time.time + _delayBeforeRecharge;
            _statManager.RemoveStatModifier(StatModifier);
        }

        while (!ChargeDelayServed())
            yield return new WaitForEndOfFrame();

        while (!_turboEngaged && _currentThrusterCharge < 1)
        {
            _currentThrusterCharge += Mathf.Clamp(_thrusterPercentChargedPerSecond * Time.deltaTime, 0f, 1f);
            ThrusterUIManager.Instance.UpdateThrusterFillAmount(GetThrusterChargePercentage());
            yield return new WaitForEndOfFrame();
        }

        _chargeRoutine = null;
    }

    private bool ChargeDelayServed()
    {
        return Time.time >= _chargeDelayEnd;
    }

    private float GetThrusterChargePercentage()
    {
        return (_currentThrusterCharge / 1);
    }

    private void SetTurboThruster(bool engaged)
    {
        _turboEngaged = engaged;

        if (_turboEngaged)
        {
            ResetRoutine(_chargeRoutine);
            ResetRoutine(_boostRoutine);

            _boostRoutine = StartCoroutine(ReduceThrusterCharge());
        }
        else
        {
            ResetRoutine(_chargeRoutine);
            ResetRoutine(_boostRoutine);

            _chargeRoutine = StartCoroutine(IncreaseThrusterCharge());
        }
    }

    private void ResetRoutine(Coroutine routine)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }

    public void PlayAudio()
    {
        AudioManager.Instance.PlayAudio(this);
    }
}