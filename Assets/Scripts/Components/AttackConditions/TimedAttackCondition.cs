using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TimedAttackCondition : MonoBehaviour, IAttackCondition
{
    [SerializeField] private float _currentTime;
    [SerializeField] private float _nextAttackTime;
    [SerializeField] private bool _coolDownIsRandom = true;
    [SerializeField][Tooltip("Used if cool downs are not random")]private float _coolDownTimer;
    [SerializeField] private float _coolDownMin;
    [SerializeField] private float _coolDownMax;
    [SerializeField] private bool _isPrimed;
    public bool IsPrimed => _isPrimed;


    private void Start() => _nextAttackTime = GetCoolDownValue();

    private void Update()
    {
        _currentTime = Time.time;
    }

    private float GetCoolDownValue() => _coolDownIsRandom ? Random.Range(_coolDownMin, _coolDownMax) : _coolDownTimer;


    public void PrimeCondition()
    {
        _isPrimed = Time.time > _nextAttackTime;
    }

    public void Activate()
    {
        if (_isPrimed)
        {
            _isPrimed = false;
            _nextAttackTime = Time.time + GetCoolDownValue();
        }
    }

    
    

}