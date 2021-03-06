﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Update the color of the pipe according to the substance's color.
 */

public class Pipe : MonoBehaviour
{
    // Set's the sprite renderer color to the generator specific color.
	private void Start ()
    {
        SubstanceGenerator particleGenerator = GetComponentInChildren<SubstanceGenerator>();
        SpriteRenderer rend = GetComponentInChildren<SpriteRenderer>();

        Color substanceColor = particleGenerator.particleSubstance.particleColor;
        rend.color = substanceColor;
	}
	
}
