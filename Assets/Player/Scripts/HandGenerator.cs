using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for generating particles from the player's hand.
 */ 

public class HandGenerator : Generator
{
    public void Release(sSubstance substanceToCreate)
    {
        Particle newParticle = CreateSubstance(substanceToCreate);
    }
}
