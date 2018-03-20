using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleCollecting : MonoBehaviour
{
    public SpriteRenderer feedbackSprite;
    public float feedbackDuration;

    private PlayerInstructions instructions;

    private void Start()
    {
        PlayerMovement player = GetComponentInParent<PlayerMovement>();

        instructions = player.GetComponentInChildren<PlayerInstructions>();
    }

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
        if (instructions.CanCollect() == false)
            return;

        StopAllCoroutines();

        StartCoroutine(PossibleCollectingCR());

    }

    private IEnumerator PossibleCollectingCR()
    {
        ChangeFeedBack(true);

        yield return new WaitForSeconds(feedbackDuration);

        ChangeFeedBack(false);

        yield break;
    }

    private void ChangeFeedBack(bool isEnabled)
    {
        feedbackSprite.enabled = isEnabled;

        if (isEnabled)
            instructions.ShowCollectingCanvas();
        else
            instructions.HideCollectingCanvas();

    }
}
