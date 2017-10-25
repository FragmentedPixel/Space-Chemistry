using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSubstance : iSubstanceBehaviour
{
    public override void BehaviourUpdate()
    {
        // Move the particle according to it's velocity and scale it down for burning effect.
        MovementAnimation();
        ScaleDown();
    }

    public override State CollidingWith(State otherSubstance)
    {
        if (otherSubstance == State.WATER)
            return State.GAS;

        return State.NONE;
    }

    //TODO: Add randomness to mimic fire.
}