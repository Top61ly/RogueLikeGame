using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    public IntVariable playerHealth;

    public bool isResetableHp = true;

    public IntVariable playerMaxHealth;
    public Vector3Variable playerPosition;

    public GameEvent PlayerDamaged;
    public GameEvent PlayerDead;

    public override void TakeDamage(int damage)
    {
        playerHealth.ApplyChange(-damage);
        PlayerDamaged.Raise();

        if (playerHealth.value <= 0)
        {
            Dead();
            PlayerDead.Raise();
        }
    }

    private void Dead()
    {
        GetComponent<Animator>().SetTrigger("Dead");
        gameObject.layer = LayerMask.NameToLayer("Corps");
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        enabled = false;
    }

    private void Update()
    {
        playerPosition.SetValue(transform.position);
    }
}
