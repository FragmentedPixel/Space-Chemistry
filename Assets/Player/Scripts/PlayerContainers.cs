using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Responsible for managing the player's containers.
 */

public enum ContainersState
{
    Collecting, Releasing, Mixing, Idle
}

public class PlayerContainers : PlayerContrable
{
    #region Variabiles
    // Reference to the containers.
    public ContainersState state;
    private Container[] containers = new Container[3];

    // currently selected container.
    private int currentIndex = 0;
    // was the change triggered already?
    private bool axisTriggered = false;

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

    // Enum state

    #endregion

    #region Containers & UI
    private void Start()
    {
        // Set the containers from the UI.
        SetContainers();

        // Get components from the player.
        generator = GetComponentInChildren<HandGenerator>();
        collector = GetComponentInChildren<HandCollector>();
        audioS = GetComponent<AudioSource>();
        mixer = new ContanterMixer();
    }

    private void SetContainers()
    {
        containers = new Container[containersCount];
        ContainersUI containerManager = FindObjectOfType<ContainersUI>();
        Container[] uiContainers = containerManager.GetContainers(containersCount);

        for(int i = 0; i < containersCount; i++)
            containers[i] = uiContainers[i];

        containers[currentIndex].HighLight();
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

    #region Super Class
    // Action performed when control is removed.
    public override void RemoveControl()
    {
        StopCollecting();
    }
    #endregion

    #region Commands
    private void Update()
    {
        if (MyImputManager.connectedToController)
            ReadControllerInput();
        else
            ReadInputKeyboard();

    }

    private void ReadInputKeyboard()
    {
        if (Input.GetButtonUp("Collect"))
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

        else if (Input.GetButtonDown("Mix"))
        {
            Mix();
        }

        else
        {
            SelectContainer();
        }

        UpdateContainersPercent();
    }

    private void ReadControllerInput()
    {
        float axisInput = Input.GetAxisRaw("Container Axis");
        
        if (axisInput == 0f && axisTriggered == true)
        {
            StopCollecting();
        }

        else if (axisInput < 0f)
        {
            Collect();
        }

        else if (axisInput > 0f)
        {
            Relase();
        }

        else if (Input.GetButtonDown("Mix"))
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
        state = ContainersState.Releasing;
        axisTriggered = true;

        if (availableContainers == 0)
            return;

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
        state = ContainersState.Collecting;
        axisTriggered = true;
        if (availableContainers == 0)
            return;

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
        state = ContainersState.Idle;
        axisTriggered = false;

        if (audioS.isPlaying && audioS.loop == true)
        {
            audioS.loop = false;
            audioS.Stop();
            audioS.PlayOneShot(endCollect);
        }

        collector.StopCollecting();
    }

    private void SelectContainer()
    {
        state = ContainersState.Idle;

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
        if (Input.GetButtonDown("Change Container"))
            input = (currentIndex + 1) % availableContainers;

        //Change the highlighted container according to input.
        if (input != -1)
        {
            containers[currentIndex].StopHighLight();
            currentIndex = input;
            containers[currentIndex].HighLight();
        }

        containers[currentIndex].OnStay();
    }

    private void Mix()
    {
        state = ContainersState.Mixing;

        if (containersCount == 3)
            mixer.Mix(containers[0], containers[1], containers[2]);
        else if (containersCount == 2)
            mixer.Mix(containers[0], containers[1]);
    }
    #endregion

    private bool AreContainersAvaible()
    {
        ContainersUI containersUI = FindObjectOfType<ContainersUI>();
        return containersUI.AreContainersAvailable();
    }

    public bool CanCollect()
    {
        return (state != ContainersState.Releasing && AreContainersAvaible() == true);
    }
}
