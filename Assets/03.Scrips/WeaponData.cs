using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemScriptable/Weapon ItemData")]
public class WeaponData : ItemData
{
    public float Damage;
    public int ClipSize;
    public int TotalClip;
}
