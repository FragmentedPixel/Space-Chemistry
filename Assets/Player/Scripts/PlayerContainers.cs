﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Responsible for managing the player's containers.
 */

public class PlayerContainers : MonoBehaviour
{
    #region Variabiles
    // UI Reference to the containers.
    public UIContainer[] containers = new UIContainer[3];

    // Container capacity
    public int capacity = 15;

    // currently selected container.
    private int currentIndex = 0;

    // Hand subclasses.
    private HandGenerator generator;
    private HandCollector collector;

    // Sounds
    private AudioSource audioS;
    public AudioClip releaseSound;
    public AudioClip collectSound;
    public AudioClip endCollect;
    #endregion

    #region Methods

    private void Start()
    {
        generator = GetComponentInChildren<HandGenerator>();
        collector = GetComponentInChildren<HandCollector>();
        audioS = GetComponent<AudioSource>();

        containers[currentIndex].HighLight();
    }

    private void UpdateSubstanceColor()
    {
        sSubstance newSubstance = collector.StopCollecting();
        containers[currentIndex].substance = newSubstance;

        if(newSubstance != null)
        {
          containers[currentIndex].UpdateContainerColor(newSubstance.particleColor);
        }
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
            UpdateSubstanceColor();
        }
        else
        {
            // Can't change containers while performing any actions.
            SelectContainer();
        }
    }

    private void SelectContainer()
    {
        // Check for new input.
        int input = -1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            input = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            input = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            input = 2;

        //Change the highlighted container according to input.
        if(input != -1)
        {
            containers[currentIndex].StopHighLigh();
            currentIndex = input;
            containers[currentIndex].HighLight();
        }
    }

    private void Relase()
    {
        //Check if container is empty
        if (containers[currentIndex].particules <= 0)
        {
            MessageManager.getInstance().DissplayMessage("Container is empty", 1f);
            containers[currentIndex].substance = null;
        }
        else
        {
            sSubstance particleSubstanceToRelease = containers[currentIndex].substance;

            if (particleSubstanceToRelease != null)
            {
                //Set the current particles in the container
                bool goodRelease = generator.Relase(particleSubstanceToRelease);

                if (goodRelease)
                    containers[currentIndex].particules--;

                UpdateContainersPercent();
            }

            if (!audioS.isPlaying)
            {
                audioS.PlayOneShot(releaseSound);
            }
        }
    }

    private void Collect()
    {
        int currentparticles = collector.Collect(containers[currentIndex].particules, containers[currentIndex].substance);

        if(currentparticles>=capacity)
        {
            sSubstance newSubstance = collector.StopCollecting();
            containers[currentIndex].substance = newSubstance;
            currentparticles = capacity;
            if(!audioS.isPlaying)
            {
                audioS.PlayOneShot(endCollect);
            }
        }
        else
        {
            if (!audioS.isPlaying)
            {
                audioS.PlayOneShot(collectSound);
            }
        }

        //Set the current particles in the container
        containers[currentIndex].particules = currentparticles;
        //Change the UI accordingly
        UpdateContainersPercent();
        
    }

    private void UpdateContainersPercent()
    {
        float newPercent =(float)containers[currentIndex].particules / capacity;
        Color newColor = Color.Lerp(Color.red, Color.green, newPercent);

        containers[currentIndex].UpdateContainer(newColor,newPercent);
    }
    
    #endregion
}