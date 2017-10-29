using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for generating particles from the player's hand.
 */ 

public class HandGenerator : MonoBehaviour 
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
	public bool Relase(sSubstance substanceToRelase)
	{
        if (nextRelease <= Time.time)
        {
            // It is time to spawn a new particle.

            // Create the new particle object.
            Particle newParticle = ParticlePool.instance.RequestParticle(substanceToRelase);

            // Update particle parameters.
            newParticle.ChangeSubstanceState(substanceToRelase);
            newParticle.rb.AddForce(transform.right * relaseForce);
            newParticle.transform.position = transform.position;
            
            // Set the timer.
            nextRelease = Time.time + releaseInterval;
            return true;
        }
        else
            return false;
        
	}
	#endregion
}
