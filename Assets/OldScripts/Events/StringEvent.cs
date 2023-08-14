using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/String event")]
public class StringEvent : ScriptableObject
{
    public UnityAction<string> OnEventRaised;

    public void RaiseEvent(string text)
    {
        if(OnEventRaised != null)
        {
            OnEventRaised.Invoke(text);
        }
    }
}

