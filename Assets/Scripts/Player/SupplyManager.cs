using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SupplyManager : MonoBehaviour
{
    private List<ISuppliable> _AllSupplies;

    private void Start()
    {
        _AllSupplies = GetComponents<ISuppliable>().ToList();
    }

    public void Resupply(SupplyBox supplyBox)
    {
        var suppliable = _AllSupplies.First(t => t.SupplyType == supplyBox.SupplyType);
        if (suppliable != null)
        {
            suppliable.Resupply(supplyBox.Amount);
        }
    }
}