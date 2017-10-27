using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollector : MonoBehaviour
{
    // Number of particles need to sample substance.
    public int particlesNeeded = 15;

    private int currentParticles = 0;

    // Is the player collecting now ?
    private bool collecting = false;

    private void Update()
    {
        if(Input.GetMouseButton(1) && currentParticles < particlesNeeded)
        {
            collecting = true;
        }

        else
        {
            collecting = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: Make sure it is the small collider.
        if (!collecting)
            return;

        Substance collectedSubstance = collision.GetComponent<Substance>();

        if(collectedSubstance != null)
        {
            Debug.Log(currentParticles);
            currentParticles++;
            Destroy(collectedSubstance.gameObject);
        }
    }
}
