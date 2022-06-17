using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // save for efficiency
    private Rigidbody2D rb2d;   // keep private so no other scripts can get this
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    float dirX;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float jumpSpeed = 14f;

    // for animation states
    private enum MovementState
    {
        idle,
        running,
        jumping,
        falling
    }

    // for gravity inversion
    bool canInvert = true;

    // Start is called before the first frame update
    private void Start()
    {
        // get components for use later
        rb2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // get movement in x direction
        dirX = Input.GetAxisRaw("Horizontal");

        // move in x direction
        rb2d.velocity = new Vector2(dirX * moveSpeed, rb2d.velocity.y);    // keeps y-vel of frame before

        // jump controls
        if (Input.GetKeyDown("space") && canInvert == true && IsGrounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
        }
        else if (Input.GetKeyDown("space") && canInvert == false && IsOnCeiling())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -jumpSpeed);
        }

        // invert gravity controls
        if (Input.GetKeyDown("v") && canInvert == true)
        {
            rb2d.gravityScale *= -1;
            canInvert = false;
            Invoke("ResetGravity", 5f);
        }

        if (rb2d.gravityScale < 0)
        {
            sprite.flipY = true;
        }
        else
        {
            sprite.flipY = false;
        }

        SetAnimationState();
    }

    /// <summary>
    /// Checks for if the player is on the ground
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    /// <summary>
    /// Checks for if the player is on the ceiling
    /// </summary>
    /// <returns></returns>
    private bool IsOnCeiling()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, -0.1f, jumpableGround);
    }


    /// <summary>
    /// Sets the animation state
    /// </summary>
    private void SetAnimationState()
    {
        MovementState state;

        // check for running
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;   // face right
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;    // face left
        }
        else
        {
            state = MovementState.idle;
        }

        // check for jumping
        if (rb2d.velocity.y > 0.1f && canInvert == true || rb2d.velocity.y < -0.1f && canInvert == false)
        {
            state = MovementState.jumping;
        }

        // check if falling
        else if (rb2d.velocity.y < -0.1f && canInvert == true || rb2d.velocity.y > 0.1f && canInvert == false)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    /// <summary>
    /// Resets inverted gravity
    /// </summary>
    private void ResetGravity()
    {
        rb2d.gravityScale *= -1;
        canInvert = true;
    }
}
