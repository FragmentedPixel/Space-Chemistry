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
		
		bool controolermoved = false;
		for (int i = 0; i < 20; i++)
		{
			if (Input.GetKeyDown("joystick 1 button " + i))
			{
				controolermoved = true;
			}
		}

		if (lastMouseCoordinates != Input.mousePosition)
		{
			lastMouseCoordinates = Input.mousePosition;
			connectedToController = false;
		}
		if (controolermoved)
		{
			connectedToController = true;

		}
		Cursor.visible = !connectedToController;	
	}
}
