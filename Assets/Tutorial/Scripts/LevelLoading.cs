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
            VignetteModel vignet = ppp.vignette;
            VignetteModel.Settings newVignette = vignet.settings;

            newVignette.intensity = Mathf.Lerp(1f, 0f, currentTime / duration);
            newVignette.smoothness = Mathf.Lerp(1f, 0f, currentTime / duration);

            vignet.settings = newVignette;

            GrainModel grain = ppp.grain;
            GrainModel.Settings newGrain = grain.settings;

            newGrain.intensity = Mathf.Lerp(1f, 0f, currentTime / duration);
            newGrain.size = Mathf.Lerp(3f, 1f, currentTime / duration);
            newGrain.luminanceContribution = Mathf.Lerp(0f, 1f, currentTime / duration);

            grain.settings = newGrain;

            currentTime += Time.deltaTime;
            yield return null;
        }
    }

}
