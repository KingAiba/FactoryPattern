using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryB : IFactory
{
    public Weapon CreateWeapon(Transform holder)
    {
        GameObject WeaponPrefab = Resources.Load<GameObject>("Prefabs/WeaponB");
        WeaponB weapon = GameObject.Instantiate(WeaponPrefab, holder).GetComponent<WeaponB>();
        weapon.SetDamage(5);

        return weapon;
    }
}
