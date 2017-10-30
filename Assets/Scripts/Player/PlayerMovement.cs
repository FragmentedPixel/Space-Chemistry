using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for the player's movement.
 */

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
        //this can return -1 if you move to the left 0 if you don't move 1 if you move to the right
        float movement = Input.GetAxisRaw("Horizontal"); 

        // Get current speed of the rb.
        float vel = Mathf.Abs(myBody.velocity.x);

        // Stop if there is no movement input.
        if(movement == 0)
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

        // Upate the player's components.
        anim.SetBool(walkingHash, true);
        float forceX = right ? speed : -speed;
        myBody.AddForce(new Vector2(forceX, 0));
    }

    private void StopMove()
    {
        myBody.velocity = new Vector2(0f, myBody.velocity.x);
        anim.SetBool(walkingHash, false);
    }
    #endregion
}
