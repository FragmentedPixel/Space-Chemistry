using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BubbleGum", menuName = "Substance/New BubbleGum")]
public class BubbleGumSubstance : GooSubstance
{
    public override void BehaviourUpdate(Particle substanceScript)
    {
        Solidify(substanceScript);
        ScaleUp(substanceScript);
    }
}
