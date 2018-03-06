using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Responsible for managing the player's containers.
 */

public class Container : MonoBehaviour
{
    #region Variabiles
    public sSubstance substance;
    public float particules;
    public float capacity = 15;

    public Image highLightImage;
    public Image fillImage;
    public Transform containerTransform;

    public float blinkSpeed = .1f;
    public float targetAlpha = .5f;
    private float currentBlink;
    private bool goingToBlack = false;
    #endregion

    #region UI Updates

    public void UpdateContainerSubstance(sSubstance newSubstance)
    {
        substance = newSubstance;
    }

    public void UpdateContainerUI()
    {
        fillImage.fillAmount = particules / capacity;

        if(substance != null)
            fillImage.color = substance.particleColor;
    }

    #endregion

    #region Fill & Release
    public bool AddParticule(sSubstance substanceParticule)
    {

        // Update substance if none is inside.
        if (substance == null)
        {
            GreenBackground();

            particules = 1;
            substance = substanceParticule;
            return true;
        }
        // Check if there is the right substance.
        else if (substance == substanceParticule)
        {
            GreenBackground();

            particules+=5;
            return true;
        }
        // Let the player know if the wrong substance is inside.
        else
        {
            Blink();
            MessageManager.getInstance().DissplayMessage("There is an other substance inside this container.", 1f);
            return false;
        }
    }

    public sSubstance ReleaseParticule()
    {

        // Check if there is any substance in the container.
        if (substance == null)
        {
            Blink();
            return null;
        }

        // Check if there is any substance left inside the container.
        else if (particules == 0)
        {
            Blink();
            substance = null;
            return null;
        }

        // Return particle of the substance.
        else
        {
            RedBackground();
            particules--;
            return substance;
        }
    }

    public void OnStay()
    {
        GreyBackground();
    }

    private void RedBackground()
    {
        Color targetColor = Color.red;
        targetColor.a = highLightImage.color.a;
        highLightImage.color = targetColor;
    }

    private void GreenBackground()
    {
        Color targetColor = Color.green;
        targetColor.a = highLightImage.color.a;
        highLightImage.color = targetColor;
    }

    private void GreyBackground()
    {
        Color targetColor = Color.grey;
        targetColor.a = highLightImage.color.a;
        highLightImage.color = targetColor;
    }

    private void Blink()
    {
        currentBlink += blinkSpeed * Time.deltaTime;

        if (currentBlink > 1f)
        {
            goingToBlack = !goingToBlack;
            currentBlink = 0f;
        }

        if (goingToBlack)
        {
            Color targetColor = Color.Lerp(Color.white, Color.black, currentBlink);
            targetColor.a = highLightImage.color.a;
            highLightImage.color = targetColor;
        }
        else
        {
            Color targetColor = Color.Lerp(Color.black, Color.white, currentBlink);
            targetColor.a = highLightImage.color.a;
            highLightImage.color = targetColor;
        }

        
    }
    #endregion

    #region Empty & Fill Container
    public void EmptyContainer()
    {
        // Clear all data about the container.
        particules = 0;
        substance = null;

        UpdateContainerUI();
    }

    public void FillWith(float particulesCount, sSubstance newSubstance)
    {
        particules = particulesCount;
        substance = newSubstance;

        UpdateContainerUI();
    }

    public bool isFull()
    {
        if (particules == 0)
            Blink();

        return (capacity <= particules);
    }

    public bool isEmpty()
    {
        if (substance == null)
            return true;
        else if (particules <= 0)
            return true;
        else
            return false;
    }
    #endregion

    #region Highlighting
    public void Unlock()
    {
        StartCoroutine(UnlockCR());
    }

    private IEnumerator UnlockCR()
    {
        int repeats = 5;
        int current = 0;

        while(current < repeats)
        {
            currentBlink += blinkSpeed * Time.deltaTime;

            if (currentBlink > 1f)
            {
                goingToBlack = !goingToBlack;
                currentBlink = 0f;

                current++;
            }

            if (goingToBlack)
            {
                Color targetColor = Color.Lerp(Color.blue, Color.red, currentBlink);
                targetColor.a = 1;
                highLightImage.color = targetColor;
            }
            else
            {
                Color targetColor = Color.Lerp(Color.red, Color.blue, currentBlink);
                targetColor.a = 1;
                highLightImage.color = targetColor;
            }


            yield return null;
        }

        PlayerContainers player = FindObjectOfType<PlayerContainers>();

        if(player.GetCurrentContainer() == this)
        {
            GreyBackground();
            HighLight();
        }
        else
        {
            StopHighLight();
        }

        yield break;
    }

    public void HighLight()
    {
        SetImageAlpha(targetAlpha);
        containerTransform.GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;

    }

    public void StopHighLight()
    {
        SetImageAlpha(0f);
        containerTransform.GetComponent<RectTransform>().localScale = Vector3.one * 1f;
    }

    private void SetImageAlpha(float alpha)
    {
        Color highColor = highLightImage.color;
        highColor.a = alpha;
        highLightImage.color = highColor;
    }
    #endregion
}
