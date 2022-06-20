using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Booster : MonoBehaviour
{
    [SerializeField] private StatModifier StatModifier;
    private bool IsActive;
    private StatManager _statManager;

    private void OnEnable()
    {
        PlayerInputReader.OnTurboChanged += AdjustStat;
    }
    
    private void OnDisable()
    {
        PlayerInputReader.OnTurboChanged -= AdjustStat;
    }

    private void Start()
    {
        _statManager = GetComponent<StatManager>();
        if(_statManager == null)
            Debug.LogError("The stat manager is null on the booster component");
    }


    private void AdjustStat(bool active)
    {
        if (active)
        {
            _statManager.AddStatModifier(StatModifier);
        }
        else
        {
            _statManager.RemoveStatModifier(StatModifier);
        }

    }

}
