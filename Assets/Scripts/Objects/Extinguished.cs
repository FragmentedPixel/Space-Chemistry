using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Requests a number of drops in order the extinguish
 */

public class Extinguished : MonoBehaviour
{
	// UI Heat meter of the object.
    public Image dropsBar;

	// Number of drops needed in order to extinguish the fire.
    public float dropsNeeded = 30;
	private float dropsAdded = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Substance substance = collision.GetComponent<Substance>();

        if(substance != null)
        {
			// Check if the current substance is water.
            if(substance.substanceState == State.WATER)
            {
                dropsAdded++;

				if (dropsAdded > dropsNeeded) 
				{
					Destroy (gameObject);
				}
				else 
				{
					//TODO: Creat reaction with fire.
					Destroy (substance.gameObject);
				}
            }

			else if(substance.substanceState == State.LAVA)
			{
				//TODO: If lava hits container, increase it.
				//dropsAdded--;
				//Destroy (substance.gameObject);
			}
        }
    }

    private void Update()
    {
        dropsBar.fillAmount = (dropsNeeded - dropsAdded) / dropsNeeded;
    }
}
