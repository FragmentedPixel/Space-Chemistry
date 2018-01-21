using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlatform : MonoBehaviour
{
    public float maxFallingSpeed = 1f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(rb.velocity.y < maxFallingSpeed)
        {
            rb.velocity = new Vector2(0f, maxFallingSpeed);
        }
    }
}
