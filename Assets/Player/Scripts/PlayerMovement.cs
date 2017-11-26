using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for the player's movement.
 */

public class PlayerMovement : MonoBehaviour
{
    #region Variabiles

    // Movement parameters.
    [Header("Movement")]
    public float speed = 10f;
    public float maxSpeed = 4f;
    
    // Components.
    private Rigidbody2D rb;
    private Animator anim;
    private const string walkingHash = "Walk";

    // Is the player on the ground?
    private bool grounded = false;

    // Player's feet.
    [Header("Grounded")]
    public Transform groundCheck;
    public Transform cameraPoint;
    public LayerMask whatIsGround;


    // Jumping Parameters.
    [Header("Jumping")]
    public float jumpForce;
    public float pressJumpForce;
    public float gravityBonus = 9.81f;

    #endregion

    #region Init
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
    }

    public void ChangeControl(bool hasControl)
    {
        if (hasControl == false)
            StopMove();

        enabled = hasControl;
    }
    #endregion

    #region Update
    void Update()
    {
        grounded = isGrounded();

        HandleJump();

        HandleMovement();
    }

    public bool isGrounded()
    {
        float distanceToGround = 0.2f;

        bool isPlayerOnGround = Physics2D.OverlapCircle(groundCheck.position, distanceToGround, whatIsGround);

        return isPlayerOnGround;
    }

    #endregion

    #region Movement
    private void HandleMovement()
    {
        //this can return -1 if you move to the left 0 if you don't move 1 if you move to the right
        float movement = Input.GetAxis("Horizontal");

        // Get current speed of the rb.
        float vel = Mathf.Abs(rb.velocity.x);

        // Stop if there is no movement input.
        if (movement == 0)
        {
            StopMove();
        }

        // Update the player rigidbody according to the user input.
        else if (vel < maxSpeed)
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

        //TODO: Discus this lepring options
        //float currentSpeed = Mathf.Lerp(0, maxSpeed, Mathf.Abs(Input.GetAxis("Horizontal")));
        //float forceX = right ? currentSpeed : -currentSpeed;

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
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (! grounded && Input.GetButton("Jump"))
        { 
            rb.AddForce(new Vector2(0f, pressJumpForce));
        }

        if(!grounded)
        {
            ApplayGravity();
        }
        
    }

    private void ApplayGravity()
    {
        Vector2 currentVel = rb.velocity;
        currentVel.y -= gravityBonus * Time.deltaTime;
        rb.velocity = currentVel;
    }
    #endregion

}
