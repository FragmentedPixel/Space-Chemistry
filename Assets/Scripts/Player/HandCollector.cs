using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for collecting substances particles for the player.
 */

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
        // Get information about the current collision.
        Particle collectedSubstance = collision.GetComponent<Particle>();
     
        //Escape collisions with ground or other objects.
        if (collectedSubstance ==  null) 
            return;

        // The players is not collecting now.
        if (!collecting)
            return;


        if(currentSubstance == null)
        {
            currentSubstance = collectedSubstance.currentSubstance;
        }

        // Make sure to collect the same type of substance.
        else if (currentSubstance!=collectedSubstance.currentSubstance)             
        {
            MessageManager.instance.DissplayMessage("You can't collect this substance", 3f);
            return;
        }

        if(collectedSubstance != null)
        {
            currentParticles++;
            Destroy(collectedSubstance.gameObject);
        }

    }
    #endregion
}
