using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldsSpawner : MonoBehaviour
{
    public GameObject defaultField;
    public GameObject blueFieldPref;
    public GameObject redFieldPref;
    public GameObject greenFieldPref;
    public GameObject yellowFieldPref;
    public GameObject purpleFieldPref;

    private void OnEnable()
    {
        Events.Instance.SpawnFields += SpawnFields;
    }

    private void OnDisable()
    {
        Events.Instance.SpawnFields -= SpawnFields;
    }

    private void SpawnFields(FieldDefinition[] fields)
    {
        //int i = 0;
        float sum = 0f;
        foreach (var field in fields)
        {
            //print($"-----------------------i: {i} -----------------------");
            InstantiateField(field);
            //print($"width: {field.Size.WidthPercentage}, height: {field.Size.HeightPercentage}, pos: {field.PositionOnSidePercentage}");
            //print($"{field.Parent}, {field.FieldColor}");
            if (field.Parent == Side.Bottom)
            {
                sum += field.Size.WidthPercentage; //????????????????????????????
            }
            //i++;
        }
    }

    private void InstantiateField(FieldDefinition fieldDefinition)
    {
        GameObject GO = Instantiate(PrefabReference(fieldDefinition.FieldColor));
        fieldDefinition.field = GO.GetComponent<Field>();
        fieldDefinition.field.definition = fieldDefinition;
        SideDefinition parent = Sides.Instance.sides[(int)fieldDefinition.Parent];

        GO.transform.localPosition = new Vector3(
            fieldDefinition.PositionOnSidePercentage * parent.GetWidthWorld(),
            0,
            0
            );
        GO.transform.SetParent(parent.transform, false);


        GO.GetComponent<Field>().ChangeSize(
            new Vector2(
            fieldDefinition.Size.WidthPercentage * parent.GetWidthWorld(),
            fieldDefinition.Size.HeightPercentage * parent.GetHeightWorld()
            )
            );
    }

    private GameObject PrefabReference(FieldColor color)
    {
        return color switch
        {
            FieldColor.Blue => blueFieldPref,
            FieldColor.Red => redFieldPref,
            FieldColor.Yellow => yellowFieldPref,
            FieldColor.Green => greenFieldPref,
            FieldColor.Purple => purpleFieldPref,
            _ => defaultField,
        };
    }
}
