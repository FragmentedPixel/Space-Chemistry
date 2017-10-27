using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollector : MonoBehaviour
{
    #region Variabiles
    // Number of particles need to sample substance.
    public float particlesNeeded = 15;

    // Currently soaked particles.
    private float currentParticles = 0;

    // Is the player collecting now ?
    private bool collecting = false;

    // The force at which the particles are attracted.
    public Effector2D particleSoaker;

    // The trigger for entering the collector.
    public Collider2D particleCollector;
    #endregion

    #region Collecting
    public float Collect()
    {
        if(collecting == false)
        {
            StartCollecting();
            return 0;
        }
        else
        {
            return currentParticles / particlesNeeded;
        }
    }

    private void StartCollecting()
    {
        collecting = true;
        particleSoaker.enabled = true;
        particleCollector.enabled = true;
        currentParticles = 0;
    }

    public void StopCollecting()
    {
        collecting = false;
        particleSoaker.enabled = false;
        particleCollector.enabled = false;
        currentParticles = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collecting)
            return;

        // Make sure to collect only 1 type of substance.
        Substance collectedSubstance = collision.GetComponent<Substance>();

        if(collectedSubstance != null)
        {
            currentParticles++;
            Destroy(collectedSubstance.gameObject);
        }

        //TODO: play sound when enough particles.
    }
    #endregion
}
