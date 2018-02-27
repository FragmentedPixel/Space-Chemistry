using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public Sprite[] imagesToDisplay;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null && triggered == false)
        {
            triggered = true;
            PopUpUI.instance.RequestPopUp(imagesToDisplay);
        }
    }
}
