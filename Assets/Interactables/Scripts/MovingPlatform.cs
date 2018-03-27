using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for moving the player between multiple points.
 */

public class MovingPlatform : MonoBehaviour 
{
    #region Variabiles
    public Transform wayPointsparent;
    public Transform arrow;
    public float arrowRotation = 30f;

	public float speed = 5f;
    public float maxSpeed = 3f;
	public float threshhold = .3f;
    public float angleOffset = 90f;

	private int currentIndex = 0;
    #endregion

    private void Update()
	{
        // Get the direction towards the current target destination.
		Vector3 direction =  wayPointsparent.GetChild (currentIndex).position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleOffset;

        // Slerping the current angle to the target angle.
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        arrow.rotation = Quaternion.Slerp(arrow.rotation, rotation, Time.deltaTime * arrowRotation);

        // Change the direction to the next  
        if (direction.magnitude < threshhold)
		{
			currentIndex = (currentIndex + 1) % wayPointsparent.childCount;
		}

        // Move the platform towards the current destination.
		else
		{

			direction = Vector3.ClampMagnitude(direction, speed);
            transform.position = Vector3.Lerp(transform.position, transform.position + direction, speed * Time.deltaTime);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Stick the player to the platform.
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
            player.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Remove the player from the platform.
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
            player.transform.SetParent(null);
    }
}
