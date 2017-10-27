using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContainers : MonoBehaviour
{
    // UI Reference to the containers.
    public Image[] containersImage = new Image[3];

    // State of the particles hold inside the containers.
    private State[] containers = new State[3];

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
    }

    private void SelectContainer()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentIndex = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            currentIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            currentIndex = 2;
    }

    private void Relase()
    {
        State particleToRelase = containers[currentIndex];

        generator.Relase(particleToRelase);
    }

    private void Collect()
    {
        float percent = collector.Collect();
        //TODO: Make option to get substance collected.

        containersImage[currentIndex].fillAmount = percent;
    }
}
