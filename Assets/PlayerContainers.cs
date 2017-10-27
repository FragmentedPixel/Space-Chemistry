using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainers : MonoBehaviour
{
    // State of the particles hold inside the containers.
    public State[] containers = new State[3];

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

        // Update UI.
    }
}
