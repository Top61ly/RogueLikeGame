using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public float timerGap;

    public override void Use(Transform player)
    {
        var playerAttack = player.GetComponent<PlayerAttack>();
        var weapon = player.GetComponentInChildren<Weapon>();
        
        playerAttack.weaponTransform.DetachChildren();
        weapon.DisableEffect();
        weapon.transform.rotation = Quaternion.identity;

        transform.SetParent(playerAttack.weaponTransform);
        transform.GetComponent<BoxCollider2D>().enabled = false;

        weapon.GetComponent<BoxCollider2D>().enabled = true;

        playerAttack.weapon = this;
        Init();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
    }

    public abstract void Init();
    
    public abstract void TriggerWeapon();
    public abstract void DisableEffect();
}
