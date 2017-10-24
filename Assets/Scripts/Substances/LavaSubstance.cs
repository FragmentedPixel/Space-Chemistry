using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSubstance : iSubstanceBehaviour
{
    public override void ActivateState()
    {
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        rb.gravityScale = particleGravity;

    }

    public override void Update()
    {
        MovementAnimation();
        ScaleDown();
    }

    public override State HandleSubstanceCollision(State otherSubstance)
    {
        if (otherSubstance == State.WATER)
            return State.GAS;

        return State.NONE;
    }

}