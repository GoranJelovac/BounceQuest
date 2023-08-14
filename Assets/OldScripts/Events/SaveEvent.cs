using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Save event")]
public class SaveEvent : ScriptableObject
{
    public UnityAction<Save> OnEventRaised;
    public void RaiseEvent(Save save)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(save);
        }
    }
}