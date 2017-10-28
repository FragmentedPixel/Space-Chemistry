using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidReactant : Reactant
{
    protected override void ReactWith(Substance otherState)
    {
        otherState.ReactWith(reactantState);
    }
    
}
