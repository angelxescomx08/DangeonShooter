using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 input;
    private bool isFacingRight = true;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInputs();
        Flip();
        animator.SetFloat("Speed", input.magnitude);
    }

    private void ProcessInputs()
    {
        int xInput = (int)Input.GetAxisRaw("Horizontal");
        int yInput = (int)Input.GetAxisRaw("Vertical");
        input = new Vector2(xInput, yInput).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = input * speed;
    }

    private void Flip()
    {
        if((isFacingRight && input.x < 0f) || (!isFacingRight && input.x > 0f))
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            isFacingRight = !isFacingRight;
        }
    }
}
