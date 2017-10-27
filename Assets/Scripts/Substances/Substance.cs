using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All possible states of the particle.
public enum State { WATER, GAS, LAVA, ACID, NONE };

/*
 * Responsible for delegating the events to the current state.
 */

public class Substance : MonoBehaviour
{
    #region Variabiles

    // Current state of the substance.
    public State substanceState;

    // Time since the particle was created.
    private float currentLifeTime = 0f;

    // Total life time of the particle.
    private float totalLifeTime = 0f;

    // Current substance behaviour running.
    private iSubstanceBehaviour currentBehaviour;

    // All possible substances behaviours.
    private List<iSubstanceBehaviour> behaviourList = new List<iSubstanceBehaviour>();    
    #endregion

    #region State Specific
    private void Update()
    {
        currentBehaviour.BehaviourUpdate();
        
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
        Substance otherSubstance = collision.gameObject.GetComponent<Substance>();

        if (otherSubstance != null)
        {
            // Handle the collision between them in the current state.
            State otherState = otherSubstance.substanceState;
            State newState = currentBehaviour.CollidingWith(otherState);

            // Update current substance if the answer is not null.
            if (newState != State.NONE)
                ChangeSubstanceState(newState);
        }
    }
    #endregion

	#region Activation

	private void OnEnable()
	{
		// Reads all the behaviours on the substance.
		SetUpSubstancesBehavioursList ();
	}

	public void Activate(State newState)
	{
		ChangeSubstanceState (newState);
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
		GetComponent<Substance> ().enabled = isOn;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

		// Make particle invisible.
		transform.localScale = isOn ? Vector3.one : Vector3.zero;
	}

	#endregion

    #region Helper Methods

    private void SetUpSubstancesBehavioursList()
    {
        // Adds all the behaviours to the substance.
        foreach (Transform child in transform)
        {
            // Get Behaviour from child.
            iSubstanceBehaviour childSubstance = child.GetComponent<iSubstanceBehaviour>();

            // Check for null.
            if (childSubstance == null)
            {
                Debug.LogError("Non substance child on " + childSubstance.name);
            }
            else
            {
                // Add Behaviour to list.
                childSubstance.gameObject.SetActive(false);
                behaviourList.Add(childSubstance);
            }
        }
    }

    private void UpdateBehaviour()
    {
        // Reset life timer.
        currentLifeTime = 0.0f;
        
        // Set current Behaviour of to hide sprite.
        if (currentBehaviour != null)
            currentBehaviour.gameObject.SetActive(false);

        // Look for corresponding behaviour for the new state.
        foreach (iSubstanceBehaviour behaviour in behaviourList)
        {
            if (behaviour.particleState == substanceState)
            {
                // Set the new behaviour for update.
                currentBehaviour = behaviour;

                // Updates the gravity and layer.
                currentBehaviour.gameObject.SetActive(true);
                currentBehaviour.ActivateState();

                break;
            }
        }
    }
    
    public void ChangeSubstanceState(State newState)
    {
        substanceState = newState;
        UpdateBehaviour();
    }

    public float GetPercentagePassed()
    {
        return currentLifeTime/totalLifeTime;
    }

    public void UpdateTotalLifeTime(float newTotal)
    {
        totalLifeTime = newTotal;
    }

    #endregion

}
