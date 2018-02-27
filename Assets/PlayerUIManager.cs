using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public UIMenu containersUI;
    public UIMenu invetoryUI;
    public UIMenu popUpUI;
    public UIMenu errorTextUI;
    //public UIMenu healthUI;

    private void Start()
    {
        // Activate the containers pannel.
        containersUI.Activate();

        // Activate the invetory pannel.
        invetoryUI.Activate();
        
        // Activate the health UI.
        //healthUI.Activate();
    }






}
