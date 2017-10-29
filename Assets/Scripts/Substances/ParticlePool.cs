using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Particle pool responsible for performing the object pool for the particles in the scene.
 */

public class ParticlePool : MonoBehaviour 
{
    #region Variabiles
    // Singleton reference.
    public static ParticlePool instance;

	// Prefab for the particle instance.
	public GameObject particlePrefab;

	// Maximum particles at one moment.
	public int MAX_Particules;

    // Lists to hold particles.
	private List<Particle> notInUse = new List<Particle>();
	private List<Particle> inUse = new List<Particle>();
    #endregion

    #region Pool Methods
    private void Start()
	{
        // Setup singleton.
        instance = this;

        // Create particles for the pool.
		for (int i = 0; i < MAX_Particules; i++) 
		{
			GameObject substanceInstance = Instantiate (particlePrefab, transform);

			Particle substanceScript = substanceInstance.GetComponent<Particle> ();
			substanceScript.Deactivate ();

			notInUse.Add (substanceScript);
		}
	}

	public GameObject RequestParticle(sSubstance substanceRequested)
	{
		// If the list is empty return null.
		if (notInUse.Count <= 0) 
		{
			// Create a new substance for the empty list.
			GameObject substanceInstance = Instantiate (particlePrefab, transform);

			Particle substanceScript = substanceInstance.GetComponent<Particle> ();
			substanceScript.Deactivate ();

			notInUse.Add (substanceScript);

			// Debug to increase the number.
			Debug.Log ("The object pool went over " + MAX_Particules);
		}

        // Select the first particle.
		Particle substScript = notInUse [0];

		// Update the list.
		notInUse.Remove(substScript);
		inUse.Add (substScript);

		// Activate the substance.
		substScript.Activate (substanceRequested);

		return substScript.gameObject;
	}

	public void ReturnParticle(GameObject substanceToReturn)
	{
        // Access the particle's script to reset it for later use.
		Particle substanceScript = substanceToReturn.GetComponent<Particle> ();
		substanceScript.Deactivate ();

        // Update it;s position in the lists.
		inUse.Remove (substanceScript);
		notInUse.Add (substanceScript);
	}
    #endregion
}
