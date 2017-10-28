using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLiquid", menuName = "Substance/New Liquid")]
public class LiquidSubstance : sSubstance
{
    public override void BehaviourUpdate(Particle substanceScript)
    {
        // Let gravity handle the movement only scale the particle down and move it accordingly.
        ScaleDown(substanceScript);
    }
}
