using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{

	public Transform target;

	public float smoothSpeed = 12.5f;
	public Vector3 offset;

	void FixedUpdate()
	{
		if (target == null) 
		{
			target = FindObjectOfType<PlayerHealth> ().transform;
			offset = transform.position - target.position;
		}

		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);

		transform.position = smoothedPosition;
	}

}
