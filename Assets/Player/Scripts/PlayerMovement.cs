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
    private Rigidbody2D myBody;
    private Animator anim;
    private const string walkingHash = "Walk";

    private bool grounded = false;

    public Transform groundCheck;

    private float groundRadius = 0.5f;

    public float jumpForce = 5f;

    private float jumpMult = 0f;

    public float jumpForcePerSecond = .1f;

    public LayerMask whatisGround;
    #endregion

    #region Methods
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
    }

	void Update () 
	{
        PlayerMoveKeyboard();
	}

    void PlayerMoveKeyboard()
    {
        grounded = isGrounded();

        Jump();

        //this can return -1 if you move to the left 0 if you don't move 1 if you move to the right
        float movement = Input.GetAxisRaw("Horizontal"); 

        // Get current speed of the rb.
        float vel = Mathf.Abs(myBody.velocity.x);

        // Stop if there is no movement input.
        if (movement == 0)
        {
            StopMove();
        }

        // Update the player rigidbody according to the user input.
        else if(vel < maxVelocity)
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
        myBody.velocity = (new Vector2(forceX, myBody.velocity.y));
    }

    private void StopMove()
    {
        myBody.velocity = new Vector2(0f, myBody.velocity.y);
        anim.SetBool(walkingHash, false);
    }

    public bool isGrounded()
    {
        float distanceToGround = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, -Vector2.up, distanceToGround, whatisGround);

        return (hit.point != Vector2.zero);
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Jump")))
            jumpMult = 1f;

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetButton("Jump")))
            jumpMult += jumpForcePerSecond * Time.deltaTime;

        if (grounded && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetButtonUp("Jump")))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce * jumpMult));
        }
    }
    #endregion
}
