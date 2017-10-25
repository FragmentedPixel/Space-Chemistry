using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour 
{
    public static ParticlePool instance;

	// Prefab for the particle instance.
	public GameObject substancePrefab;

	// Maximum particles at one moment.
	public int MAX_SUBSTANCES;

	public List<Substance> notInUse = new List<Substance>();
	public List<Substance> inUse = new List<Substance>();

	private void Start()
	{
        instance = this;

		for (int i = 0; i < MAX_SUBSTANCES; i++) 
		{
			GameObject substanceInstance = Instantiate (substancePrefab, transform);

			Substance substanceScript = substanceInstance.GetComponent<Substance> ();
			substanceScript.Deactivate ();

			notInUse.Add (substanceScript);
		}
	}

	public GameObject RequestParticle(State substanceState)
	{
		// If the list is empty return null.
		if (notInUse.Count <= 0) 
		{
			Debug.Log ("The object pool went over " + MAX_SUBSTANCES);
			return null;
		}

		else 
			//TODO: Check if remove at has better performance.
		{
			// Select the first particle.
			Substance substanceScript = notInUse [0];

			// Change the list.
			notInUse.Remove(substanceScript);
			inUse.Add (substanceScript);

			// Activate the substance.
			substanceScript.Activate (substanceState);

			return substanceScript.gameObject;
		}
	}

	public void ReturnParticle(GameObject substanceToReturn)
	{
		Substance substanceScript = substanceToReturn.GetComponent<Substance> ();
		substanceScript.Deactivate ();

		inUse.Remove (substanceScript);
		notInUse.Add (substanceScript);
	}

}
