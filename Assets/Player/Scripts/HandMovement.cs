using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Rotates the hand of the player towards the mouse cursor.
 */

public class HandMovement : MonoBehaviour 
{
    // Camera from where the raycast is done.
	public Camera handCamera;

    // Speed of the rotation.
	public float speed = 5f;

	private void Update () 
	{
        // Calculating the angle from raycast.
		Vector2 direction = handCamera.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;

        // Slerping the current angle to the target angle.
        Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, speed * Time.deltaTime);
	}
}
