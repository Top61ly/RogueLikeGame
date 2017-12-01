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

    private void Start()
    {
        if (isResetableHp)
            playerHealth.SetValue(playerMaxHealth);                    
    }

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
        Debug.Log("PlayerDead");
        GetComponent<Animator>().SetTrigger("Dead");
    }

    private void Update()
    {
        playerPosition.SetValue(transform.position);
    }
}
