using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class LevelLoading : MonoBehaviour
{
    public float duration;

    private PostProcessingProfile ppp;

    private float targetIntesity;
    private float targetSmooth;

    private void Start()
    {
        ppp = GetComponent<PostProcessingBehaviour>().profile;
        StartCoroutine(LerpCR());
    }

    private IEnumerator LerpCR()
    {
        float currentTime = 0f;

        while(currentTime < duration)
        {
            //ppp.vignette.settings.intensity = Mathf.Lerp(1f, targetIntesity, currentTime / duration);
            //ppp.vignette.settings.smoothness = Mathf.Lerp(1f, targetSmooth, currentTime / duration);

            currentTime += Time.deltaTime;
            yield return null;
        }
    }

}
