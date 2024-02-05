using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float TTl = 10f;
    public event Action<Vector3> OnProjectileHit;
    public void Launch(float force, Vector3 dir)
    {
        Rigidbody.AddForce(force * dir, ForceMode.Impulse);
        Invoke("KillProjectile", TTl);
    }

    private void KillProjectile()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnProjectileHit?.Invoke(transform.position);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnProjectileHit = null;
    }
}
