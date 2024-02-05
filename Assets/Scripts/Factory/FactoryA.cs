using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryA : IFactory
{
    public Weapon CreateWeapon(Transform holder)
    {
        GameObject WeaponPrefab = Resources.Load<GameObject>("Prefabs/WeaponA");
        WeaponA weapon = GameObject.Instantiate(WeaponPrefab, holder).GetComponent<WeaponA>();
        weapon.SetDamage(15);

        return weapon;
    }
}
