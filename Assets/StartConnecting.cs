using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartConnecting : MonoBehaviour
{
    public float speed;
    public Image[] containers;

    private void Start()
    {
        StartFilling();
    }

    public void StartFilling()
    {
        StartCoroutine(FillingCR());
    }

    private IEnumerator FillingCR()
    {
        for(int i = 0; i < containers.Length; i++)
        {
            while(containers[i].fillAmount < 1)
            {
                containers[i].fillAmount += Time.deltaTime * speed;
                yield return null;
            }
        }

        FinishedFilling();
    }

    private void FinishedFilling()
    {

    }
	
}
