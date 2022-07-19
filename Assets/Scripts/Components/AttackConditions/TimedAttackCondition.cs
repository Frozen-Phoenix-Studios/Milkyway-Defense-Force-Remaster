using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimedAttackCondition : MonoBehaviour, IAttackCondition
{
    private float _conditionValueToPass;
    [SerializeField] private bool _coolDownIsRandom = true;
    private float _coolDownTimer;
    [SerializeField] private float _coolDownMin;
    [SerializeField] private float _coolDownMax;
    private bool _isPrimed;
    public bool IsPrimed => _isPrimed;

    private void Start() => _conditionValueToPass = GetCoolDownValue();

    private float GetCoolDownValue() => _coolDownIsRandom ? Random.Range(_coolDownMin, _coolDownMax) : _coolDownTimer;


    public bool PrimeCondition()
    {
        _isPrimed = Time.time > _conditionValueToPass;
        return _isPrimed;
    }

    public bool CheckIsMet()
    {
        if (_isPrimed)
        {
            _conditionValueToPass = Time.time + GetCoolDownValue();
            _isPrimed = false;
            return true;
        }

        return false;
    }
}