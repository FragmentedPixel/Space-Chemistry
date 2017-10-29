using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Specific data container for Floating Substances.
 */

[CreateAssetMenu(fileName = "NewGas", menuName = "Substance/New Gas")]
public class FloatingSubstance : sSubstance
{
    [Header("Specific")]
    // How fast does the floating substance go up?
    public float floatability = 15.0f;

    public override void BehaviourUpdate(Particle substanceScript)
    {
        GasFloating(substanceScript);
        ScaleDown(substanceScript);
    }

    private void GasFloating(Particle substanceScript)
    {
        Rigidbody2D rb = substanceScript.rb;

        // Gas always goes upwards
        if (rb.velocity.y < 50)
        {
            rb.AddForce(new Vector2(0, floatability));
        }
    }


}
