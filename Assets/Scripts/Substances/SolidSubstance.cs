using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSolid", menuName = "Substance/New Solid")]
public class SolidSubstance : sSubstance
{
    public override void BehaviourUpdate(Particle substanceScript)
    {
        // Solid particules shouldn't scale down or change orientation
    }
}
