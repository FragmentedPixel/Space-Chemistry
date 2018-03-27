using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryTrigger : OneTimeSoundTrigger
{
    InvetoryUI invetoryUI;

    private void Start()
    {
        invetoryUI = FindObjectOfType<InvetoryUI>();
        invetoryUI.Deactivate();
    }

    protected override void OnPlayerTriggered()
    {
        invetoryUI.Activate();
    }
}
