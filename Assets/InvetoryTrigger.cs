using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryTrigger : PlayerTriggerable
{
    protected override void OnPlayerTriggered()
    {
        FindObjectOfType<InvetoryUI>().HighLight();
    }
}
