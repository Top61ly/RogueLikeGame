using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyHealth : MonoBehaviour
{
    public EventHandler OnEnemyDestroyed;

    private EnemyMovement enemyMovement;
    private BulletsGenerator.BulletsAttack bulletsAttack;

    private CircleCollider2D circleCollider2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float healthPoints;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        bulletsAttack = GetComponent<BulletsGenerator.BulletsAttack>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1;
    }

    private void OnDestroyed()
    {
        Debug.Log(name + " Dead");
        animator.SetTrigger("Dead");
        enemyMovement.enabled = false;
        bulletsAttack.enabled = false;
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1);             
        if (OnEnemyDestroyed != null)
            OnEnemyDestroyed.Invoke(this, new EventArgs());
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints < 0)
            OnDestroyed();
    }
    
    public void DisableCollider()
    {
        circleCollider2D.enabled = false;
    }

    private void OnMouseDown()
    {
        TakeDamage(10);
    }
}
