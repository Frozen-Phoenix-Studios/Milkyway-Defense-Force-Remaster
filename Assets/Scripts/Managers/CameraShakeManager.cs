using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShakeManager : MonoBehaviour
{
    private CinemachineImpulseSource _source;
    private NoiseSettings _settings;
    [SerializeField] private float _smallShakeFrequency;
    [SerializeField] private float _largeShakeFrequency;
    [SerializeField] private float _shortShakeLength;
    [SerializeField] private float _longShakeLength;


    private void OnEnable()
    {
        CameraShakeTrigger.OnSmallShake += GenerateSmallShake;
        CameraShakeTrigger.OnLargeShake += GenerateLargeShake;
        CameraShakeTrigger.OnLongShake += GenerateLongShake;
    }

    private void OnDisable()
    {
        CameraShakeTrigger.OnSmallShake -= GenerateSmallShake;
        CameraShakeTrigger.OnLargeShake -= GenerateLargeShake;
        CameraShakeTrigger.OnLongShake -= GenerateLongShake;
    }

    private void Start()
    {
        _source = GetComponent<CinemachineImpulseSource>();
        if (_source == null)
            Debug.LogError($"The impulse source is null on {transform.name}");
    }

    private void GenerateSmallShake()
    {

        _source.m_ImpulseDefinition.m_TimeEnvelope.m_AttackTime = _shortShakeLength;
        _source.m_ImpulseDefinition.m_FrequencyGain = _smallShakeFrequency;

        _source.GenerateImpulse();
    }

    private void GenerateLargeShake()
    {

        _source.m_ImpulseDefinition.m_TimeEnvelope.m_AttackTime = _shortShakeLength;
        _source.m_ImpulseDefinition.m_FrequencyGain = _largeShakeFrequency;

        _source.GenerateImpulse();
    }

    private void GenerateLongShake()
    {
        _source.m_ImpulseDefinition.m_TimeEnvelope.m_AttackTime = _longShakeLength;
        _source.m_ImpulseDefinition.m_FrequencyGain = _largeShakeFrequency;

        _source.GenerateImpulse();
    }
}