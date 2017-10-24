using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All possible states of the particle.
public enum State { WATER, GAS, LAVA, NONE };

public class Substance : MonoBehaviour
{
    #region Variabiles

    // Current state of the substance.
    public State substanceState;

    // Current substance behaviour running.
    private iSubstanceBehaviour currentBehaviour;

    // All possible substances behaviours.
    private List<iSubstanceBehaviour> behaviourList = new List<iSubstanceBehaviour>();

    // Time since the particle was created.
    public float currentLifeTime = 0f;
    
    #endregion

    #region Initialization

    private void Start()
    {
        SetUpSubstancesBehavioursList();
        UpdateBehaviour();
    }

    #endregion

    #region State Specific
    private void Update()
    {
        currentBehaviour.Update();

        if(currentLifeTime > currentBehaviour.particleLifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            currentLifeTime += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentBehaviour.OnCollisionEnter2D(collision);
    }
    #endregion

    #region Helper Methods

    private void SetUpSubstancesBehavioursList()
    {
        // Reset life timer.
        currentLifeTime = 0.0f;

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
    
    public void UpdateSubstanceState(State newState)
    {
        substanceState = newState;
        UpdateBehaviour();
    }

    #endregion
}
