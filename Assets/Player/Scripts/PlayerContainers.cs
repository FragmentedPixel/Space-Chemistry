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
    private Container[] containers = new Container[3];

    // currently selected container.
    private int currentIndex = 0;
    // was the change triggered already?
    private bool changeTriggered = false;

    // Hand subclasses.
    private HandGenerator generator;
    private HandCollector collector;
    private ContanterMixer mixer;
    private int containersCount = 2;

    //Tutorial variable
    public static int availableContainers = 2;

    // Sounds.
    private AudioSource audioS;
    public AudioClip releaseSound;
    public AudioClip collectSound;
    public AudioClip endCollect;
    #endregion

    #region Containers & UI
    private void Start()
    {
        // Set the containers from the UI.
        SetContainers();

        // Get components from the player.
        generator = GetComponentInChildren<HandGenerator>();
        collector = GetComponentInChildren<HandCollector>();
        mixer = GetComponentInChildren<ContanterMixer>();
        audioS = GetComponent<AudioSource>();
    }

    private void SetContainers()
    {
        containers = new Container[containersCount];
        ContainersManager containerManager = FindObjectOfType<ContainersManager>();
        Container[] uiContainers = containerManager.GetContainers(containersCount);

        for(int i = 0; i < containersCount; i++)
            containers[i] = uiContainers[i];

        containers[currentIndex].HighLight();
    }

    public void ChangeControl(bool hasControl)
    {
        if (hasControl == false)
            StopCollecting();

        enabled = hasControl;
    }

    private void UpdateContainersPercent()
    {
        foreach (Container container in containers)
            container.UpdateContainerUI();
    }

    public Container GetCurrentContainer()
    {
        return containers[currentIndex];
    }
    #endregion

    #region Commands
    private void Update()
    {
        if(Input.GetButtonUp("Collect"))
        {
            StopCollecting();
        }

        else if (Input.GetButton("Collect"))
        {
            Collect();
        }

        else if (Input.GetButton("Release"))
        {
            Relase();
        }

        else if(Input.GetButtonDown("Mix"))
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
        // Play corresponding sound.
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
            generator.Release(particleSubstanceToRelease);
        }
    }




    private void Collect()
    {
        // Check if the current container can collect.
        bool canCollect = collector.Collect(containers[currentIndex]);

        if(canCollect)
        {
            // Play corresponding sound.
            if (!audioS.isPlaying || audioS.clip != collectSound)
            {
                audioS.loop = true;
                audioS.clip = collectSound;
                audioS.Play();
            }
        }
        else
        {
            MessageManager.getInstance().DissplayMessage("Container is full.", 1f);
            StopCollecting();
        }
        

    }

    private void StopCollecting()
    {
        if (audioS.isPlaying)
        {
            audioS.loop = false;
            audioS.Stop();
            audioS.PlayOneShot(endCollect);
        }

        collector.StopCollecting();
    }

    private void SelectContainer()
    {
        collector.StopCollecting();


        // Stop sounds played.
        if (audioS.isPlaying)
        {
            audioS.loop = false;
            audioS.Stop();
        }

        // Check for new input.
        int input = -1;

        // Keyboard Input.
        if (Input.GetKeyDown(KeyCode.Alpha1))
            input = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2) && availableContainers >= 2)
            input = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3) && availableContainers >= 3)
            input = 2;
        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            input = (currentIndex + 1) % availableContainers;

        // Read controller Input.
        int controllerInput = (int) Input.GetAxisRaw("Container Axis");

        // Change input & lock axis after change.
        if (controllerInput != 0f && changeTriggered == false)
        {
            input = (currentIndex + controllerInput) % availableContainers;

            if (input < 0)
            {
                input = containersCount - 1;
            }

            changeTriggered = true;
        }

        // Reset Axis
        if(controllerInput == 0)
        {
            changeTriggered = false;
        }

        //Change the highlighted container according to input.
        if(input != -1)
        {
            containers[currentIndex].StopHighLight();
            currentIndex = input;
            containers[currentIndex].HighLight();
        }

        containers[currentIndex].OnStay();
    }

    private void Mix()
    {
        if (containersCount == 3)
            mixer.Mix(containers[0], containers[1], containers[2]);
        else if (containersCount == 2)
            mixer.Mix(containers[0], containers[1]);
    }
    #endregion
}
