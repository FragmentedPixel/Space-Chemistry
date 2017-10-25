using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Prototype class, please don't judge.
 */

public class Extinguished : MonoBehaviour
{
    public Image dropsBar;

    private float dropsAdded = 0;
    public float dropsNeeded = 30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Substance substance = collision.GetComponent<Substance>();

        if(substance != null)
        {
            if(substance.substanceState == State.WATER)
            {
                dropsAdded++;

                if (dropsAdded > dropsNeeded)
                    Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        dropsBar.fillAmount = (dropsNeeded - dropsAdded) / dropsNeeded;
    }
}
