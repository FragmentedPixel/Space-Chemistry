using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public void Relase(State particleStateToRelase)
	{
        if (nextRelease <= Time.time)
        {
            // It is time to spawn a new particle.

            // Get the current state of the selected container.
            State particleState = particleStateToRelase;

            // Create the new particle object.
            GameObject newParticle = ParticlePool.instance.RequestParticle(particleState);

            // Update particle parameters.
            newParticle.GetComponent<Rigidbody2D>().AddForce(transform.right * relaseForce);
            newParticle.GetComponent<Substance>().ChangeSubstanceState(particleState);
            newParticle.transform.position = transform.position;

            // Set the timer.
            nextRelease = Time.time + releaseInterval;
        }	
	}
	#endregion
}
