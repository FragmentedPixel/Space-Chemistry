using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;
    public float maxVelocity = 4f;

    private Rigidbody2D myBody;
    private Animator anim;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
    }

	// Update is called once per frame
	void Update () 
	{
        PlayerMoveKeyboard();
	}

    void PlayerMoveKeyboard()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);

        float movement = Input.GetAxisRaw("Horizontal"); //this can return -1 if you move to the left 0 if you don't move 1 if you move to the right

        if (movement > 0)
        {
            if (vel < maxVelocity)
            {
                forceX = speed;
				Vector3 temp = anim.transform.localScale;
                temp.x = 0.3f;
				anim.transform.localScale = temp;
                anim.SetBool("Walk", true);
            }
        }
        else if (movement < 0)
        {
            if (vel < maxVelocity)
            {
                forceX = -speed;
				Vector3 temp = anim.transform.localScale;
                temp.x = -0.3f;
				anim.transform.localScale = temp;
                anim.SetBool("Walk", true);
            }
        }
        else
        {
            forceX = 0f;
			myBody.velocity = Vector2.zero;
            anim.SetBool("Walk", false);
        }
        myBody.AddForce(new Vector2(forceX, 0));
    }
}
