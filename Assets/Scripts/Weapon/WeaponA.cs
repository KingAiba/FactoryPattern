using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponA : Weapon
{
    public GameObject projectilePrefab;
    public override void Shoot()
    {
        //Instantiate(projectilePrefab, );
        Debug.Log($"Damage: {damage}");
    }
}
