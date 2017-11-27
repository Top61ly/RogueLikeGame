using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrap : Trap
{
    public float timeGap;

    public Animator trapAnimator;

    private float timer;

    private List<Collider2D> colliderCharacter = new List<Collider2D>();

    private void Start()
    {
        timer = Time.time;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer>timeGap)
        {
            trapAnimator.SetTrigger("Attack");
            timer = 0;
        }
    }

    public override void Attack()
    {
        foreach (var collider in colliderCharacter)
        {
            if (!collider)
            {
                var characterHealth = collider.GetComponent<CharacterHealth>();
                if (characterHealth.healthPoints > 0)
                    characterHealth.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!colliderCharacter.Contains(collision))
        {
            colliderCharacter.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliderCharacter.Contains(collision))
        {
            colliderCharacter.Remove(collision);
        }
    }
}
