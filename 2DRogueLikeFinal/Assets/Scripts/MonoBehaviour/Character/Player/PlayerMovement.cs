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

    public bool isFacingRight = true;   

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Move(h,v);

        Turn(mousePosition);

        Animating(h, v);

    }

    void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1;
    }

    private void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0;

        animator.SetBool("isWalking", walking);
    }

    private void Move(float h, float v)
    {
        movement.Set(h, v);

        rb.MovePosition(rb.position + movement.normalized*moveSpeed*Time.deltaTime);    
    }
    
    private void Turn(Vector2 mousePosition)
    {
        float viewDirection = mousePosition.x - transform.position.x;
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
