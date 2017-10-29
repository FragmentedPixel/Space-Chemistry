using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContainers : MonoBehaviour
{
    // UI Reference to the containers.
    public Image[] containersImage = new Image[3];

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

    private void Start()
    {
        generator = GetComponentInChildren<HandGenerator>();
        collector = GetComponentInChildren<HandCollector>();
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
            //TODO: Update Container Color.
            sSubstance newSubstance = collector.StopCollecting();
            
            containers[currentIndex] = newSubstance; 
        }
        else
            SelectContainer(); //Nu poti schimba containerul cat timp elimini/colectezi
    }

    private void SelectContainer()
    {
        //TODO: Update selected Container color.

        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentIndex = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            currentIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            currentIndex = 2;
    }

    private void Relase()
    {
        //Check if container is empty
        if (particles[currentIndex] == 0)
        {
            MessageManager.instance.DissplayMessage("Container is empty", 3f);
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
        if(currentparticles>=particlesNeeded)  //Dont go over the maximum capacity
        {
            sSubstance newSubstance = collector.StopCollecting();
            containers[currentIndex] = newSubstance;
            currentparticles = particlesNeeded;
        }
        //TODO: Make option to get substance collected.

        //Set the current particles in the container
        particles[currentIndex] = currentparticles;
        //Change the UI accordingly
        ChangeContainerImage();
        
    }

    private void ChangeContainerImage()
    {
        float percent =(float) particles[currentIndex] / particlesNeeded;

        containersImage[currentIndex].fillAmount = percent;

        containersImage[currentIndex].color = Color.Lerp(Color.red, Color.green, percent);

    }
}
