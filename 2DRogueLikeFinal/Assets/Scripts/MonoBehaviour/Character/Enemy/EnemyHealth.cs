using System.Collections;
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

    private CircleCollider2D circleCollider2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float healthPoints;

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
        //EnemyAttack enable = false;

        //
        if (OnEnemyDestroyed != null)
            OnEnemyDestroyed.Invoke(this, new EventArgs());
    }

    public override void TakeDamage(int damage,Vector3 hitPoint,float effectTime,float effectForce)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            enemyMovement.GetHit(hitPoint, effectTime, effectForce, true);
            OnDestroyed();
        }
        else
            enemyMovement.GetHit(hitPoint, effectTime, effectForce);
    }
    
    public void DisableCollider()
    {
        circleCollider2D.enabled = false;
    }
}
