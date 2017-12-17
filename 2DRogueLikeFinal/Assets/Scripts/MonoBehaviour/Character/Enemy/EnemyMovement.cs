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

    public bool isEffecting = false;
	public bool isWalking = false;
	public bool isAttacking = false;
	private bool isFacingRight = false;
    
    private Animator animator;
	private Rigidbody2D rigidBody2D;
	private Transform player;
	private CircleCollider2D circleCollider2D;

    
	   
	void Awake ()
	{
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


	public IEnumerator UpdateState()
	{
		while (true)
		{
			yield return new WaitForSeconds(thinkTime);
			if (!isAttacking)
			{
				var actionIndex = actionRandom.Random;
				enemyState = (EnemyState)actionIndex;
				switch (enemyState)
				{                    
					case EnemyState.Chasing:
						if (!isWalking && !isEffecting)
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
            if (isEffecting)
            {
                animator.SetBool("isWalking", false);
                isWalking = false;
                yield break;
            }
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

	private void CreateMovePosition()
	{

		float x = moveRange.Random;
		float y = moveRange.Random;

		Vector2 movement = new Vector2(x, y);

        Vector2 direction = movement.normalized;

        int a = 1 << LayerMask.NameToLayer("Environment");

		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,movement.magnitude+0.5f,a);

		if ( hit.collider != null)
		{
			float distance = (hit.point - rigidBody2D.position).magnitude;
            if (distance < movement.magnitude)
            {
                destination = transform.position;
            }
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

	public void GetEffect()
	{
		StopAllCoroutines();
		isAttacking = false;
        isEffecting = true;
		isWalking = true;
		StartCoroutine(UpdateState());
	}
}
