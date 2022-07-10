using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    public GameObject AttackPrefab;
    public float ActiveTime;
    public float OffsetX;
    public float OffsetY;
}


//todo: could break down all power ups into supply box then player could convert them into the correct location on their script