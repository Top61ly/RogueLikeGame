using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public int playerIndex;
    public float timerGap;
    public int damage;

    public override void Use(Transform player)
    {
        var playerAttack = player.GetComponent<PlayerAttack>();
        var weapon = player.GetComponentInChildren<Weapon>();
        
        playerAttack.weaponTransform.DetachChildren();
        weapon.DisableEffect();
        weapon.transform.rotation = Quaternion.identity;
        weapon.transform.position = new Vector3(weapon.transform.position.x, weapon.transform.position.y - 0.2f, weapon.transform.position.z);

        transform.SetParent(playerAttack.weaponTransform);
        transform.GetComponent<BoxCollider2D>().enabled = false;

        weapon.GetComponent<BoxCollider2D>().enabled = true;
      //  weapon.transform.SetParent(GameObject.Find("EnemyHolder").transform);

        playerAttack.weapon = this;
        Init();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Start()
    {
        Init();
        DisableEffect();
    }
    
    public abstract void TriggerWeapon();
    public abstract void DisableEffect();
}
