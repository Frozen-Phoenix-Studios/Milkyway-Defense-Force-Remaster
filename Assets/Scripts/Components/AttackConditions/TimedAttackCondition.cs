using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimedAttackCondition : MonoBehaviour, IAttackCondition
{
    private float _conditionValueToPass;
    [SerializeField] private bool _coolDownIsRandom = true;
    [SerializeField] private float _coolDown;
    [SerializeField] private float _coolDownMin;
    [SerializeField] private float _coolDownMax;
    private bool _isPrimed;
    public bool IsPrimed => _isPrimed;



    private void Start() => _conditionValueToPass = GetCoolDownValue();

    private float GetCoolDownValue() => _coolDownIsRandom ? Random.Range(_coolDownMin, _coolDownMax) : _coolDown;


    public bool PrimeCondition()
    {
        if (Time.time >= _conditionValueToPass)
        {
            _isPrimed = true;
            Debug.Log($"Condition {this} is primed");

        }
        return _isPrimed;
    }

    public bool CheckIsMet()
    {
        if (_isPrimed)
        {
            _conditionValueToPass = Time.time + GetCoolDownValue();
            _isPrimed = false;
            Debug.Log($"Condition {this} is met");

            return true;
        }
        Debug.Log($"Condition {this} is NOT met");

        return false;
    }
}