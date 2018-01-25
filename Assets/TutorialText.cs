using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public bool controllerConnected;

    [TextArea] public string KeyboardText;
    [TextArea] public string GamepadText;

    private void OnEnable()
    {
        controllerConnected = HandMovement.connectedToController;

        if (controllerConnected == false)
            GetComponent<Text>().text = KeyboardText;
        else
            GetComponent<Text>().text = GamepadText;
    }

}
