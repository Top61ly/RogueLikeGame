using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeWeapon : Weapon
{
    public GameObject bullets;
        
    public override void TriggerWeapon()
    {
        ImmediateShoot();
    }

    public override void DisableEffect()
    {
        ImmediateDisableEffect();
    }

    protected virtual void ImmediateDisableEffect() { }

    protected abstract void ImmediateShoot(); 
}
