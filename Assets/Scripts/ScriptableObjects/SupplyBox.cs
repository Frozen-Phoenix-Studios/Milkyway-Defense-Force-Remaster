using UnityEngine;

public enum SupplyType
{
    Health,
    Ammo
}

[CreateAssetMenu(menuName = "Supply box")]
public class SupplyBox : ScriptableObject
{
    public SupplyType SupplyType;
    public float Amount;

}