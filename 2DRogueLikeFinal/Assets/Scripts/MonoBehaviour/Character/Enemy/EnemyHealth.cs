using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    Usual,
    Rare,
    Legend
}

public class EnemyHealth : CharacterHealth
{
    public int healthPoints;

    public EnemyHealthRunTimeSet enemyHealthSet;
    public GameEvent enemyDead;

    private EnemyMovement enemyMovement;

    public EnemyType enemyType = EnemyType.Usual;
    public SpriteRenderer spriteRenderer;    

    private CircleCollider2D circleCollider2D;
    private Animator animator;

    private void OnEnable()
    {
        enemyHealthSet.Add(this);
    }

    private void OnDisable()
    {
        enemyHealthSet.Remove(this);
    }

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
        enabled = false;
        enemyDead.Raise();
    }

    public override void TakeDamage(int damage)
    {
        healthPoints -= damage;
        PopupTextControl.CreateFloatingText(damage, transform);
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
