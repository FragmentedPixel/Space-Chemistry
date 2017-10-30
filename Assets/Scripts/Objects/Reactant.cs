using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for creating reaction between particles and objects.
 */

public class Reactant : MonoBehaviour
{
    // Having similar reaction to this substance.
    public sSubstance reactantSubstance;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if a reaction is needed.
        Particle otherParticle = collision.gameObject.GetComponent<Particle>();

        if(otherParticle != null)
        {
            otherParticle.ReactWith(reactantSubstance);
        }
    }
}
