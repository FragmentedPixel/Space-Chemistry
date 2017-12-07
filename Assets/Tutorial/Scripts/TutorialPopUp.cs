using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUp : MonoBehaviour
{
    public GameObject tutorialPanelToShow;

    private void Start()
    {
        tutorialPanelToShow.SetActive(false);
    }

    private bool isPlayer(GameObject go)
    {
        PlayerMovement player = go.GetComponent<PlayerMovement>();
        return (player != null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPlayer(collision.gameObject))
        {
            tutorialPanelToShow.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isPlayer(collision.gameObject))
        {
            tutorialPanelToShow.SetActive(false);
        }
    }
}
