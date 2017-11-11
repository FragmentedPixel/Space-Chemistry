using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Update the color of the pipe according to the substance's color.
 */

public class Pipe : MonoBehaviour
{

	private void Start ()
    {
        Generator particleGenerator = GetComponentInChildren<Generator>();
        SpriteRenderer rend = GetComponentInChildren<SpriteRenderer>();

        Color substanceColor = particleGenerator.particleSubstance.particleColor;
        rend.color = substanceColor;
	}
	
}
