using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected int damage = 0;
    public virtual void SetDamage(int value)
    {
        damage = value;
    }
    public abstract void Shoot();
}
