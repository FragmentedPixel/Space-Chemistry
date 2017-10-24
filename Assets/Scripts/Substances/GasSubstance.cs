using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSubstance : iSubstanceBehaviour
{
    [Header("Specific")]
    // How fast does the gas goes up?
    public float floatability = 7.0f;

    public override void ActivateState()
    {
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        rb.gravityScale = particleGravity;

        //TODO: Add layer.
    }

    public override void Update()
    {
        //TODO: Refactor this.
        if (GetComponentInParent<Rigidbody2D>().velocity.y < 50)
        {
            //Limits the speed in Y to avoid reaching mach 7 in speed
            GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, floatability)); // Gas always goes upwards
        }

        ScaleDown();
    }
    
}
