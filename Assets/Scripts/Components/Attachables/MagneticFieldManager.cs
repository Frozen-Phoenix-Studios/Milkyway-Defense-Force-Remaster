using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MagneticFieldManager : MonoBehaviour, IAttachable, IHaveAudio
{
    private bool _isActive;
    public static Action<bool, GameObject> OnTractorBeamActive;
    [SerializeField] private GameObject _magneticField;
    private Animator _anim;

    [SerializeField] AudioClip _audioClip;
    public AudioClip AudioClip => _audioClip;
    private static readonly int MagneticFieldActive = Animator.StringToHash("MagneticFieldActive");
    private float _currentMagneticFieldCharge;
    private bool _charging;
    private bool _magneticFieldActive;
    [SerializeField] private float _magneticBatteryPercentUsedPerSecond = 0.25f;
    [SerializeField] private float _magneticBatteryPercentChargedPerSecond = 0.05f;
    [SerializeField] private float _delayBeforeCharging = 1.5f;
    private float _chargingDelayEnd;
    private Coroutine _chargeRoutine;
    private Coroutine _attractRoutine;

    public bool IsActive => _isActive;

    private void OnEnable()
    {
        PlayerInputReader.OnMagneticFieldStatusChanged += HandleMagneticFieldStatusChanged;
        _anim = GetComponent<Animator>();
        if (_anim == null)
            Debug.LogError("The animator is null on the Magnetic Field");
        _currentMagneticFieldCharge = 1;
        MagneticFieldUIManager.Instance.UpdateThrusterFillAmount(GetMagneticChargePercentage());
    }

    private void OnDisable()
    {
        PlayerInputReader.OnMagneticFieldStatusChanged -= HandleMagneticFieldStatusChanged;
    }

    private void HandleMagneticFieldStatusChanged(bool isActive)
    {
        _magneticFieldActive = isActive;
        if (isActive)
        {
            Attach();
        }
        else
        {
            Detach();
        }
    }

    public void Attach()
    {
        ResetRoutine(_attractRoutine);
        _attractRoutine = StartCoroutine(ReduceMagneticFieldBattery());
    }
    
    public void Detach()
    {
        _magneticField.SetActive(false);
        _anim.SetBool(MagneticFieldActive, false);

        OnTractorBeamActive?.Invoke(false, gameObject);
    }

    private void ResetRoutine(Coroutine routine)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }



    private IEnumerator ReduceMagneticFieldBattery()
    {
        if (_currentMagneticFieldCharge > 0)
        {
            SetMagneticFieldActive(true);
            _charging = !_magneticFieldActive;
        }

        while (_magneticFieldActive && _currentMagneticFieldCharge > 0)
        {
            PlayAudio();
            _currentMagneticFieldCharge -= Mathf.Clamp(_magneticBatteryPercentUsedPerSecond * Time.deltaTime, 0, 1);
            MagneticFieldUIManager.Instance.UpdateThrusterFillAmount(GetMagneticChargePercentage());
            _magneticFieldActive = _currentMagneticFieldCharge > 0;
            yield return new WaitForEndOfFrame();
        }
        
        SetMagneticFieldActive(false);
        _charging = !_magneticFieldActive;

        
        _chargingDelayEnd = Time.time + _delayBeforeCharging;
        ResetRoutine(_chargeRoutine);
        _chargeRoutine = StartCoroutine(IncreaseMagneticFieldBattery());
    }

    private void SetMagneticFieldActive(bool isActive)
    {
        _magneticFieldActive = isActive;
        _magneticField.SetActive(_magneticFieldActive);
        _anim.SetBool(MagneticFieldActive, _magneticFieldActive);
        OnTractorBeamActive?.Invoke(_magneticFieldActive, gameObject);
    }

    private IEnumerator IncreaseMagneticFieldBattery()
    {
        SetMagneticFieldActive(false);
        
        if (!_charging)
        {
            _charging = true;
            _chargingDelayEnd = Time.deltaTime + _delayBeforeCharging;
        }

        while (!ChargeDelayServed())
            yield return new WaitForEndOfFrame();

        while (!_magneticFieldActive && _currentMagneticFieldCharge < 1)
        {
            _currentMagneticFieldCharge += Mathf.Clamp(_magneticBatteryPercentChargedPerSecond * Time.deltaTime, 0, 1);
            MagneticFieldUIManager.Instance.UpdateThrusterFillAmount(GetMagneticChargePercentage());

            yield return new WaitForEndOfFrame();
        }

        _charging = false;
        _chargeRoutine = null;

    }
    
    private float GetMagneticChargePercentage()
    {
        return (_currentMagneticFieldCharge / 1);
    }

    private bool ChargeDelayServed() => Time.time > _chargingDelayEnd;


    public void PlayAudio() => AudioManager.Instance.PlayPlayerEffectAudioClip(this);
}