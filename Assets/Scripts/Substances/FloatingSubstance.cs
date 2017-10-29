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
        Rigidbody2D particleRb = substanceScript.GetComponent<Rigidbody2D>();
        GasFloating(particleRb);
        ScaleDown(substanceScript);
    }

    private void GasFloating(Rigidbody2D rb)
    {
        // Gas always goes upwards
        if (rb.velocity.y < 50)
        {
            rb.AddForce(new Vector2(0, floatability));
        }
    }


}
