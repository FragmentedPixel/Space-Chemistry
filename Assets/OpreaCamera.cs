using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpreaCamera : MonoBehaviour
{
    public Shader opreaShader;

	// Use this for initialization
	void Start () {
        GetComponent<Camera>().RenderWithShader(opreaShader, "");		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
