using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public Light light;

    public float speed;
    public float noise;

	// Use this for initialization
	void Start ()
    {
        light = GetComponent<Light>();
        StartCoroutine(Flicker());

    }


    IEnumerator Flicker()
    {
        light.enabled = true;
        float randNoise = Random.Range(-1, 1) * Random.Range(-noise, noise);
        yield return new WaitForSeconds(speed + randNoise);
        light.enabled = false;
        yield return new WaitForSeconds(speed);
        StartCoroutine(Flicker());
    }
}
