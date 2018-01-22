using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeEvent : MonoBehaviour
{

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if(player != null && triggered == false)
        {
            triggered = true;
            ActionHappening();
        }
    }

    public virtual void ActionHappening()
    {
        Debug.Log("It happend.");
    }
}
