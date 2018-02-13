﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePath : MonoBehaviour
{
    public float power = 5f;
    public GameObject arrowPrefab;

    float force = 4.0f;
    int samples = 25;
    float spacing = 0.1f;  // Time between samples 

    private Vector3 offset;
    private Vector3 home;
    private GameObject[] argo;
 
    private Vector3 velocity = Vector3.zero;
    private bool freeze = false;


    // Use this for initialization
    void Start ()
    {
        home = transform.position;
        argo = new GameObject[samples];
        for (var i = 0; i < argo.Length; i++)
        {
            var go = Instantiate(arrowPrefab, transform.position, transform.rotation, transform);
            argo[i] = go;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        DisplayIndicators();
    }


    void ShowHideIndicators(bool show)
    {
        for (var i = 0; i < argo.Length; i++)
        {
            argo[i].GetComponent<SpriteRenderer>().enabled = show;
            argo[i].transform.position = home;
        }
    }

    void DisplayIndicators()
    {
        Vector3 indicator;

        if (!MyImputManager.connectedToController)
        {
            Vector3 handPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            handPosition.z = transform.position.z;
            indicator = (handPosition - transform.position).normalized * power;
        }
        else
        {
			indicator = transform.right * power;
        }

        argo[0].GetComponent<SpriteRenderer>().enabled = false;
        var v3 = transform.position;
        var y = (force * (indicator)).y;
        float t = 0.0f;
        v3.y = 0.0f;
        for (var i = 1; i < argo.Length; i++)
        {
            v3 += force * (indicator) * spacing;
            t += spacing;
            v3.y = y * t + 0.5f * Physics.gravity.y * t * t + transform.position.y;
            v3.z = transform.parent.position.z;
            argo[i].transform.position = v3;
        }

        for(int i = 1; i < argo.Length-1; i++)
        {
            argo[i].transform.rotation = Quaternion.LookRotation(argo[i - 1].transform.position - argo[i].transform.position, transform.forward);
        }
    }
}



