using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Void event")]
public class VoidEvent : ScriptableObject
{
    public UnityAction OnEventRaised;
    public void RaiseEvent()
    {
        if(OnEventRaised != null)
        {
            OnEventRaised.Invoke();
        }
    }
}
