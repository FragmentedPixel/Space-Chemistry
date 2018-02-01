using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Transform door;
    public float openSpeed = 3f;
    public float openDistance;

    private bool opneningDoor;

    private Vector3 closePosition;
    private Vector3 openPosition;

    private void Start()
    {
        closePosition = door.transform.position;
        openPosition = door.transform.position + Vector3.right * openDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if(player != null)
        {
            opneningDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            opneningDoor = false;
        }
    }

    private void Update()
    {
        Vector3 lappedPosition;

        if(opneningDoor)
        {
            lappedPosition = Vector3.Lerp(door.transform.position, openPosition, openSpeed * Time.deltaTime);
        }
        else
        {
            lappedPosition = Vector3.Lerp(door.transform.position, closePosition, openSpeed * Time.deltaTime);
        }

        door.transform.position = lappedPosition;
    }

    public void Opendoor()
    {
        opneningDoor = true;
    }


}
