using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryB : IFactory
{
    public Weapon CreateWeapon()
    {
        GameObject WeaponPrefab = Resources.Load<GameObject>("Prefabs/WeaponB");
        WeaponB weapon = GameObject.Instantiate(WeaponPrefab, GameManager.Instance.transform).GetComponent<WeaponB>();
        weapon.SetDamage(5);

        return weapon;
    }
}
