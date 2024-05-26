using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

public class MainCharater : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] public float speed = 10f;
    [SerializeField] public float jumpForce = 20f;
    //[SerializeField] private TrailRenderer tr;

    private bool canJump;
    public Transform _canJump;
    public LayerMask ground;
    private bool doubleJump;
    public Animator anim;
    float dirX = 0f;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 30f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    //public AudioSource src;
    //public AudioClip jump, dash;
    private enum MovementState { idle, running, jumping, falling }



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * dirX, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.localScale = new Vector2(1, 1);
        }

        canJump = Physics2D.OverlapCircle(_canJump.position, 0.2f, ground);

        if (canJump && !Input.GetKey(KeyCode.K))
        {
            doubleJump = false;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {

            if (canJump || doubleJump)
            {
                //src.clip = jump;
                //src.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                doubleJump = !doubleJump;

            }
        }
        if (Input.GetKeyDown(KeyCode.L) && canDash)
        {
            //src.clip = dash;
            //src.Play();
            StartCoroutine(Dash());
        }
        UpdateAnimationState();
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }
    private IEnumerator Dash()
    {
        anim.SetTrigger("Dash");
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
       // tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
        }
        else if (dirX < 0f)
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
}
