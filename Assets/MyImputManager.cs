using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyImputManager : MonoBehaviour {

	public static bool connectedToController=false;
	Vector3 lastMouseCoordinates;

	// Use this for initialization
	void Start () 
	{
		lastMouseCoordinates = Input.mousePosition;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		bool controlermoved = false;
		for (int i = 0; i < 20; i++)
		{
			if (Input.GetKeyDown("joystick 1 button " + i))
			{
				controlermoved = true;
			}
		}
        for (int i = 0; i < 19; i++)
        {
            if (Input.GetKeyDown("Joystick 1 axis " + i))
            {
                controlermoved = true;
            }
        }

        if (lastMouseCoordinates != Input.mousePosition)
		{
			lastMouseCoordinates = Input.mousePosition;
			connectedToController = false;
		}
		if (controlermoved)
		{
			connectedToController = true;

		}
		Cursor.visible = !connectedToController;	
	}
}
