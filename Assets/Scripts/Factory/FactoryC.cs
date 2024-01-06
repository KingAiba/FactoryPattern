using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryC : IFactory
{
    public Weapon CreateWeapon()
    {
        GameObject WeaponPrefab = Resources.Load<GameObject>("Prefabs/WeaponC");
        WeaponC weapon = GameObject.Instantiate(WeaponPrefab, GameManager.Instance.transform).GetComponent<WeaponC>();
        weapon.SetDamage(10);

        return weapon;
    }
}
