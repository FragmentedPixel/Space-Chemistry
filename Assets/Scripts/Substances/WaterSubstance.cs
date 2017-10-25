using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSubstance : iSubstanceBehaviour
{
    public override void BehaviourUpdate()
    {
		// Let gravity handle the movement only scale the particle down and move it accordingly.
		ScaleDown ();

    	// TODO: Check if needed.
		//MovementAnimation ();
	}

    public override State CollidingWith(State otherSubstance)
    {
        if (otherSubstance == State.LAVA)
            return State.GAS;

        return State.NONE;
    }
}
