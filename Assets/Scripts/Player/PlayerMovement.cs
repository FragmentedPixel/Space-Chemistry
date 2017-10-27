using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // The movement speed of the player.
    public float speed = 10f;

    // Maximum speed of the player.
    public float maxVelocity = 4f;
    
    private Rigidbody2D myBody;
    private Animator anim;
    private const string walkingHash = "Walk";

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

        if(movement == 0)
        {
            StopMove();
        }

        else if(vel < maxVelocity)
        {
            Move(movement > 0);
        }
    }

    private void Move(bool right)
    {
        Vector3 temp = anim.transform.localScale;
        temp.x = right ? 0.3f :  -0.3f;

        anim.transform.localScale = temp;
        anim.SetBool(walkingHash, true);

        float forceX = right ? speed : -speed;
        myBody.AddForce(new Vector2(forceX, 0));
    }

    private void StopMove()
    {
        myBody.velocity = Vector2.zero;
        anim.SetBool(walkingHash, false);
    }

}
