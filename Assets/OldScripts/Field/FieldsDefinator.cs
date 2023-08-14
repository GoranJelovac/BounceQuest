using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldsDefinator : MonoBehaviour
{
    [SerializeField] private IInputs _userInputs;
    [SerializeField] private CheckerAbstract allowedAreaChecker;

    public void DefineField(FieldDefinition fieldDefinition, FieldDefinition adventageField = null, bool isLast = false)
    {
        DefineWidth(fieldDefinition, isLast);
        DefinePosition(fieldDefinition, adventageField);
        DefineHeight(fieldDefinition, adventageField);
        DefineColor(fieldDefinition);
    }

    private void DefineColor(FieldDefinition fieldDefinition)
    {
        int inputColorsNumber = _userInputs.GetColorData();
        int _maxColorsNumber = System.Enum.GetValues(typeof(FieldColor)).Length;

        inputColorsNumber = Mathf.Clamp(inputColorsNumber, 0, _maxColorsNumber);

        int x = Random.Range(0, inputColorsNumber);
        fieldDefinition.FieldColor = (FieldColor)x;
    }

    public void DefineHeights(FieldDefinition[] fields, float minHeight, float maxHeight)
    {
        float lastHeight = 0f;
        float delta = 0.2f;
        float upis;

        for (int i = 0; i < fields.Length; i++)
        {
            float ostatak = 1;
            float randomFloat = Random.Range(0f, 1f);
            if (i == 0)
            {
                upis = randomFloat;
                lastHeight = upis;

                fields[i].Size.HeightPercentage = minHeight + (maxHeight - minHeight) * upis;
                continue;
            }

            if (lastHeight <= delta)
            {
                ostatak = ostatak - lastHeight - delta;
            }
            else if (lastHeight >= 1 - delta)
            {
                ostatak = ostatak - (1 - lastHeight) - delta;
            }
            else
            {
                ostatak -= 2 * delta;
            }

            float novaVisina = ostatak * randomFloat;

            if (novaVisina > lastHeight - delta)
            {
                upis = novaVisina + (1 - ostatak);
                lastHeight = upis;
                fields[i].Size.HeightPercentage = minHeight + (maxHeight - minHeight) * upis;
                continue;
            }
            upis = novaVisina;
            lastHeight = upis;
            fields[i].Size.HeightPercentage = minHeight + (maxHeight - minHeight) * upis;
        }
    }

    public void DefineHeight(FieldDefinition field, FieldDefinition adventageField = null)
    {

        float maxHeight = Utils.RoundOnXDecimals(allowedAreaChecker.GetMaxHeight(field), 4);
        float minHeight = Utils.RoundOnXDecimals(allowedAreaChecker.GetMinHeight(field), 4);

        // U slucaju da nema adventage na stranici onda njegova visina ne moze biti veca od sirine fielda sa stranice pored
        if (adventageField != null)
        {
            maxHeight = Mathf.Min(adventageField.Size.WidthPercentage, maxHeight);
        }

        field.Size.HeightPercentage = Random.Range(minHeight, maxHeight);

        /*Debug.Log($"MAXh is: {maxHeight}");
        Debug.Log($"MINh is: {minHeight}");
        Debug.Log($"Izabrano je: {field.Size.HeightPercentage}");
        Debug.Log("------------------");*/
    }

    public void DefinePosition(FieldDefinition fieldDefinition, FieldDefinition adventageField = null)
    {
        float pos = 0;

        SideDefinition parent = Sides.Instance.sides[(int)fieldDefinition.Parent];

        float adventageFieldHeight = 0;

        if (adventageField != null)
        {
            adventageFieldHeight = adventageField.Size.HeightPercentage;
        }

        if (parent.IsItFirstField(fieldDefinition))
        {
            pos = adventageFieldHeight + fieldDefinition.Size.WidthPercentage / 2;
        }
        else if (parent.IsItLastField(fieldDefinition))
        {
            pos = 1f - adventageFieldHeight - fieldDefinition.Size.WidthPercentage / 2;
        }
        else
        {
            pos = parent.GetFieldPosition(fieldDefinition);
        }

        DefinePosition(fieldDefinition, pos);
    }

    public void DefinePosition(FieldDefinition field, float pos)
    {
        field.PositionOnSidePercentage = pos;
    }

    public void DefinePositions(FieldDefinition[] fields)
    {
        for (int i = 1; i < fields.Length; i++)
        {
            DefinePosition(
                fields[i],
                fields[i - 1].PositionOnSidePercentage + fields[i - 1].Size.WidthPercentage / 2 + fields[i].Size.WidthPercentage / 2
                );
        }
    }

    public void DefineWidth(FieldDefinition fieldDefinition, bool isLast = false)
    {
        SideDefinition parent = Sides.Instance.sides[(int)fieldDefinition.Parent];

        if (isLast)
        {
            fieldDefinition.Size.WidthPercentage = parent.GetActualIdealWidthPercentage();
        }
        else
        {
            float maxWidth = parent.GetActualIdealWidthPercentage() * (1 + GlobalParameters.deltaWidth);
            fieldDefinition.Size.WidthPercentage = Random.Range(GlobalParameters.minWidth, maxWidth);
        }
    }
}
