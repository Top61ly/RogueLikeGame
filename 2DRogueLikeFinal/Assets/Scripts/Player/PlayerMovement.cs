using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    public float smoothTime = 0.3f;
    private float moveSpeed;

    private Animator playerAnimator;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private bool isFacingRight = true;
    private Vector3 target;
    
    private bool isWalking;
    private float hMove;
    private float vMove;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        isWalking = false;
    }


    void Update()
    {
        getInput();
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1;
    }

    private void getInput()
    {
        Vector3 movement = new Vector3();
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }

        movement = target - transform.position; 
           
        hMove = movement.x;
        vMove = movement.y;
        isWalking = isWalk();
    }

    private void Move()
    {
        if (isWalking)
        {
            playerAnimator.SetBool("isWalking", true);

            // Move your game object using a rigid body force to get it moving in the right direction. 
            rb.MovePosition(rb.position + new Vector2(hMove * moveSpeed * Time.fixedDeltaTime, vMove * moveSpeed * Time.fixedDeltaTime));
            //transform.position = Vector3.SmoothDamp(transform.position, target, ref vec3MoveSpeed, smoothTime);

            //playerTransform.Translate(new Vector3(hMove*moveSpeed*Time.deltaTime,vMove*moveSpeed*Time.deltaTime));
        }
        else
        {
            rb.velocity = Vector3.zero;
            playerAnimator.SetBool("isWalking", false);
        }
    }

    private bool isWalk()
    {
        if (hMove != 0 || vMove != 0)
            return true;
        else
            return false;
    }

    private void Turn()
    {

        if (hMove > 0 && !isFacingRight)
            flip();
        else if (hMove < 0 && isFacingRight)
            flip();
    }

    private void flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}
