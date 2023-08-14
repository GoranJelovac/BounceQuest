using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldColor
{
    Blue,
    Red,
    Yellow,
    Green,
    Purple,
}

[Serializable]
public class SerializableVector2
{
    public virtual float WidthPercentage { get; set; }
    public virtual float HeightPercentage { get; set; }

    public SerializableVector2()
    {
    }
}

[Serializable]
public class RoundedSerializableVector2 : SerializableVector2
{
    [SerializeField] private float _width;
    [SerializeField] private float _height;

    public override float WidthPercentage
    {
        get
        {
            return _width;
        }
        set
        {
            _width = Utils.RoundOnXDecimals(value, 4);
        }
    }

    public override float HeightPercentage
    {
        get
        {
            return _height;
        }
        set
        {
            _height = Utils.RoundOnXDecimals(value, 4);
        }
    }
}

[Serializable]
public class FieldDefinition
{
    public RoundedSerializableVector2 Size;
    public FieldColor FieldColor;
    public float PositionOnSidePercentage;
    public Side Parent { get; private set; }
    [NonSerialized] public Field field;

    public FieldDefinition(Side aParent)
    {
        Parent = aParent;
        Size = new RoundedSerializableVector2();
    }
}
