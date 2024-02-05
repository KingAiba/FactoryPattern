using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected int damage = 0;
    public GameObject projectilePrefab;
    public Transform LaunchPoint;

    public event Action OnShoot;
    public event Action<Vector3> OnProjectileHit;
    public virtual void SetDamage(int value)
    {
        damage = value;
    }
    public abstract void Shoot();
    protected void InvokeOnShoot()
    {
        OnShoot?.Invoke();
    }
    protected void ProjectileHit(Vector3 position)
    {
        OnProjectileHit?.Invoke(position);
    }
}
