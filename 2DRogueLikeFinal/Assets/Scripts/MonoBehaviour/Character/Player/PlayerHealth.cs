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

    }

    private void Update()
    {
        playerPosition.SetValue(transform.position);
    }
}
