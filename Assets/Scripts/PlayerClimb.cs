using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public Rigidbody2D rb;
    public float SpeedClimb = 10f;
    private bool isClimbing = false;
    private float originalGravity;
    public Animator ani;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
            ani.SetBool("IsClimbing", true);
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            ani.SetBool("IsClimbing", false);

            isClimbing = false;
            rb.gravityScale = originalGravity;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            float climbInput = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, climbInput * SpeedClimb);
        }
    }
}
