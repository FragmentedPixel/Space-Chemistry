using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for collecting substances particles for the player.
 */

public class HandCollector : MonoBehaviour
{
    #region Variabiles
    // Is the player collecting now ?
    private bool collecting = false;

    // The currently filled container.
    private Container containerToFill;
    
    // The force at which the particles are attracted.
    public Effector2D particleSoaker;

    // The trigger for entering the collector.
    public Collider2D particleCollector;
    #endregion

    #region Collecting
    public bool Collect(Container _containerToFill)
    {
        containerToFill = _containerToFill;
        bool isFull = containerToFill.isFull();

        if (!isFull)
            StartCollecting();
        else
            StopCollecting();

        return !isFull;
    }

    private void StartCollecting()
    {
        collecting = true;
        particleSoaker.enabled = true;
        particleCollector.enabled = true;
    }

    public void StopCollecting()
    {
        collecting = false;
        particleSoaker.enabled = false;
        particleCollector.enabled = false;
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collecting == false)
            return;

        FiniteBarrel barrel = collision.gameObject.GetComponent<FiniteBarrel>();
        if(barrel != null)
        {
            //TODO: Remove particle only when successful.
            //TODO: Make error with add particle with null check.
            sSubstance particleInsideBarrel = barrel.GetParticle();
            containerToFill.AddParticule(particleInsideBarrel);
        }

        // Get information about the current collision.
        Particle collectedParticle = collision.GetComponent<Particle>();

        //Escape collisions with ground or other objects or the players is not collecting now.
        if (collectedParticle !=  null)
        {
            // Add the substance to the current container sent.
            bool success = containerToFill.AddParticule(collectedParticle.currentSubstance);

            // Destory particle if collected.
            if (success)
            {
                Destroy(collectedParticle.gameObject);
            }

        }
    }
    #endregion
}
