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
    [HideInInspector] public bool collecting = false;

    // The currently filled container.
    private Container containerToFill;
    
    // The force at which the particles are attracted.
    public Effector2D particleSoaker;

    // The trigger for entering the collector.
    public Collider2D particleCollector;

    // The particle system emitted when collectiong.
    public ParticleSystem particleFeedback;
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
        bool success = false;
        Color collectedColor = Color.green;

        if (collecting == false)
            return;

        FiniteBarrel barrel = collision.gameObject.GetComponent<FiniteBarrel>();
        if(barrel != null)
        {
            sSubstance particleInsideBarrel = barrel.CheckParticle();

            if (particleInsideBarrel != null)
            {
                success = containerToFill.AddParticule(particleInsideBarrel);
                if (success)
                    barrel.GetParticle();
                else
                    MessageManager.getInstance().DissplayMessage("There is a problem with this barrel", 1f);

            }
            else
                MessageManager.getInstance().DissplayMessage("There is no substance inside the barrel", 1f);
        }

        // Get information about the current collision.
        Particle collectedParticle = collision.GetComponent<Particle>();

        //Escape collisions with ground or other objects or the players is not collecting now.
        if (collectedParticle !=  null)
        {
            // Add the substance to the current container sent.
            success = containerToFill.AddParticule(collectedParticle.currentSubstance);

            // Destory particle if collected.
            if (success)
            {
                collectedColor = collectedParticle.currentSubstance.particleColor;
                Destroy(collectedParticle.gameObject);
            }

        }

        if (success)
        {
            particleFeedback.startColor = collectedColor;

            var vel = particleFeedback.velocityOverLifetime;

            PlayerMovement player = FindObjectOfType<PlayerMovement>();

            Vector3 direction = player.transform.position - particleFeedback.transform.position;
            direction = direction.normalized * 10f;

            vel.x = direction.x;
            vel.y = direction.y;
            vel.z = direction.z;
            

            particleFeedback.Play();
        }
    }
    #endregion
}
