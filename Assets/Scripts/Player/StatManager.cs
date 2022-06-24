using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] private List<Stat> _allStats;

    public Stat BindStat(Stat statToBind)
    {
        var stat = _allStats.First(t => t.StatName == statToBind.StatName);
        return stat;
    }

    private void Start()
    {
        foreach (var stat in _allStats)
        {
            stat.RemoveAllModifiers();
        }
    }

    public void AddTemporaryStatModifier(StatModifier modifier)
    {
        if (VerifyStat(modifier, out var stat)) return;

        if (modifier.Length > 0)
        {
            StartCoroutine(AddTemporaryStatRoutine(stat, modifier));
        }
    }
    
    private bool VerifyStat(StatModifier modifier, out Stat stat)
    {
        stat = _allStats.First(t => modifier.StatToModify = t);
        return stat == null;
    }

    public void AddStatModifier(StatModifier modifier)
    {
        if (VerifyStat(modifier, out var stat)) return;
        stat.AddStatModifier(modifier);
    }
    public void RemoveStatModifier(StatModifier modifier)
    {
        if (VerifyStat(modifier, out var stat)) return;
        stat.RemoveStatModifier(modifier);
    }
    
    

    public float GetStatValue(Stat stat)
    {
        if (_allStats.Contains(stat))
            return _allStats.First(t => t.StatName == stat.StatName).GetCurrentModifiedValue();

        Debug.LogError($"Stat {stat.StatName} can not be found");
        return 0;
    }

    private IEnumerator AddTemporaryStatRoutine(Stat statToModify, StatModifier modifier)
    {
        statToModify.AddStatModifier(modifier);
        yield return new WaitForSeconds(modifier.Length);
        statToModify.RemoveStatModifier(modifier);
    }
}