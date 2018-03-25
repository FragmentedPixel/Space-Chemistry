using System;
using UnityEngine;

[Serializable]
public class FloatVariable : ScriptableObject
{
    [Tooltip("Description of the variable.")]
    [TextArea] [SerializeField] private string description;

    [SerializeField] private float defaultValue;

    private float currentValue;

    public float value
    {
        get { return currentValue; }
        set {
            OnVariableChanged();
            currentValue = value;
        }
    }

    private void OnEnable()
    {
        currentValue = defaultValue;
    }

    private void OnVariableChanged()
    {
        Debug.Log("Variable was changed.");
    }


}
