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
    // Reference to the containers.
    //TODO: Implement containers manager.
    public Container[] containers = new Container[3];

    // currently selected container.
    private int currentIndex = 0;

    // Hand subclasses.
    private HandGenerator generator;
    private HandCollector collector;
    private ContanterMixer mixer;

    // Sounds.
    private AudioSource audioS;
    public AudioClip releaseSound;
    public AudioClip collectSound;
    public AudioClip endCollect;
    #endregion

    #region Methods

    private void Start()
    {
        // Get components from the player.
        generator = GetComponentInChildren<HandGenerator>();
        collector = GetComponentInChildren<HandCollector>();
        mixer = GetComponentInChildren<ContanterMixer>();

        audioS = GetComponent<AudioSource>();
        containers[currentIndex].HighLight();
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
            StopCollecting();
        }

        else if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            Mix();
        }

        else
        {
            SelectContainer();
        }

        UpdateContainersPercent();

    }

    private void Relase()
    {
        // PLay corresponding sound.
        // TODO: Check this.
        if (!audioS.isPlaying)
        {
            audioS.loop = true;
            audioS.clip = releaseSound;
            audioS.Play();
        }

        // Get the substance from the current container.
        sSubstance particleSubstanceToRelease = containers[currentIndex].ReleaseParticule();

        // Release a particle of the substance.
        if (particleSubstanceToRelease != null)
        {
            generator.Relase(particleSubstanceToRelease);
        }
    }
    

    private void Collect()
    {
        // Play corresponding sound.
        //TODO: Check if done correct.
        if (!audioS.isPlaying)
        {
            audioS.loop = true;
            audioS.clip = collectSound;
            audioS.Play();
        }

        collector.Collect(containers[currentIndex]);
    }

    private void StopCollecting()
    {
        if (audioS.isPlaying)
        {
            //TODO: Remove this.
            audioS.loop = false;
            audioS.Stop();
            audioS.PlayOneShot(endCollect);
        }

        collector.StopCollecting();
    }

    private void SelectContainer()
    {

        if (audioS.isPlaying)
        {
            audioS.loop = false;
            //audioS.clip = reelase sound;
            audioS.Stop();
        }

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

    private void Mix()
    {
        mixer.Mix(containers[0], containers[1], containers[3]);
    }

    private void UpdateContainersPercent()
    {
        foreach (Container container in containers)
            container.UpdateContainerUI();
    }
    
    #endregion
}
