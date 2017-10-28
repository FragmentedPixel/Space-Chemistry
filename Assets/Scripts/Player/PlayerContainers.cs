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
        SelectContainer();

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
            if(containersImage[currentIndex].fillAmount >= 1)
            {
                containers[currentIndex] = newSubstance;
            }
        }
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
        sSubstance particleSubstanceToRelease = containers[currentIndex];

        if (particleSubstanceToRelease != null)
            generator.Relase(particleSubstanceToRelease);
        else
            MessageManager.instance.DissplayMessage("Container is empty", 3f);
    }

    private void Collect()
    {
        float percent = collector.Collect();
        //TODO: Make option to get substance collected.

        containersImage[currentIndex].fillAmount = percent;

        containersImage[currentIndex].color = Color.Lerp(Color.red, Color.green, percent);
    }
}
