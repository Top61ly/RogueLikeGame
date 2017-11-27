﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum EnemyType
{
    Usual,
    Rare,
    Legend
}

public class EnemyHealth : CharacterHealth
{
    public EventHandler OnEnemyDestroyed;

    private EnemyMovement enemyMovement;

    public EnemyType enemyType = EnemyType.Usual;
    public SpriteRenderer spriteRenderer;    

    private CircleCollider2D circleCollider2D;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1;
    }

    private void OnDestroyed()
    {
        enemyMovement.StopAllCoroutines();
        enemyMovement.enabled = false;
        animator.SetTrigger("Dead");
        gameObject.layer = LayerMask.NameToLayer("Corps");
        spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f, 1f);
        
        if (OnEnemyDestroyed != null)
            OnEnemyDestroyed.Invoke(this, new EventArgs());
    }

    public override void TakeDamage(int damage)
    {
        healthPoints -= damage;
        StartCoroutine(Flash(0.1f));
        if (healthPoints <= 0)
        {
            OnDestroyed();
        }
   }

    protected virtual IEnumerator Flash(float flashTime)
    {       
        spriteRenderer.material.SetFloat("_FlashAmount", 1.0f);
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.material.SetFloat("_FlashAmount", 0.0f);

    }
}
