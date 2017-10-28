using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for delegating the events to the current state.
 */

public class Particle : MonoBehaviour
{
    #region Variabiles
    // All possible substances behaviours.

    //TODO: Add them from substance data scriptable object.
    public List<sSubstance> behaviourList = new List<sSubstance>();

    // Current state of the substance.
    public sSubstance currentSubstance;

    // Time since the particle was created.
    private float currentLifeTime = 0f;

    // Total life time of the particle.
    private float totalLifeTime = 0f;

    // Components
    private Rigidbody2D rb;
    #endregion

    #region State Specific
    private void Update()
    {
        // Runs the update on the scriptable object.
        currentSubstance.BehaviourUpdate(this);
        
        // Return the particle to the pool when it dies.
        if(currentLifeTime > totalLifeTime)
        {
            ParticlePool.instance.ReturnParticle(gameObject);
        }

        else
        {
            currentLifeTime += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // The the other substance script.
        Particle otherParticle = collision.gameObject.GetComponent<Particle>();

        if (otherParticle != null)
        {
            // Handle the collision between them in the current state.
            sSubstance otherSubstance = otherParticle.currentSubstance;
            ReactWith(otherSubstance);
        }
    }

    public void ReactWith(sSubstance otherSubstance)
    {
        // Run the collision on the current substance scriptable object.
        sSubstance newSubstance = currentSubstance.CollidingWith(otherSubstance);

        // Update current substance if the answer is not null.
        if (newSubstance != null)
            ChangeSubstanceState(newSubstance);
    }
    #endregion

	#region Activation

	private void OnEnable()
	{
        rb = GetComponent<Rigidbody2D>();
	}

	public void Activate(sSubstance newSubstance)
	{
		ChangeSubstanceState (newSubstance);
		ChangeActiveState (true);
	}

	public void Deactivate()
	{
		ChangeActiveState (false);
	}

	private void ChangeActiveState(bool isOn)
	{
		// Disable Components.
		GetComponent<Collider2D> ().enabled = isOn;
		GetComponent<Particle> ().enabled = isOn;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

		// Make particle invisible.
		transform.localScale = isOn ? Vector3.one : Vector3.zero;
	}

	#endregion

    #region Helper Methods
    public void ChangeSubstanceState(sSubstance newState)
    {
        currentSubstance = newState;
        rb.velocity = Vector2.zero;

        // Update the total life time.
        totalLifeTime = newState.particleLifeTime;

        // Set the gravity for this particle.
        rb.gravityScale = newState.particleGravity;

        // Move it to the corresponding layer.
        gameObject.layer = (int)newState.particleLayer;

        GetComponent<MeshRenderer>().material = newState.particleMaterial;
    }

    public float GetPercentagePassed()
    {
        return currentLifeTime/totalLifeTime;
    }
    #endregion
}
