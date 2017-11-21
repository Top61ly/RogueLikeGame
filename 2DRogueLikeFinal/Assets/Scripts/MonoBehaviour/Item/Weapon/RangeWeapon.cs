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
        ImmediateEnableEffect();
    }

    public override void DisableEffect()
    {
        ImmediateDisableEffect();
    }

    public override void Init()
    {
        SpecificInit();
    }

    protected virtual void ImmediateEnableEffect() { }

    protected virtual void ImmediateDisableEffect() { }

    protected abstract void ImmediateShoot();

    protected abstract void SpecificInit();
}
