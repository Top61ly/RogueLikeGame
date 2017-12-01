using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{   
    public float moveSpeed;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public Vector2 movement;

    public Vector2 facePosition;

    public bool isFacingRight = true;   

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        
        Move();

        Turn();

        Animating();

    }

    void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1;
    }

    private void Animating()
    {
        bool walking = movement.magnitude > 0.01f;

        animator.SetBool("isWalking", walking);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement.normalized*moveSpeed*Time.deltaTime);    
    }
    
    private void Turn()
    {
        float viewDirection = facePosition.x - transform.position.x;
        if (viewDirection > 0 && !isFacingRight)
            flip();
        else if (viewDirection < 0 && isFacingRight)
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
