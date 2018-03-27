using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public Transform elevatorTarget;
    private Elevator elevator;
    private SpriteRenderer spRenderer;

    private void Start()
    {
        elevator = GetComponentInParent<Elevator>();
        spRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<HandCollector>() != null)
        {
            spRenderer.color = Color.red;
            elevator.MoveTo(elevatorTarget.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<HandCollector>() != null)
            spRenderer.color = Color.white;

    }


}
