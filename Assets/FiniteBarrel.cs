using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiniteBarrel : MonoBehaviour
{
    public int totalParticles;
    public int currentParticles;
    public sSubstance particleSubstance;

    public Image fillImage;

    private void Start()
    {
        fillImage.color = particleSubstance.particleColor;
    }

    private void Update()
    {
        fillImage.fillAmount = (float) currentParticles / totalParticles;
    }

    public sSubstance GetParticle()
    {
        if(currentParticles >= 0)
        {
            currentParticles--;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;

            return particleSubstance;
        }

        else
        {
            //TODO: Display error when empty.
            return null;
        }
    }
}
