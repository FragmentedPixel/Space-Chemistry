using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for the player's movement.
 */

public class PlayerMovement : PlayerContrable
{
    #region Variabiles

    [Header("Grounded")]
    public Transform groundCheck;
    public Transform cameraPoint;
    public LayerMask whatIsGround;
    public float distanceToGround = .3f;

    [Header("Movement")]
    public float minSpeed = 1f;
    public float maxSpeed = 4f;
    public float acceleration = 1f;
    
    [Header("Jumping")]
    public float jumpForce;
    public float pressJumpForce;
    public float jumpMovement;
    public float gravityBonus = 9.81f;
    public float bigFallDistance = 2f;

    [Header("Effects")]
    public ParticleSystem trailParticles;
    public ParticleSystem bigFallParticles;

    // Components.
    private Rigidbody2D rb;
    private Animator anim;

    // Animation Hashes
    private const string walkingHash = "Walk";
    private const string jumpHash = "Jump";
    private const string movementSpeedHash = "MovementSpeed";
    private const string groundedHash = "Ground";
    
    // Current state of the player
    private bool grounded = false;
    private float currentSpeed = 0f;
    private float currentJump = 0f;

    #endregion

    #region Super Classes

    public override void RemoveControl()
    {
        StopMove();
    }

    #endregion

    #region Initialization
    void Awake()
    {
        // Get components from the GO.
        rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
    }
    #endregion

    #region Every Frame

    private void Update()
    {
        // Check if is grounded this frame.
        grounded = isGrounded();

        // Handle Jump input.
        HandleJump();

        // Handle Movement input.
        HandleMovement();
    }

    private bool isGrounded()
    {
        // Circular raycast for ground elements.
        bool isPlayerOnGround = Physics2D.OverlapCircle(groundCheck.position, distanceToGround, whatIsGround);

        // Updating the effects if the player is not on the ground.
        if(isPlayerOnGround == false)
        {
            StopTrailParticles();
        }

        // Updating the anim.
        anim.SetBool(groundedHash, grounded);

        // Return to update isgrounded.
        return isPlayerOnGround;
    }

    #endregion

    #region Jump

    private void HandleJump()
    {
        // Check the player input.
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)) && grounded)
        {
            // The jump has started and we need to update the animator.
            anim.SetBool(jumpHash, true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (!grounded && (Input.GetButton("Jump") || Input.GetKey(KeyCode.Space)))
        {
            //TODO: Allow the player to add force only when going up (rb.vel.y > .3f)
            // The jump should last longer.
            rb.AddForce(new Vector2(0f, pressJumpForce * Time.deltaTime), ForceMode2D.Impulse);
        }
        else if(grounded && currentJump >= bigFallDistance)
        {
            PlayFallParticles();
        }

        // Update to reflect grounded state.
        if (!grounded)
        {
            currentJump += Time.deltaTime;
            ApplayGravity();
        }
        else
        {
            currentJump = 0f;
            anim.SetBool(jumpHash, false);
        }
    }

    private void ApplayGravity()
    {
        // Apply extra down velocity for more falling speed.
        Vector2 currentVel = rb.velocity;
        currentVel.y -= gravityBonus * Time.deltaTime;
        rb.velocity = currentVel;
    }

    public void DisableJump()
    {
        jumpForce = 0f;
    }

    #endregion

    #region Movement 
    private void HandleMovement()
    {
        //this can return -1 if you move to the left 0 if you don't move 1 if you move to the right
        float playerInput = Input.GetAxisRaw("Horizontal");
        bool movingRight = (playerInput > 0);

        // Stop movement if there is no horizontal movement.
        if (playerInput == 0)
        {
            StopMove();
        }

        // Update player's rb according to user b input.
        else if(grounded)
        {
            MoveOnGround(movingRight);
        }
        else if(!grounded)
        {
            MoveInAir(movingRight);
        }
    }

    private void MoveOnGround(bool movingRight)
    {
        // Updates the player's sprite flip to face forward direction.
        FlipSprite(movingRight);

        // Updates the forces applied to the rb.
        MoveRigidBody(movingRight);

        // Updates the player's animator.
        AnimatorMovement();

        // Add effects to the movement.
        PlayTrailParticles();
    }

    private void MoveInAir(bool movingRight)
    {
        // Updates the player's sprite flip to face forward direction.
        FlipSprite(movingRight);

        // Updates the forces applied to the rb.
        MoveRigidBody(movingRight);
    }

    private void FlipSprite(bool movingRight)
    {
        // Make the player face the movement direction.
        Vector3 temp = anim.transform.localScale;
        float xValue = Mathf.Abs(temp.x);

        // Update the player mesh with the new scale.
        temp.x = movingRight ? xValue : -xValue;
        anim.transform.localScale = temp;
    }

    private void AnimatorMovement()
    {
        // Set the walking subtree
        anim.SetBool(walkingHash, true);

        // Set the value for the walking blend state.
        float animValue = (currentSpeed - minSpeed) / (maxSpeed - minSpeed);
        anim.SetFloat(movementSpeedHash, animValue);
    }

    private void MoveRigidBody(bool movingRight)
    {
        if(grounded)
        {
            currentSpeed += Time.deltaTime * acceleration;
            currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
        }
        else
        {
            currentSpeed = jumpMovement;
        }
        

        float forceX = movingRight ? currentSpeed : -currentSpeed;

        rb.velocity = (new Vector2(forceX, rb.velocity.y));
    }

    private void StopMove()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        anim.SetBool(walkingHash, false);
        currentSpeed = minSpeed;

        StopTrailParticles();
    }

    private void SlowDownMovement()
    {
        float slowDownTime = 1.5f;
        float speedX = Mathf.Lerp(rb.velocity.x, 0f, slowDownTime * Time.deltaTime);
        rb.velocity = new Vector2(speedX, rb.velocity.y);
    }
    #endregion

    #region Particles
    private void StopTrailParticles()
    {
        trailParticles.Stop();
    }

    private void PlayTrailParticles()
    {
        if (!trailParticles.isPlaying)
            trailParticles.Play();
    }

    private void PlayFallParticles()
    {
        bigFallParticles.Play();
    }
    #endregion
}
