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

    // Movement sound.
    public AudioClip[] movementSounds;

    // audio source.
    private AudioSource audioS;

    // Speed of the rotation.
	public float speed = 5f;

    public float rotationoffset = 20f;


    // Is reading Input from controller.

    private void Start()
    {
        audioS = gameObject.AddComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume()/3;

        handCamera = CameraManager.cameraManager.GetHandCamera();

        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            if (names[x].Length == 33)
            {
				MyImputManager.connectedToController = true;
            }
        }

    }
    #endregion

    #region Update
    private void Update()
	{
        

        Vector2 dir = Vector2.zero;

		if(MyImputManager.connectedToController)
        {
            dir = new Vector2(Input.GetAxis("Hand X"), Input.GetAxis("Hand Y"));
        }

        else
        {   
            Ray ray = handCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 result = ray.GetPoint((transform.position.z - handCamera.transform.position.z) / ray.direction.z);

            dir = result - transform.position;
        }
        if (dir == Vector2.zero)
            return;
        if (transform.parent.localScale.x > 0)
        {
            MoveHand(dir);
        }
        else if (transform.parent.localScale.x < 0)
        {
            MoveHand(-dir);
        }
            
    }
    
    private void MoveHand(Vector2 direction)
    {
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		angle += MyImputManager.connectedToController ? (0f) : (rotationoffset);

        // Slerping the current angle to the target angle.
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);


        if (!audioS.isPlaying && Quaternion.Angle(rotation, transform.rotation) > 2f)
        {
            int index = Random.Range(0, movementSounds.Length);
            //audioS.PlayOneShot(movementSounds[index]);
        }
    }

    public void ChangeControl(bool hasControl)
    {
        enabled = hasControl;
    }
    #endregion
}
