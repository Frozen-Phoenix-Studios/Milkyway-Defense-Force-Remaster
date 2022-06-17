using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    public GameObject AttackPrefab;
    public float ActiveTime;
    public float Offset;
}