using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Fields event")]
public class FieldsEvent : ScriptableObject
{
    public UnityAction<FieldDefinition[]> OnEventRaised;
    public void RaiseEvent(FieldDefinition[] fields)
    {
        if(OnEventRaised != null)
        {
            OnEventRaised.Invoke(fields);
        }
    }
}
