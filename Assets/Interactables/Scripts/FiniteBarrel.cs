using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Responsible for holding the substance inside the barrel for a finite amnount.
 */

public class FiniteBarrel : MonoBehaviour
{
    #region Variabiles
    public int totalParticles;
    public int currentParticles;
    public sSubstance particleSubstance;

    public Image fillImage;
    #endregion

    #region Methods
    private void Start()
    {
        // Update the UI according to the substance inside the container.
        fillImage.color = particleSubstance.particleColor;
    }

    private void Update()
    {
        // Update the UI each frame to represent the substance inside.
        fillImage.fillAmount = (float) currentParticles / totalParticles;
    }

    public sSubstance GetParticle()
    {
        // Return the particle to the player if any.
        if(currentParticles >= 0)
        {
            currentParticles--;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;

            return particleSubstance;
        }

        else
        {
            MessageManager.getInstance().DissplayMessage("Container is empty.", 1f);
            return null;
        }
    }
    #endregion
}
