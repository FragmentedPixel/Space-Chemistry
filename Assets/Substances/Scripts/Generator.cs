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

    // Should the particle persist?
    public bool persistentParticle;
    #endregion

    #region Update
    private void Update()
    {
        if (spawnTimer >= spawnInterval && particleSubstance != null)
        {
            // It is time to spawn a new particle.

            // Create the new particle object.
            Particle newParticle = ParticlePool.instance.RequestParticle(particleSubstance);

			Vector3 randomVector = randomForce * Random.onUnitSphere;

            // Update particle parameters.
			newParticle.rb.AddForce(particleForce + randomVector);
            newParticle.transform.position = transform.position;

            // Make particle persist.
            if(persistentParticle)
                newParticle.MakeInfiniteLifeTime();

            // Reset timer.
            spawnTimer = 0f; 	
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }
    }

    public void SetSubstance(sSubstance newSubst)
    {
        particleSubstance = newSubst;
    }
    #endregion
}
