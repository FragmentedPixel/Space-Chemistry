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
    public float startSpeed = 1f;
    public float maxSpeed = 4f;
    public float acceleration = 1f;
    private float currentAcceleration;
    
    public ParticleSystem sparklesPartilces;

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

        if(isPlayerOnGround == false)
        {
            StopParticles();
        }

        anim.SetBool("Ground", grounded);

        return isPlayerOnGround;
    }

    private void StopParticles()
    {
        sparklesPartilces.Stop();
    }

    #endregion

    #region Movement
    private void HandleMovement()
    {
        //this can return -1 if you move to the left 0 if you don't move 1 if you move to the right
        float movement = Input.GetAxisRaw("Horizontal");

        // Get current speed of the rb.
        float vel = Mathf.Abs(rb.velocity.x);

        // Slow down if player is going too fast.
        if(vel > maxSpeed)
        {
            SlowDownMovement();
        }
        // Stop if there is no movement input.
        else if(movement == 0)
        {
            StopMove();
        }
        else
        // Update player's rb according to user input.
        {
            bool movingRight = (movement > 0);
            Move(movingRight);
        }
        
    }

    private void Move(bool right)
    {
        // Make the player face the movement direction.
        Vector3 temp = anim.transform.localScale;
        float xValue = Mathf.Abs(temp.x);

        temp.x = right ? xValue : -xValue;  
        anim.transform.localScale = temp;

        // update the player's components.
        anim.SetBool(walkingHash, true);
        currentAcceleration += Time.deltaTime;

        float currentSpeed = Mathf.Lerp(startSpeed, maxSpeed, currentAcceleration / acceleration);
        anim.SetFloat("MovementSpeed", currentAcceleration / acceleration);

        float forceX = right ? currentSpeed : -currentSpeed;

        rb.velocity = (new Vector2(forceX, rb.velocity.y));

        if (grounded)
        {
            if (!sparklesPartilces.isPlaying)
                sparklesPartilces.Play();
        }

    }

    private void StopMove()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        anim.SetBool(walkingHash, false);
        currentAcceleration = 0f;

        StopParticles();
    }

    private void SlowDownMovement()
    {
        float slowDownTime = 1.5f;
        float speedX = Mathf.Lerp(rb.velocity.x, 0f, slowDownTime * Time.deltaTime);
        rb.velocity = new Vector2(speedX, rb.velocity.y);
    }
    #endregion

    #region Jump
    private void HandleJump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)) && grounded)
        {
            anim.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        if (! grounded && (Input.GetButton("Jump") || Input.GetKey(KeyCode.Space)))
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
