using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Bool event")]
public class BoolEvent : ScriptableObject
{
    public UnityAction<bool> OnEventRaised;

    public void RaiseEvent(bool boolean)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(boolean);
        }
    }
}
