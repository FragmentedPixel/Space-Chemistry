using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour 
{
	public Camera handCamera;
	public float speed = 5f;

	private void Update () 
	{
		Vector2 direction = handCamera.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;

		Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, speed * Time.deltaTime);
	}
}
