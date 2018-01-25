using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewSubstance", menuName = "Substance/New Basic Substance")]

[Serializable]
public class sSubstance : ScriptableObject
{
    // Layer of the particle
    public enum SubstanceLayer { Fluids = 8, Floating = 9, Solid = 10  };

    #region Variabiles
    [Header("General")]
    public ReactionsTable reactionTable;

    public Material baseMaterial;

    [Header("Specific")]

    // Layer of the particle
    public SubstanceLayer particleLayer;

    // Gravity particle
    public float particleGravity;

    // Material of the particle.
    public Material particleMaterial;

    // How much time before the particle scales down and dies
    public float particleLifeTime = 3.0f;

    // Color of the particle.
    public Color particleColor;

    // List of all reaction with this substance.
    private List<ReactionEq> reactions;
    #endregion

    #region Specific
    private void OnEnable()
    {
        reactions = reactionTable.GetReactionsFor(this);
        Material mat = new Material(baseMaterial);
        particleMaterial = mat;
        particleMaterial.color = particleColor;
    }

    public sSubstance CollidingWith(sSubstance otherSubstance)
    {
        for(int i = 0; i < reactions.Count; i++)
        {
            if (reactions[i].second == otherSubstance)
                return reactions[i].result;
        }

        return null;
    }

    public virtual void BehaviourUpdate(Particle substanceScript)
    {
        Debug.Log("Running Update on base class.");
    }

    #endregion

    #region Movement & Scale

    public void MovementAnimation(Transform transform, Rigidbody2D rb)
    {
        // This scales the particle image according to its velocity, so it looks like its deformable

        Vector3 movementScale = new Vector3(1.0f, 1.0f, 1.0f);
        movementScale.x += Mathf.Abs(rb.velocity.x) / 30.0f;
        movementScale.z += Mathf.Abs(rb.velocity.y) / 30.0f;
        movementScale.y = 1.0f;

        transform.localScale = movementScale;
    }

    public void ScaleDown(Particle substanceScript)
    {
        // The effect for the particle to seem to fade away
        float percentagePassed = substanceScript.GetPercentagePassed();

        // Calculate scale depending on life time left.
        float scaleValue = 1.0f - percentagePassed;
        Vector2 particleScale = Vector2.one;

        // Update the particle scale.
        particleScale.x = scaleValue;
        particleScale.y = scaleValue;
        substanceScript.ScaleDownSprite(particleScale);
    }

    #endregion
}
