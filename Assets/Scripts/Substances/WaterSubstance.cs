using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSubstance : iSubstanceBehaviour
{
    [Header("Specific")]
    public float chestieSpecifica;

    public override void ActivateState()
    {
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        rb.gravityScale = particleGravity;
    }

    public override void Update()
    {
        ScaleDown();
    }

    public override State HandleSubstanceCollision(State otherSubstance)
    {
        if (otherSubstance == State.LAVA)
            return State.GAS;

        return State.NONE;
    }


}