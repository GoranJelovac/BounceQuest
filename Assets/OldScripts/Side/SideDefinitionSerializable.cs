using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SideDefinitionSerializable
{
    public FieldDefinition[] Fields;

    public SideDefinitionSerializable(FieldDefinition[] aFields)
    {
        Fields = aFields;
    }

    public void Convert(Side side)
    {
        Sides.Instance.sides[(int)side].Fields = Fields;
    }
}
