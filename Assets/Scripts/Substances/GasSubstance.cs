using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSubstance : iSubstanceBehaviour
{
    [Header("Specific")]
    // How fast does the gas goes up?
    public float floatability = 7.0f;

    public override void BehaviourUpdate()
    {
        GasFloating();
        ScaleDown();
    }

    public override State CollidingWith(State otherSubstance)
    {
        return State.NONE;
    }

    private void GasFloating()
    {
        // Gas always goes upwards

        if (rb.velocity.y < 50)
        {
            rb.AddForce(new Vector2(0, floatability));
        }
    }
}
