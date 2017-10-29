using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollector : MonoBehaviour
{
    #region Variabiles

    // Currently soaked particles.
    private int currentParticles = 0;

    // Is the player collecting now ?
    private bool collecting = false;

    // The substance the player is now collecting.
    private sSubstance currentSubstance;

    // The force at which the particles are attracted.
    public Effector2D particleSoaker;

    // The trigger for entering the collector.
    public Collider2D particleCollector;
    #endregion

    #region Collecting
    public int Collect(int particles,sSubstance currentSubstance)
    {
        
        
        if(collecting == false)
        {
            this.currentSubstance = currentSubstance;
            currentParticles = particles;
            StartCollecting();
        }
        return currentParticles;
    }

    private void StartCollecting()
    {
        collecting = true;
        particleSoaker.enabled = true;
        particleCollector.enabled = true;
    }

    public sSubstance StopCollecting()
    {
        collecting = false;
        particleSoaker.enabled = false;
        particleCollector.enabled = false;
        currentParticles = 0;
        
        return currentSubstance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Escape colisions with ground or other objects
        if (!collision.GetComponent<Particle>()) 
            return;

        if (!collecting)
            return;

        // Get information about the current collision
        Particle collectedSubstance = collision.GetComponent<Particle>();


        if(currentSubstance == null)
        {
            currentSubstance = collectedSubstance.currentSubstance;
        }
        else if(currentSubstance!=collectedSubstance.currentSubstance)             // Make sure to collect the same type of substance.
        {
            MessageManager.instance.DissplayMessage("You can't collect this substance", 3f);
            return;
        }

        if(collectedSubstance != null)
        {

            //TODO: Make getter method.
                currentParticles++;
                Destroy(collectedSubstance.gameObject);
        }

        //TODO: play sound when enough particles.
    }
    #endregion
}
