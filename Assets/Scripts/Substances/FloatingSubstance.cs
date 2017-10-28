using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGas", menuName = "Substance/New Gas")]
public class FloatingSubstance : sSubstance
{

    [Header("Specific")]
    // How fast does the gas goes up?
    public float floatability = 7.0f;

    public override void BehaviourUpdate(Particle substanceScript)
    {
        //TODO: Remove get Component.
        GasFloating(substanceScript.GetComponent<Rigidbody2D>());
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
