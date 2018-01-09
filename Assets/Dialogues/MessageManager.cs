using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Responsible for displaying messages on screen for a short period of time.
 */

public class MessageManager : MonoBehaviour
{
    #region Variabiles
    // Singleton instance.
    private static MessageManager instance;

    // UI References.
    private Text messageText;

    // Colors.
    private Color initColor;
    private Color finalColor;
    #endregion

    #region Methods
    private void Start()
    {
        // References.
        instance = this;
        messageText = GetComponent<Text>();
        
        // Set up colors for messages.
        initColor = messageText.color;
        finalColor = initColor;
        finalColor.a = 0;
    }

    public static MessageManager getInstance()
    {
        return instance;
    }

    public void DissplayMessage(string text, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayMessageCR(text, duration));
    }

    private IEnumerator DisplayMessageCR(string text, float duration)
    {
        // Reset timer.
        float currentTime = 0f;

        // Display text.
        messageText.text = text;
        
        // Lerp Color.
        while(currentTime < duration)
        {
            messageText.color = Color.Lerp(initColor, finalColor, currentTime / duration);
            currentTime += Time.deltaTime;

            yield return null;
        }

        // Hide Text.
        messageText.color = initColor;
        messageText.text = string.Empty;
        yield break;

    }
    #endregion
}
