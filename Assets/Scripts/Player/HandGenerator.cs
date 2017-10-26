using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGenerator : MonoBehaviour 
{

	#region Parameters
	// Interval between particles releases.
	public float relaseInvterval = 0.025f;

	//The last relase time.
	private float relaseTimer = 0f; 

	// Initial Force of the particle at spawn.
	public float relaseForce;

	// currently selected container.
	public int currentState = 0;

	// State of the particles hold inside the containers.
	public State[] containers = new State[3];

	#endregion

	#region Update

	private void Update()
	{
		SelectContainer ();

		if (Input.GetMouseButton (0)) 
		{
			Relase ();
		}
		else if(Input.GetMouseButton(1))
		{
			Collect ();
		}
	}

	private void SelectContainer()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1))
			currentState = 0;
		else if (Input.GetKeyDown (KeyCode.Alpha2))
			currentState = 1;
		else if (Input.GetKeyDown (KeyCode.Alpha3))
			currentState = 2;
	}

	private void Relase()
	{
		if (relaseTimer >= relaseInvterval) 
		{
			// It is time to spawn a new particle.

			// Get the current state of the selected container.
			State particleState = containers [currentState];

			// Create the new particle object.
			GameObject newParticle = ParticlePool.instance.RequestParticle (particleState);

			// Update particle parameters.
			newParticle.GetComponent<Rigidbody2D> ().AddForce (transform.right * relaseForce);
			newParticle.GetComponent<Substance> ().ChangeSubstanceState (particleState);
			newParticle.transform.position = transform.position;

			// Reset timer.
			relaseTimer = 0f; 	
		} 

		else 
		{
			relaseTimer += Time.deltaTime;
		}	

	}

	private void Collect()
	{
		
	}

	#endregion
}
