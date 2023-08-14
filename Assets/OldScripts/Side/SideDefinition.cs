using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum Side
{
    Bottom = 0,
    Right = 1,
    Top = 2,
    Left = 3,
}

public abstract class SideDefinition : MonoBehaviour
{
    protected abstract void DefinePosition();
    public abstract float GetWidthWorld();
    public abstract float GetHeightWorld();

    public FieldDefinition[] Fields;

    public bool StartAdventage { get; private set; }
    public bool EndAdventage { get; private set; }

    public SideDefinition LeftSide { protected set; get; }
    public SideDefinition RightSide { protected set; get; }

    private void Start()
    {
        DefinePosition();
    }

    public void Init(int numberOfFields)
    {
        InitialingFields(numberOfFields);
    }

    public SideDefinitionSerializable Convert()
    {
        return new SideDefinitionSerializable(Fields);
    }

    public void AssignNeighborsSides(SideDefinition leftNeighbor, SideDefinition rightNeighbor)
    {
        LeftSide = leftNeighbor;
        RightSide = rightNeighbor;
    }

    public void DefineAdventages(bool startAdventage, bool endAdventage)
    {
        StartAdventage = startAdventage;
        LeftSide.EndAdventage = !startAdventage;

        EndAdventage = endAdventage;
        RightSide.StartAdventage = !endAdventage;

    }

    public FieldDefinition GetFirstField()
    {
        return Fields[0];
    }
    public FieldDefinition GetLastField()
    {
        return Fields[Fields.Length - 1];
    }

    private void InitialingFields(int num)
    {
        Side parentSide = Side.Bottom;
        if (this is RightSide)
        {
            parentSide = Side.Right;
        }
        else if (this is TopSide)
        {
            parentSide = Side.Top;
        }
        else if (this is LeftSide)
        {
            parentSide = Side.Left;
        }

        Fields = new FieldDefinition[num];
        for (int i = 0; i < num; i++)
        {
            Fields[i] = new FieldDefinition(parentSide);
        }
    }

    public void FieldPositionsOnSide()
    {
        float delta = 0;
        float coefficient = 1;
        float tempFloat = 0;

        if (this is BottomSide || this is TopSide)
        {
            coefficient = GlobalParameters.Instance.CamAsp;
        }

        if (!StartAdventage)
        {
            delta = LeftSide.GetLastField().Size.HeightPercentage / coefficient;
        }

        for (int i = 0; i < Fields.Length; i++)
        {
            Fields[i].PositionOnSidePercentage = delta + tempFloat + Fields[i].Size.WidthPercentage / 2;
            tempFloat += Fields[i].Size.WidthPercentage;
        }
    }

    public float GetFieldsWidthPercentage()
    {
        float sum = 0f;
        foreach (FieldDefinition field in Fields)
        {
            sum += field.Size.WidthPercentage;
        }
        return sum;
    }

    public float GetActualIdealWidthPercentage()
    {
        float idealWidth = 1f;

        if (!StartAdventage)
        {
            idealWidth -= LeftSide.GetLastField().Size.HeightPercentage;
        }
        if (!EndAdventage)
        {
            idealWidth -= RightSide.GetFirstField().Size.HeightPercentage;
        }

        idealWidth -= GetFieldsWidthPercentage();
        return idealWidth / GetNumOfUndifinitedFields();
    }

    public bool IsItFirstField(FieldDefinition fieldDefinition)
    {
        return GetFirstField() == fieldDefinition;
    }

    public bool IsItLastField(FieldDefinition fieldDefinition)
    {
        return GetLastField() == fieldDefinition;
    }

    public float GetFieldPosition(FieldDefinition fieldDefinition)
    {
        int fieldNumOrder = FieldNumberOrderInSide(fieldDefinition);

        float pos = Fields[fieldNumOrder - 1].PositionOnSidePercentage +
            Fields[fieldNumOrder - 1].Size.WidthPercentage / 2 +
            Fields[fieldNumOrder].Size.WidthPercentage / 2;

        return pos;
    }

    private int GetNumOfUndifinitedFields()
    {
        int numOfUndifinitedFields = 0;
        foreach (FieldDefinition field in Fields)
        {
            if (field.Size.WidthPercentage == 0)
            {
                numOfUndifinitedFields++;
            }
        }
        return numOfUndifinitedFields;
    }

    public int FieldNumberOrderInSide(FieldDefinition fieldDefinition)
    {
        for (int i = 0; i < Fields.Length; i++)
        {
            if (Fields[i] == fieldDefinition)
            {
                return i;
            }
        }
        return -1;
    }
}
