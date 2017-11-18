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
    public Camera handCamera;

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
                Debug.LogWarning("Nu mergea inainte pentru aveai controller pus. Fuck you Mircea.");
            }
        }
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
            dir = handCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }

        if(dir != Vector2.zero)
        {
            MoveHand(dir);
        }
        
    }
    
    private void MoveHand(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Slerping the current angle to the target angle.
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
    #endregion
}
