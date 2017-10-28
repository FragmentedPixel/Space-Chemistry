using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSubstance : iSubstanceBehaviour
{
    [Header("Specific")]
    // How fast does the lava goes up?
    public float floatability = 7.0f;

    public override void BehaviourUpdate()
    {
        // Move the particle according to it's velocity and scale it down for burning effect.
        SubstanceFloating();
        ScaleDown();
    }

    public override State CollidingWith(State otherSubstance)
    {
        if (otherSubstance == State.WATER)
            return State.GAS;

        return State.NONE;
    }

    public void SubstanceFloating()
    {
        // fire always goes upwards

        if (rb.velocity.y < 50)
        {
            rb.AddForce(new Vector2(0, floatability));
        }
    }
}