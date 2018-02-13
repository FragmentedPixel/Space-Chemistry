using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleCollecting : MonoBehaviour
{
    public SpriteRenderer feedbackSprite;
    public float feedbackDuration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FiniteBarrel barrel = collision.gameObject.GetComponent<FiniteBarrel>();
        if (barrel != null)
        {
            sSubstance particleInsideBarrel = barrel.CheckParticle();

            if (particleInsideBarrel != null)
            {


                StartFeedBack();
            }
        }

        // Get information about the current collision.
        Particle collectedParticle = collision.GetComponent<Particle>();

        //Escape collisions with ground or other objects or the players is not collecting now.
        if (collectedParticle != null)
        {

            StartFeedBack();
        }
    }

    private void StartFeedBack()
    {
        StopAllCoroutines();

        StartCoroutine(PossibleCollectingCR());

    }

    private IEnumerator PossibleCollectingCR()
    {
        feedbackSprite.enabled = true;

        yield return new WaitForSeconds(feedbackDuration);

        feedbackSprite.enabled = false;

        yield break;
    }
}
