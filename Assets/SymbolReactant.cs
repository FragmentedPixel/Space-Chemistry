using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolReactant : MonoBehaviour
{
    // Having similar reaction to this substance.
    public sSubstance reactantSubstance;
    public SpriteRenderer reactionSymbol;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        // Check if a reaction is needed.
        Particle otherParticle = collider.gameObject.GetComponent<Particle>();

        if (otherParticle != null)
        {
            ChangeAlpha(1f);
        }
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        // Check if a reaction is needed.
        Particle otherParticle = collider.gameObject.GetComponent<Particle>();

        if (otherParticle != null)
        {
            ChangeAlpha(.5f);
        }
    }

    private void ChangeAlpha(float newValue)
    {
        Color newColor = reactionSymbol.color;
        newColor.a = newValue;
        reactionSymbol.color = newColor;
    }
}
