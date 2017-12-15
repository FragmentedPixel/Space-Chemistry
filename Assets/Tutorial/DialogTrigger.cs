using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public GameObject dialogGO;

    private bool triggered = false;

    private void Start()
    {
        dialogGO.transform.localScale = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null && triggered == false)
        {
            StartCoroutine(ShowDialogCR(player.transform.position));
        }
    }

    private IEnumerator ShowDialogCR(Vector3 startPosition)
    {
        triggered = true;

        float duration = .5f;
        float currentTime = 0f;

        Vector3 endPosition = dialogGO.transform.position;

        while(currentTime < duration)
        {
            dialogGO.transform.position = Vector3.Lerp(startPosition, endPosition, currentTime / duration);
            dialogGO.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, currentTime / duration);

            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        GameObject.Destroy(dialogGO);

        yield break;
    }

}
