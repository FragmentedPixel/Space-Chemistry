using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool teleportPlayerOnly = true;
    public Checkpoint[] checkpoints;
    public Light spotLight;
    public Transform destination;

    private void Update()
    {
        if (AreAllCheckpointsPassed())
            spotLight.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(AreAllCheckpointsPassed() == true)
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if(player != null)
            {
                collision.transform.position = destination.position;
                FindObjectOfType<LevelLoading>().StartLevel();
            }
            else if(teleportPlayerOnly == false)
            {
                collision.transform.position = destination.position;
            }

        }
    }

    private bool AreAllCheckpointsPassed()
    {
        if (checkpoints.Length == 0)
            return true;

        foreach (Checkpoint checkPoint in checkpoints)
            if (checkPoint.passed == false)
                return false;

        return true;
    }
}
