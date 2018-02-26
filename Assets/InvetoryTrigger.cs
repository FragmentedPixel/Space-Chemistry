using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryTrigger : PlayerTriggerable
{
    private void Start()
    {
        FindObjectOfType<InvetoryUI>().Hide();
    }

    protected override void OnPlayerTriggered()
    {
        FindObjectOfType<InvetoryUI>().HighLight();
    }
}
