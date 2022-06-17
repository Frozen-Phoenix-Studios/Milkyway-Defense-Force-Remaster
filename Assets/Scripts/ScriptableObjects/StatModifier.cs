using UnityEngine;

[CreateAssetMenu(menuName = "Stat Modifier")]
public class StatModifier : ScriptableObject
{
    public Stat StatToModify;
    public float Value;
    public float Length;
    public bool IsStackable;
}