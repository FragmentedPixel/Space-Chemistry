using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Goo", menuName = "Substance/New Goo")]
public class GooSubstance : sSubstance
{
    public override void BehaviourUpdate(Particle substanceScript)
    {
        Solidify(substanceScript);
    }

    private void Solidify(Particle substanceScript)
    {
        // The effect for the particle to seem to fade away
        float percentagePassed = substanceScript.GetPercentagePassed();

        // Calculate scale depending on life time left.
        float scaleValue = percentagePassed;

        Color substColor = substanceScript.GetComponentInChildren<SpriteRenderer>().material.color;
        substColor.a = scaleValue + .5f;
        substanceScript.GetComponentInChildren<SpriteRenderer>().material.color = substColor;

        if (scaleValue * 5 < 1)
            substanceScript.GetComponent<Rigidbody2D>().gravityScale = 1 - scaleValue * 5;
        else
        {
            substanceScript.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }
}
