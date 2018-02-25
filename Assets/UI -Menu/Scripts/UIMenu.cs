using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    protected Animator anim;

    public bool isOpen;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        anim.SetTrigger("IntroTrigger");
        isOpen = true;
    }

    public void Deactivate()
    {
        anim.SetTrigger("OutroTrigger");

        if (gameObject.activeInHierarchy)
            StartCoroutine(WaitForAnimationEnd());

        isOpen = false;
    }

    private IEnumerator WaitForAnimationEnd()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
        yield break;
    }

}
