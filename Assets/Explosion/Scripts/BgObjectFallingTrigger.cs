using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObjectFallingTrigger : OneTimeSoundTrigger
{
    public GameObject FallingObjectPrefab;
    public Transform WhereToSpawn;

    protected override void OnPlayerTriggered()
    {
        Instantiate(FallingObjectPrefab, WhereToSpawn.position, FallingObjectPrefab.transform.rotation, transform);
    }
}
