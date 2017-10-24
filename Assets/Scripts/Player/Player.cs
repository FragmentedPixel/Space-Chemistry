using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 10f;
    public float maxVelocity = 4f;

    private Rigidbody2D myBody;
    private Animator anim;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
		
	}

    void FixedUpdate()
    {
        
    }

	// Update is called once per frame
	void Update () {
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
                Vector3 temp = transform.localScale;
                temp.x = 0.3f;
                transform.localScale = temp;
                anim.SetBool("Walk", true);
            }
        }
        else if (movement < 0)
        {
            if (vel < maxVelocity)
            {
                forceX = -speed;
                Vector3 temp = transform.localScale;
                temp.x = -0.3f;
                transform.localScale = temp;
                anim.SetBool("Walk", true);
            }
        }
        else
        {
            forceX = 0f;
            anim.SetBool("Walk", false);
        }
        myBody.AddForce(new Vector2(forceX, 0));
    }
}
