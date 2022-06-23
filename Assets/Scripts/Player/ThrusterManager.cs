using System.Collections;
using UnityEngine;

public class ThrusterManager : MonoBehaviour, IHaveAudio
{
    private  PlayerInputReader _input;
    [SerializeField] private GameObject[] _thrustersArray;
    private float _speed;
    private int _index;
    private bool _turboEngaged;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _currentThrusterCharge;
    [SerializeField] [Range(0f, 1f)] private float _thrusterPercentUsedPerSecond = 0.25f;
    [SerializeField] [Range(0f, 1f)]  private float _thrusterPercentChargedPerSecond = 0.15f;
    [SerializeField] private float _delayBeforeRecharge = 1.0f;
    private float _chargeDelayEnd;
    
    private Coroutine _chargeRoutine;
    private Coroutine _boostRoutine;
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
    }

    private void Update()
    {
        _speed = _input.move.y;

        if (_turboEngaged)
        {
            _index = 3;
        }
        else if (_speed < 0)
        {
            _index = 0;
        }
        else if (_speed == 0)
        {
            _index = 1;
        }
        else
        {
            _index = 2;
        }

        SetThrusterActive(_index);
    }


    private void SetThrusterActive(int index)
    {
        for (int i = 0; i < _thrustersArray.Length; i++)
            _thrustersArray[i].SetActive(i == index);
    }

    private IEnumerator ReduceThrusterCharge()
    {
        while (_turboEngaged && _currentThrusterCharge > 0)
        {
            _currentThrusterCharge -= Mathf.Clamp(_thrusterPercentUsedPerSecond * Time.deltaTime, 0, 1);
            ThrusterUIManager.Instance.UpdateThrusterFillAmount(GetThrusterChargePercentage());
            yield return new WaitForEndOfFrame();
        }
        _chargeDelayEnd = Time.time + _delayBeforeRecharge;
        _boostRoutine = null;
    }
    
    private IEnumerator IncreaseThrusterCharge()
    {
        _chargeDelayEnd = Time.time + _delayBeforeRecharge;
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
        AudioManager.Instance.PlayPlayerEffectAudioClip(this);
    }
}
