using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharater : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask EnemiesLayer;
    [SerializeField] public float speed = 10f;
    [SerializeField] public float jumpForce = 20f;
    private float originalJumpForce; // Lưu trữ lực nhảy ban đầu
    [SerializeField] private Transform _canJump;
    [SerializeField] private LayerMask ground;
    public Animator anim;

    public AudioSource src;
    public AudioClip jump, dash;

    private bool canJump;
    private bool doubleJump;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 30f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private float dirX = 0f;


    public bool isInWater = false; // Biến đánh dấu nhân vật đang ở trong nước

    private enum MovementState { idle, running, jumping, falling }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalJumpForce = jumpForce; // Lưu trữ lực nhảy ban đầu
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * dirX, rb.velocity.y);

        if (dirX != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(dirX), 1);
        }

        canJump = Physics2D.OverlapCircle(_canJump.position, 0.2f, ground);

        if (canJump && !Input.GetKey(KeyCode.K))
        {
            doubleJump = false;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (canJump || doubleJump || isInWater)
            {

                src.clip = jump;
                src.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                if (!isInWater)
                {
                    doubleJump = !doubleJump;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.L) && canDash)
        {
            StartCoroutine(Dash());
        }

        UpdateAnimationState();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_canJump.position, 0.2f);
    }

    private IEnumerator Dash()
    {
        anim.SetTrigger("Dash");

        src.clip = dash;
        src.Play();
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f || dirX < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    public void AdjustJumpForce(float reductionFactor)
    {
        jumpForce = originalJumpForce * reductionFactor;
    }

    public void ResetJumpForce()
    {
        jumpForce = originalJumpForce;
    }
}
