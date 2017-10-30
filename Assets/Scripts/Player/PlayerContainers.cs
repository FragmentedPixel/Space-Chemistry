using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Responsible for managing the player's containers.
 */

//TODO: play sound when enough particles.

public class PlayerContainers : MonoBehaviour
{
    #region Variabiles
    // UI Reference to the containers.
    public UIContainer[] containersImage = new UIContainer[3];

    // State of the particles hold inside the containers.
    public sSubstance[] containers = new sSubstance[3];

    // Container capacity
    public int particlesNeeded = 15;

    //Number of particles in the corresponding container
    private int[] particles= { 0, 0, 0 };

    // currently selected container.
    private int currentIndex = 0;

    // Hand subclasses.
    private HandGenerator generator;
    private HandCollector collector;
    #endregion

    #region Methods

    private void Start()
    {
        generator = GetComponentInChildren<HandGenerator>();
        collector = GetComponentInChildren<HandCollector>();

        containersImage[currentIndex].HighLight();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Relase();
        }
        else if (Input.GetMouseButton(1))
        {
            Collect();
        }
        else if(Input.GetMouseButtonUp(1))
        {
            sSubstance newSubstance = collector.StopCollecting();
            containers[currentIndex] = newSubstance;

            //TODO: Improve this one.
            containersImage[currentIndex].UpdateContainerColor(newSubstance.particleColor);
        }
        else
        {
            // Can't change containers while performing any actions.
            SelectContainer();
        }
    }

    private void SelectContainer()
    {
        int input = -1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            input = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            input = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            input = 2;

        if(input != -1)
        {
            containersImage[currentIndex].StopHighLigh();
            currentIndex = input;
            containersImage[currentIndex].HighLight();
        }
    }

    private void Relase()
    {
        //Check if container is empty
        if (particles[currentIndex] == 0)
        {
            MessageManager.getInstance().DissplayMessage("Container is empty", 3f);
            containers[currentIndex] = null;
            return;
        }
            
        sSubstance particleSubstanceToRelease = containers[currentIndex];

        if (particleSubstanceToRelease != null)
        {
            bool goodRelease=generator.Relase(particleSubstanceToRelease);
            //Set the current particles in the container
            if(goodRelease)
                particles[currentIndex]--;
            //Change the UI accordingly
            ChangeContainerImage();
        }
    }

    private void Collect()
    {
        int currentparticles = collector.Collect(particles[currentIndex],containers[currentIndex]);
        if(currentparticles>=particlesNeeded)
        {
            sSubstance newSubstance = collector.StopCollecting();
            containers[currentIndex] = newSubstance;
            currentparticles = particlesNeeded;
        }

        //Set the current particles in the container
        particles[currentIndex] = currentparticles;
        //Change the UI accordingly
        ChangeContainerImage();
        
    }

    private void ChangeContainerImage()
    {
        float newPercent =(float) particles[currentIndex] / particlesNeeded;
        Color newColor = Color.Lerp(Color.red, Color.green, newPercent);

        containersImage[currentIndex].UpdateContainer(newColor,newPercent);
    }
    
    #endregion
}
