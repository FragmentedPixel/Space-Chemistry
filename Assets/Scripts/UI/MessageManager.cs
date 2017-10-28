using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public static MessageManager instance;

    private Text messageText;
    private Color initColor;

    private void Start()
    {
        instance = this;
        messageText = GetComponent<Text>();
        initColor = messageText.color;
    }

    public void DissplayMessage(string text, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayMessageCR(text, duration));
    }

    private IEnumerator DisplayMessageCR(string text, float duration)
    {
        float currentTime = 0f;
        messageText.text = text;
        
        Color finalColor = initColor;
        finalColor.a = 0;

        while(currentTime < duration)
        {
            messageText.color = Color.Lerp(initColor, finalColor, currentTime / duration);
            currentTime += Time.deltaTime;

            yield return null;
        }

        messageText.color = initColor;
        messageText.text = string.Empty;
        yield break;

    }


}
