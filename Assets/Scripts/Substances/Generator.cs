using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for generating particles at a specific interval.
 */

public class Generator : MonoBehaviour
{
    #region Parameters
    // Interval between particles spawns.
    public float spawnInterval = 0.025f;

    //The last spawn time.
    private float spawnTimer = 0f;

	// Magnitude of the random force applied at the spawn.
	public float randomForce;
    
    // Initial Force of the particle at spawn.
    public Vector3 particleForce;

    // State of the particle generated.
    public sSubstance particleSubstance;
    #endregion

    #region Update
    private void Update()
    {
        if (spawnTimer >= spawnInterval)
        {
            // It is time to spawn a new particle.

            // Create the new particle object.
            GameObject newParticle = ParticlePool.instance.RequestParticle(particleSubstance);

			Vector3 randomVector = randomForce * Random.onUnitSphere;
           
            // Update particle parameters.
			newParticle.GetComponent<Rigidbody2D>().AddForce(particleForce + randomVector);
            newParticle.GetComponent<Particle>().ChangeSubstanceState(particleSubstance);
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
