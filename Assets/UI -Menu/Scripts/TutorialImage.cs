using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialImage : MonoBehaviour
{ 
    public Sprite KeyboardSprite;
    public Sprite ControllerSprite;

    private void OnEnable()
    {
        bool controllerConnected = MyImputManager.connectedToController;

        if (controllerConnected == false)
            GetComponent<Image>().sprite = KeyboardSprite;
        else
            GetComponent<Image>().sprite = ControllerSprite;
    }
}
