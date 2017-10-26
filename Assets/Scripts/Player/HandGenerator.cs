using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGenerator : MonoBehaviour 
{

	#region Parameters
	// Interval between particles spawns.
	public float spawnInterval = 0.025f;

	//The last spawn time.
	private float spawnTimer = 0f; 

	// Initial Force of the particle at spawn.
	public float particleForce;

	// State of the particle generated.
	public State particlesState;
	#endregion

	#region Update
	private void Update()
	{
		if (spawnTimer >= spawnInterval)
		{
			// It is time to spawn a new particle.

			// Create the new particle object.
			GameObject newParticle = ParticlePool.instance.RequestParticle(particlesState);

			// Update particle parameters.
			newParticle.GetComponent<Rigidbody2D>().AddForce(transform.right * 100f*particleForce);
			newParticle.GetComponent<Substance>().ChangeSubstanceState(particlesState);
			newParticle.transform.position = transform.position;

			// Reset timer.
			spawnTimer = 0f; 	
		}
		else
		{
			spawnTimer += Time.deltaTime;
		}
	}
	#endregion
}
