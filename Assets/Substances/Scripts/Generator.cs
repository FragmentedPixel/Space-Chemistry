using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for generating particles at a specific interval.
 */

public class Generator : MonoBehaviour
{
    #region Parameters
    // Interval between particles releases.
    public float releaseInterval = 0.025f;

    //The last release time.
    private float nextRelease = 0f;

    // Initial Force of the particle at spawn.
    public float relaseForce;
    #endregion

    #region Release
    public Particle CreateSubstance(sSubstance substanceToRelase)
    {
        if (nextRelease <= Time.time && substanceToRelase != null)
        {
            // It is time to spawn a new particle.

            // Create the new particle object.
            Particle newParticle = ParticlePool.instance.RequestParticle(substanceToRelase);

            // Update particle parameters.
            newParticle.transform.position = transform.position;
            newParticle.ChangeSubstanceState(substanceToRelase);
            newParticle.rb.AddForce(transform.right * relaseForce);

            // Set the timer.
            nextRelease = Time.time + releaseInterval;

            return newParticle;
        }
        else
        {
            return null;
        }
    }
    #endregion
}
