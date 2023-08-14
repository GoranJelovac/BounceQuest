using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class AllowedAreaChecker : CheckerAbstract
{
    public GameObject[] collidersMaxGO;
    public GameObject[] collidersMinGO;

    private GameObject SpawnGO(FieldDefinition fieldDefinition, bool isLeft, float y)
    {
        SideDefinition parent = Sides.Instance.sides[(int)fieldDefinition.Parent];
        GameObject GO = new GameObject();
        float delta = fieldDefinition.Size.WidthPercentage / 2;
        delta = isLeft ? -delta : delta;
        GO.transform.localPosition = new Vector3(
            (fieldDefinition.PositionOnSidePercentage + delta) * parent.GetWidthWorld(),
            y,
            0
            );
        GO.transform.SetParent(parent.transform, false);
        return GO;
    }

    private float ColliderHeightInPoint(FieldDefinition fieldDefinition, bool isLeft, GameObject[] GOs)
    {
        GameObject ispaljivac = SpawnGO(fieldDefinition, isLeft, 50);
        GameObject baza = SpawnGO(fieldDefinition, isLeft, 0);

        EnableCollider(fieldDefinition.Parent, GOs);

        RaycastHit2D hit = Physics2D.Raycast(ispaljivac.transform.position, -ispaljivac.transform.up, float.MaxValue, 1 << 7);

        DisableAll(GOs);

        Destroy(ispaljivac);
        Destroy(baza);

        if (hit.collider == null)
        {
            Debug.LogError("Neuspesno odredjivanje maksimalne visine, nista nije pogodjeno!");
            return -1;
        }
        SideDefinition parent = Sides.Instance.sides[(int)fieldDefinition.Parent];

        return (hit.point - (Vector2)baza.transform.position).magnitude / parent.GetHeightWorld();
    }

    public override float GetMaxHeight(FieldDefinition fieldDefinition)
    {
        float height1 = ColliderHeightInPoint(fieldDefinition, true, collidersMaxGO);
        float height2 = ColliderHeightInPoint(fieldDefinition, false, collidersMaxGO);

        /*Debug.Log("*******************************************");
        Debug.Log($"Side: {Sides.Instance.sides[(int)fieldDefinition.Parent]}");
        Debug.Log($"Field: {Sides.Instance.sides[(int)fieldDefinition.Parent].FieldNumberOrderInSide(fieldDefinition)}");
        Debug.Log($"For MAX heigts hits: {height1} and {height2}");
        Debug.Log($"Max height is: {Mathf.Min(height2, height1)}");*/

        return Mathf.Min(height2, height1);
    }

    public override float GetMinHeight(FieldDefinition fieldDefinition)
    {
        float height1 = ColliderHeightInPoint(fieldDefinition, true, collidersMinGO);
        float height2 = ColliderHeightInPoint(fieldDefinition, false, collidersMinGO);

        /*Debug.Log("++++++");
        Debug.Log($"For MIN heigts hits: {height1} and {height2}");
        Debug.Log($"Min height is: {Mathf.Max(height2, height1)}");*/

        return Mathf.Max(height2, height1);
    }

    private void EnableCollider(Side side, GameObject[] GOs)
    {
        DisableAll(GOs);
        GOs[(int)side].SetActive(true);
    }

    private void DisableAll(GameObject[] GOs)
    {
        foreach (var collider in GOs)
        {
            collider.SetActive(false);
        }
    }
}