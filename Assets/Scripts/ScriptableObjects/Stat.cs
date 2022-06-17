using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Stat")]
public class Stat : ScriptableObject
{
    public event Action<float> OnValueChanged;
    public string StatName;
    public float BaseValue;
    public List<StatModifier> AllModifiers;


    private void NotifyValueChange()
    {
        OnValueChanged?.Invoke(GetCurrentValue());
    }

    public float GetCurrentValue()
    {
        if (AllModifiers == null)
            return BaseValue;

        float totalValue = BaseValue;
        
        var allMods = AllModifiers.Where(t => t.StatToModify == this);

        foreach (var mod in allMods)
        {
            totalValue += mod.Value;
        }
        return totalValue;
    }


    public void AddStatModifier(StatModifier modifier)
    {
        if (!modifier.IsStackable && AllModifiers.Contains(modifier))
            return;

        AllModifiers.Add(modifier);
        NotifyValueChange();
    }

    public void RemoveStatModifier(StatModifier modifier)
    {
        AllModifiers.Remove(modifier);
        NotifyValueChange();
    }
}