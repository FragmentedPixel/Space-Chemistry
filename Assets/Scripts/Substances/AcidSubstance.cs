﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSubstrance : iSubstanceBehaviour {

	// Use this for initialization
    public override void BehaviourUpdate()
    {
        // Let gravity handle the movement only scale the particle down and move it accordingly.
        particleLifeTime = 10.0f;

        ScaleDown();
    }

    public override State CollidingWith(State otherSubstance)
    {
        if (otherSubstance == State.WATER)
            return State.GAS;

        return State.NONE;
    }
}