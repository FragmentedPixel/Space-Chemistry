﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInstructions : MonoBehaviour
{
    public Canvas collectingCanvas;

    private List<Canvas> instructions = new List<Canvas>();

    // Components
    private PlayerContainers containers;

    private void Start()
    {
        containers = transform.parent.GetComponentInChildren<PlayerContainers>();
        instructions.Add(collectingCanvas);
    }

    private void ShowCanvas(Canvas canvasToShow)
    {
        foreach (Canvas instruction in instructions)
            instruction.enabled = false;

        canvasToShow.enabled = true;
    }

    private void HideCanvas(Canvas canvasToHide)
    {
        canvasToHide.enabled = false;
    }

    public void ShowCollectingCanvas()
    {
        ShowCanvas(collectingCanvas);
    }

    public void HideCollectingCanvas()
    {
        HideCanvas(collectingCanvas);
    }

    public bool CanCollect()
    {
        return containers.CanCollect() ;
    }
}
