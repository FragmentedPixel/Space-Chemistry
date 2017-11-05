using System.Collections;
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
            if (!audioS.isPlaying)
            {
                Debug.Log("Sound Play");
                audioS.loop = true;
                audioS.clip = collectSound;
                audioS.Play();
            }
            Collect();
        }
        else if(Input.GetMouseButtonUp(1))
        {
            if (audioS.isPlaying)
            {
                audioS.loop = false;
                audioS.Stop();
                audioS.PlayOneShot(endCollect);
            }

            UpdateSubstanceColor();
        }
        else if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            Mix();
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
                //audioS.PlayOneShot(endCollect);
            }
        }
        else
        {
            if (!audioS.isPlaying)
            {
               // audioS.PlayOneShot(collectSound);
            }
        }

        //Set the current particles in the container
        containers[currentIndex].particules = currentparticles;
        //Change the UI accordingly
        UpdateContainersPercent();
        
    }

    private void Mix()
    {
        //TODO: Change highlight better.

        containers[currentIndex].StopHighLigh();

        if(containers[0].substance == null && containers[0].particules == 0)
        {
            currentIndex = 0;
            MixContainers(1, 2); 
        }

        else if (containers[1].substance == null && containers[1].particules == 0)
        {
            currentIndex = 1;
            MixContainers(0, 2);
        }

        else if (containers[2].substance == null && containers[2].particules == 0)
        {
            currentIndex = 2;
            MixContainers(0, 1);
        }
        else
        {
            MessageManager.getInstance().DissplayMessage("All containers are full.", 1.5f);
        }

        containers[currentIndex].HighLight();
    }

    private void MixContainers(int firstContainer, int secondContainer)
    {
        
        sSubstance result = containers[firstContainer].substance.CollidingWith(containers[secondContainer].substance);
        if(result == null)
        {
            MessageManager.getInstance().DissplayMessage("Substances can't mix", 1f);
        }
        else
        {
            //TODO: Implement containers logics.
            int resultPart = containers[firstContainer].particules + containers[secondContainer].particules;

            containers[firstContainer].particules = 0;
            containers[secondContainer].particules = 0;


            containers[firstContainer].fillImage.fillAmount = 0;
            containers[secondContainer].fillImage.fillAmount = 0;

            containers[firstContainer].substance = null;
            containers[secondContainer].substance = null;

            containers[currentIndex].fillImage.color = result.particleColor;
            containers[currentIndex].particules = resultPart > capacity ? capacity : resultPart;
            containers[currentIndex].fillImage.fillAmount = containers[currentIndex].particules / capacity;
            containers[currentIndex].substance = result;

        }
    }

    private void UpdateContainersPercent()
    {
        float newPercent =(float)containers[currentIndex].particules / capacity;
        //Color newColor = Color.Lerp(Color.red, Color.green, newPercent);
        if (containers[currentIndex].substance == null)
            containers[currentIndex].substance = collector.GetCurrentSsubstance();
		if (containers[currentIndex].substance != null)
        containers[currentIndex].UpdateContainer(containers[currentIndex].substance.particleColor,newPercent);
    }
    
    #endregion
}
