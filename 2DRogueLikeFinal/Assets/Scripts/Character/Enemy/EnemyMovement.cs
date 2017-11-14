using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Chasing
}

public class EnemyMovement : MonoBehaviour,IAiMove
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

    private Animator animator;
    private Rigidbody2D rigidBody2D;
    private Transform player;
       
	void Awake ()
    {       
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
	}

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdateState());
    }

    void Update ()
    {
        Turn();
        DoActionByState(enemyState);
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
            if (!isWalking)
            {
                var actionIndex = actionRandom.Random;
                enemyState = (EnemyState)actionIndex;
                switch (enemyState)
                {
                    case EnemyState.Idle:
                        Idle();
                        break;
                    case EnemyState.Chasing:
                        if (!isWalking)
                            StartCoroutine(AiMove());
                        break;
                    default:
                        break;
                }       
        
            }
        }
    }

    void DoActionByState(EnemyState enemystate)
    {
    }

    private void Idle()
    {
        animator.SetBool("isWalking", false);
        rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, Vector2.zero, slowSmooth * Time.deltaTime);
    }

    public IEnumerator AiMove()
    {
        animator.SetBool("isWalking", true);

        isWalking = true;
        CreateMovePosition();
        float step = (moveSpeed / (destination - transform.position).magnitude) * Time.deltaTime;
        float t = 0;
        while (t<=1f)
        {
            t += step;
            Vector3 dest = Vector3.Lerp(transform.position, destination, t);
            rigidBody2D.MovePosition(dest);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.position = destination;
        isWalking = false;   
    }

    private void CreateMovePosition()
    {

        float x = transform.position.x + moveRange.Random;
        float y = transform.position.y + moveRange.Random;

        destination = new Vector3(x, y, 0);

        isWalking = true;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}
