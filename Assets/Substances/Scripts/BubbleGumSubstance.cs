using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BubbleGum", menuName = "Substance/New BubbleGum")]
public class BubbleGumSubstance : sSubstance
{
    public override void BehaviourUpdate(Particle substanceScript)
    {
        Solidify(substanceScript);
        ScaleUp(substanceScript);
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

    public void ScaleUp(Particle substanceScript)
    {
        // The effect for the particle to seem to fade away
        float percentagePassed = substanceScript.GetPercentagePassed();

        // Calculate scale depending on life time left.
        float scaleValue = 1.0f + percentagePassed / 2;
        Vector2 particleScale = Vector2.one;

        // Update the particle scale.
        particleScale.x = scaleValue;
        particleScale.y = scaleValue;
        substanceScript.ScaleDownSprite(particleScale);
    }
}
