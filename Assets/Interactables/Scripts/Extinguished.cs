using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Requests a number of drops in order the extinguish the fire.
 */

public class Extinguished : MonoBehaviour
{
	// UI Heat meter of the object.
    public Image dropsBar;
    public sSubstance substanceNeeded;


	// Number of drops needed in order to extinguish the fire.
    public float dropsNeeded = 30;
	private float dropsAdded = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Particle substance = collision.GetComponent<Particle>();

        if(substance != null)
        {
			// Check if the current substance is water.
            if(substance.currentSubstance == substanceNeeded)
            {
                dropsAdded++;

				if (dropsAdded > dropsNeeded) 
				{
                    // Stop the fire if enough particles have hit.
					Destroy (gameObject);
				}
            }
        }
    }

    private void Update()
    {
        // Update the heat meter.
        dropsBar.fillAmount = (dropsNeeded - dropsAdded) / dropsNeeded;
    }
}
