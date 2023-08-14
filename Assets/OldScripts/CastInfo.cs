using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CastInfo
{
    public Vector2 ballCentarPoint;
    public Vector2 contactPoint;
    public FieldColor? hitFieldColor;
    public Vector2 hitNormal;
    public GameObject hitObject;

    public CastInfo(Vector2 aBallCentarPoint, Vector2 aContactPoint, Vector2 aHitNormal, GameObject aHitObject)
    {
        ballCentarPoint = aBallCentarPoint;
        contactPoint = aContactPoint;
        hitFieldColor = GetFieldColor(aHitObject);
        hitNormal = aHitNormal;
        hitObject = aHitObject;
    }

    private FieldColor? GetFieldColor(GameObject GO)
    {
        if (GO == null)
        {
            return null;
        }

        Field field = GO.GetComponent<Field>();
        if (field == null)
        {
            return null;
        }

        return field.definition.FieldColor;
    }
}
