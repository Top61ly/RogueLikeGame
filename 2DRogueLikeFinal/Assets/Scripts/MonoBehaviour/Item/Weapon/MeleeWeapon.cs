using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{        
    public Collider2D weaponCollider;
    private Animator animator;

    public override void Init()
    {
        base.Init();
        animator = GetComponent<Animator>();
    }

    public override void TriggerWeapon()
    {
        animator.SetTrigger("Attack");
        var collider2Ds = Physics2D.OverlapBoxAll(weaponCollider.bounds.center, weaponCollider.bounds.extents, weaponCollider.transform.rotation.eulerAngles.z, LayerMask.GetMask("Enemy"));
        
    }

    public override void DisableEffect()
    {

    }
    
}
