using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CheckerAbstract : MonoBehaviour
{
    public abstract float GetMaxHeight(FieldDefinition fieldDefinition);
    public abstract float GetMinHeight(FieldDefinition fieldDefinition);
}
