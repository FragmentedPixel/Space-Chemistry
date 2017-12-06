using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Rotates the hand of the player towards the mouse cursor or controller.
 */

public class HandMovement : MonoBehaviour 
{
    #region Parameters
    // Camera from where the raycast is done.
    private Camera handCamera;

    // Speed of the rotation.
	public float speed = 5f;

    // Is reading Input from controller.
    private bool connectedToController = false;

    private void Start()
    {
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            if (names[x].Length == 33)
            {
                connectedToController = true;
            }
        }

        handCamera = CameraManager.cameraManager.GetHandCamera();
    }
    #endregion

    #region Update
    private void Update()
	{
        Vector2 dir = Vector2.zero;

        if(connectedToController)
        {
            dir = new Vector2(Input.GetAxis("Hand X"), Input.GetAxis("Hand Y"));
        }

        else
        {   
            Ray ray = handCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 result = ray.GetPoint((transform.position.z - handCamera.transform.position.z) / ray.direction.z);

            dir = result - transform.position;
        }

        if (transform.parent.parent.localScale.x > 0)
        {
            MoveHand(dir);
        }
        else if (transform.parent.parent.localScale.x < 0)
        {
            MoveHand(-dir);
        }
            
    }
    
    private void MoveHand(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Slerping the current angle to the target angle.
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }

    public void ChangeControl(bool hasControl)
    {
        enabled = hasControl;
    }
    #endregion
}
