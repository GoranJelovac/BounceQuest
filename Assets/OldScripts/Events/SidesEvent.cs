using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Side event")]
public class SidesEvent : ScriptableObject
{
    public UnityAction<SideDefinition[]> OnEventRaised;
    public void RaiseEvent(SideDefinition[] sides)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(sides);
        }
    }
}
