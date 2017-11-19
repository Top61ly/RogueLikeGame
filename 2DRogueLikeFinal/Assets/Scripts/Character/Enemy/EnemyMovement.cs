using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Chasing,
    Attacking
}

public class EnemyMovement : MonoBehaviour
{    
    public float moveSpeed;

    public float slowSmooth;

    public IntRange actionRandom;
    public FloatRange moveRange;
    private Vector3 destination;

    public float thinkTime;

    public EnemyState enemyState;

    private bool isWalking = false;
    private bool isFacingRight = false;
    private bool isAttacking = false;

    private Animator animator;
    private Rigidbody2D rigidBody2D;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;
       
	void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
	}

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CreateMovePosition();
        StartCoroutine(UpdateState());
    }

    void Update ()
    {
        Turn();
    }

    private void Turn()
    {
        float h = player.position.x - transform.position.x;
        if (h > 0 && !isFacingRight)
            Flip();
        else if (h < 0 && isFacingRight)
            Flip();
    }


    private IEnumerator UpdateState()
    {
        while (true)
        {
            yield return new WaitForSeconds(thinkTime);
            if (!isWalking && !isAttacking)
            {
                var actionIndex = actionRandom.Random;
                enemyState = (EnemyState)actionIndex;
                switch (enemyState)
                {                    
                    case EnemyState.Chasing:
                        if (!isWalking)
                            StartCoroutine(AiMove());
                        break;
                    case EnemyState.Attacking:
                        StartCoroutine(Attack());
                        break;
                    default:
                        break;
                }       
        
            }
        }
    }

    private IEnumerator AiMove()
    {
        animator.SetBool("isWalking", true);

        isWalking = true;
             
        CreateMovePosition();

        float step = (moveSpeed / (destination - transform.position).magnitude) * Time.deltaTime;
        var startposition = transform.position;
        float t = 0;
        while (t <= 1f)
        {
            t += step;
            Vector3 dest = Vector3.Lerp(startposition, destination, t);
            rigidBody2D.MovePosition(dest);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.position = destination;        
        isWalking = false;
        animator.SetBool("isWalking", false);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        rigidBody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.717f);
        isAttacking = false;
    }


    public void GetHit(Vector3 hitPoint, float effectTime, float effectForce,bool isEnemyDead = false)
    {
        SetStateNormal();
        StartCoroutine(HitEffect(hitPoint, effectTime, effectForce,isEnemyDead));
    }

    private IEnumerator HitEffect(Vector3 hitPoint, float effectTime, float effectForce,bool isEnemyDead = false)
    {
        if (isEnemyDead)
        {
            animator.SetTrigger("Dead");
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        if (rigidBody2D.isKinematic)
        {
            rigidBody2D.isKinematic = false;
            rigidBody2D.AddForce((rigidBody2D.position - new Vector2(hitPoint.x,hitPoint.y)).normalized * effectForce);
        }

        yield return new WaitForSeconds(effectTime);        
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.isKinematic = true;

        if (isEnemyDead)
        {
            circleCollider2D.enabled = false;
            this.enabled = false;
        }
        else
            SetStateMoveable();
    }

    private void CreateMovePosition()
    {

        float x = moveRange.Random;
        float y = moveRange.Random;

        Vector2 movement = new Vector2(x, y);

        int a = 1<< LayerMask.NameToLayer("Environment");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement,Mathf.Infinity,a);

        if ( hit.collider != null)
        {
            float distance = (hit.point - rigidBody2D.position).magnitude;
            if (distance < movement.magnitude)
                CreateMovePosition();
            else
                destination = rigidBody2D.position + movement;
        }
        else        
            destination = rigidBody2D.position + movement;        
        
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }

    private void SetStateNormal()
    {
        StopAllCoroutines();
        animator.SetBool("isWalking", false);
        isWalking = false;
        isAttacking = false;
    }

    private void SetStateMoveable()
    {
        StartCoroutine(UpdateState());
    }
}
