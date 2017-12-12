using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUp : MonoBehaviour
{
    public GameObject tutorialPanelToShow;
    public AudioClip enterSound;

    private AudioSource audioS;

    private void Start()
    {
        HidePannel();

        audioS = gameObject.AddComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();
    }

    private bool isPlayer(GameObject go)
    {
        PlayerMovement player = go.GetComponent<PlayerMovement>();
        return (player != null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPlayer(collision.gameObject))
        {
            ShowPannel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isPlayer(collision.gameObject))
        {
            HidePannel();
        }
    }

    private void ShowPannel()
    {
        audioS.PlayOneShot(enterSound);

        StartCoroutine(ChangeAlpha(0f, 1f));

        Color newColor = GetComponent<SpriteRenderer>().color;
        newColor.a = 0f;
        GetComponent<SpriteRenderer>().color = newColor;
    }

    private void HidePannel()
    {

        StartCoroutine(ChangeAlpha(1f, 0f));

        Color newColor = GetComponent<SpriteRenderer>().color;
        newColor.a = 1f;
        GetComponent<SpriteRenderer>().color = newColor;
    }

    private IEnumerator ChangeAlpha(float start, float end)
    {
        float duration = .5f;
        float currentTime = 0f;

        while(currentTime < duration)
        {
            Color newColor = tutorialPanelToShow.GetComponent<SpriteRenderer>().color;
            newColor.a = Mathf.Lerp(start, end, currentTime / duration);
            tutorialPanelToShow.GetComponent<SpriteRenderer>().color = newColor;

            currentTime += Time.deltaTime;
            yield return null;
        }

        yield break;
    }
}
