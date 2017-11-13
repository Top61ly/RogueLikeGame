using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}

public class EnemyMovement : MonoBehaviour,IAiMove
{
    public float chaseRange;

    public float attackRange;

    public float moveSpeed;

    public float slowSmooth;

    private EnemyState enemyState;
    private BulletsGenerator.BulletsAttack bulletsAttack;

    private Transform player;
    private Animator animator;
    private Rigidbody2D rigidBody2D;
       
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bulletsAttack = GetComponent<BulletsGenerator.BulletsAttack>();
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        UpdateState();
        DoActionByState(enemyState);		
	}

    void UpdateState()
    {
        float range = (player.position - transform.position).magnitude;

        if (range > chaseRange)
            enemyState = EnemyState.Idle;
        else if (range > attackRange)
            enemyState = EnemyState.Chasing;
        else
            enemyState = EnemyState.Attacking;
    }

    void DoActionByState(EnemyState enemystate)
    {
        switch (enemystate)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chasing:
                AiMove();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
            default:
                break;
        }       
    }

    private void Idle()
    {
        animator.SetBool("isWalking", false);
        rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, Vector2.zero, slowSmooth * Time.deltaTime);
    }

    public void AiMove()
    {
        animator.SetBool("isWalking", true);
        Vector3 direction = (player.position-transform.position).normalized;
        rigidBody2D.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, Vector2.zero, slowSmooth * Time.deltaTime);
    }
}
