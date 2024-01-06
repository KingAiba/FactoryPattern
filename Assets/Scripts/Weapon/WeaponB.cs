using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponB : Weapon
{
    public override void Shoot()
    {
        Debug.Log($"Damage: {damage}");
    }
}
