using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSubstance : iSubstanceBehaviour
{
    [Header("Specific")]
    // How fast does the gas goes up?
    public float floatability = 7.0f;

    public override void Update()
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
        //TODO: Refactor this.

        if (GetComponentInParent<Rigidbody2D>().velocity.y < 50)
        {
            //Limits the speed in Y to avoid speed light.
            // Gas always goes upwards
            GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, floatability));
        }
    }
}
