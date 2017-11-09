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
    public void Collect(Container _containerToFill)
    {
        containerToFill = _containerToFill;
        StartCollecting();
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
        // Get information about the current collision.
        Particle collectedParticle = collision.GetComponent<Particle>();

        //Escape collisions with ground or other objects or the players is not collecting now.
        if (collectedParticle ==  null || collecting == false ) 
            return;


        // Add the substance to the current container sent.
        bool success = containerToFill.AddParticule(collectedParticle.currentSubstance);

        // Destory particle if collected.
        if (success)
        {
            Destroy(collectedParticle.gameObject);
        }
        
    }
    #endregion
}
