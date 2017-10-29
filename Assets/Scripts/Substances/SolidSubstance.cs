using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Specific data container for Solid Substances.
 */

[CreateAssetMenu(fileName = "NewSolid", menuName = "Substance/New Solid")]
public class SolidSubstance : sSubstance
{
    public override void BehaviourUpdate(Particle substanceScript)
    {
        // Solid particles shouldn't scale down or change orientation
    }
}
