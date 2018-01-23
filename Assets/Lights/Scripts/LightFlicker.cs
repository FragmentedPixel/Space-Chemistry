using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public Light thelight; 
	// Use this for initialization
	void Start ()
    {
        thelight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
         
	}
}
