using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    
    [SerializeField] private List<Stat> _allStats;

    public Stat BindStat(Stat statToBind)
    {
        var stat = _allStats.First(t => t.StatName == statToBind.StatName);
        return stat;
    }

    public void ModifyStat(StatModifier modifier)
    {
        var stat = _allStats.First(t => modifier.StatToModify = t);
        if (stat != null)
        {
            StartCoroutine(ModifyStat(stat, modifier));
        }
    }

    public float GetStatValue(Stat stat)
    {
        if (_allStats.Contains(stat))
            return _allStats.First(t => t.StatName == stat.StatName).GetCurrentValue();
        
        Debug.LogError($"Stat {stat.StatName} can not be found");
        return 0;
    }

    private IEnumerator ModifyStat(Stat statToModify, StatModifier modifier)
    {
        statToModify.AddStatModifier(modifier);
        yield return new WaitForSeconds(modifier.Length);
        statToModify.RemoveStatModifier(modifier);
    }
    
    

}
