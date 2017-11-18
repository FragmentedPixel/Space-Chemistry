using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for the player's movement.
 */

//TODO: Create more responsive controls.

public class PlayerMovement : MonoBehaviour
{
    #region Variabiles
    // The movement speed of the player.
    public float speed = 10f;

    // Maximum speed of the player.
    public float maxVelocity = 4f;
    
    // Components.
    private Rigidbody2D rb;
    private Animator anim;
    private const string walkingHash = "Walk";

    // Is the player on the ground?
    private bool grounded = false;

    // Player's feet.
    public Transform groundCheck;
    public LayerMask whatIsGround;

    // Jumping Parameters.
    public float jumpForce;
    public float pressJumpForce;

    #endregion

    
    #region Init
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
    }

	void Update () 
	{
        grounded = isGrounded();

        Jump();

        HandleMovement();
    }

    public bool isGrounded()
    {
        float distanceToGround = 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, -Vector2.up, distanceToGround, whatIsGround);

        return (hit.point != Vector2.zero);
    }
    #endregion

    #region Movement
    private void HandleMovement()
    {
        //this can return -1 if you move to the left 0 if you don't move 1 if you move to the right
        float movement = Input.GetAxisRaw("Horizontal");

        // Get current speed of the rb.
        float vel = Mathf.Abs(rb.velocity.x);

        // Stop if there is no movement input.
        if (movement == 0)
        {
            StopMove();
        }

        // Update the player rigidbody according to the user input.
        else if (vel < maxVelocity)
        {
            bool movingRight = (movement > 0);
            Move(movingRight);
        }
    }

    private void Move(bool right)
    {
        // Make the player face the movement direction.
        Vector3 temp = anim.transform.localScale;
        temp.x = right ? 0.5f :  -0.5f;
        anim.transform.localScale = temp;

        // update the player's components.
        anim.SetBool(walkingHash, true);
        float forceX = right ? speed : -speed;
        rb.velocity = (new Vector2(forceX, rb.velocity.y));
    }

    private void StopMove()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        anim.SetBool(walkingHash, false);
    }
    #endregion

    #region Jump
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
            PerformJump();

        if(! grounded && Input.GetButton("Jump"))
        {
            rb.AddForce(new Vector2(0f, pressJumpForce));
        }
        
    }

    private void PerformJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    #endregion

}
