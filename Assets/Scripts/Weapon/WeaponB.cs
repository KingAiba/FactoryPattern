using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponB : Weapon
{

    public override void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab, LaunchPoint.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.OnProjectileHit += ProjectileHit;
        projectile.Launch(25f, LaunchPoint.forward);
        InvokeOnShoot();
        Debug.Log($"Damage: {damage}");
    }
}
