using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for transferring the player to the next level when reached.
 */

public class Door : PlayerTriggerable
{
    public string NextLevel;

    protected override void OnPlayerTriggered()
    {
        var persistent = FindObjectOfType<PersistentContainers>();
        if (persistent != null)
        {
            FindObjectOfType<PersistentContainers>().LevelEnded();
        }

        FindObjectOfType<LevelManager>().ChangeScene(NextLevel);
    }
}
