using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeWeapon : Weapon
{
    public GameObject bullets;    

    public Animator weaponAnimator;

    public AudioSource gunAudio;

    public override void TriggerWeapon()
    {
        if (gunAudio)
            gunAudio.Play();
        ImmediateShoot();
        ImmediateEnableEffect();
    }

    public override void DisableEffect()
    {
        ImmediateDisableEffect();
    }

    public override void Init()
    {
        base.Init();
        bullets.GetComponent<Bullets>().damage = damage;
        SpecificInit();
    }

    protected virtual void ImmediateEnableEffect() { }

    protected virtual void ImmediateDisableEffect() { }

    protected abstract void ImmediateShoot();

    protected abstract void SpecificInit();
}
