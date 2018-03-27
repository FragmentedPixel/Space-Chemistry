using System.Collections;
using UnityEngine;

/*
 * Responsible for triggering a dialog when the player enters the trigger.
 */

public class DialogTrigger : OneTimeSoundTrigger
{
    #region Variabiles
    public GameObject dialogGO;
    public float duration = .5f;
    #endregion

    #region Initialization
    private void Start()
    {
        dialogGO.transform.localScale = Vector3.zero;
    }
    #endregion

    #region Player Interaction
    protected override void OnPlayerTriggered()
    {
        StartCoroutine(ShowDialogCR());
    }

    private IEnumerator ShowDialogCR()
    {
        float currentTime = 0f;

        Vector3 startPosition = FindObjectOfType<PlayerMovement>().transform.position;
        Vector3 endPosition = dialogGO.transform.position;

        while(currentTime < duration)
        {
            dialogGO.transform.position = Vector3.Lerp(startPosition, endPosition, currentTime / duration);
            dialogGO.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, currentTime / duration);

            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        Destroy(dialogGO);

        yield break;
    }
    #endregion
}
