using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Goo", menuName = "Substance/New Goo")]
public class GooSubstance : sSubstance
{
    [Header("Goo Base")]
    [Tooltip("Time spent in air before being solid.")]
    public float solidifyTime;

    public override void BehaviourUpdate(Particle substanceScript)
    {
        Solidify(substanceScript);
    }

    protected void Solidify(Particle substanceScript)
    {
        // The effect for the particle to seem to fade away
        float percentagePassed = substanceScript.GetPercentagePassed();

        // Calculate scale depending on life time left.
        float scaleValue = percentagePassed;

        // Change the Alpha of the substance.
        Color substColor = substanceScript.rend.material.color;
        substColor.a = scaleValue + .5f;
        substanceScript.rend.material.color = substColor;

        // Solidifies the substance if enough time has passed.
        float currentLifeTime = substanceScript.GetCurrentLifeTime();
        if (currentLifeTime < solidifyTime)
        {
            substanceScript.rb.gravityScale = 1 - scaleValue * 5;
        }

        else
        {
            substanceScript.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }
}
