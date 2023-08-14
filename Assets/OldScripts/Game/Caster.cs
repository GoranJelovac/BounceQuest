using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Caster : MonoBehaviour
{
    [SerializeField] private CastInfoSO castInfoSO;
    [SerializeField] private CastInfoListSO castInfoListSO;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        Events.Instance.Cast += Cast;
        Events.Instance.FullDepthCast += FullDepthCast;
    }

    private void OnDisable()
    {
        Events.Instance.Cast -= Cast;
        Events.Instance.FullDepthCast -= FullDepthCast;
    }

    private void FullDepthCast(Vector2 dir, int depth, bool draw)
    {
        Vector2 startPoint = Vector2.zero;
        castInfoListSO.Value = new List<CastInfo>();
        lineRenderer.positionCount = depth + 1;
        lineRenderer.SetPosition(0, startPoint);

        for (int i = 0; i < depth; i++)
        {

            Cast(startPoint, dir);
            castInfoListSO.Value.Add(castInfoSO.Value);
            castInfoSO.Value.hitObject.SetActive(false);

            // Priprema za sledeci Cast
            startPoint = castInfoSO.Value.ballCentarPoint;
            dir = Vector2.Reflect(dir, castInfoSO.Value.hitNormal);

            // Draw
            if (draw)
            {
                lineRenderer.SetPosition(i + 1, startPoint);
            }
        }
    }

    private void Cast(Vector2 startPoint, Vector2 dir)
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(startPoint, GlobalParameters.Instance.ballRadius, dir, float.MaxValue, GlobalParameters.Instance.fieldsLayerMask);

        if (hit.Length == 0)
        {
            castInfoSO.Value = null;
            return;
        }


        /*
        castInfoSO.Value = new CastInfo(
            hit.point,
            hit.point + hit.normal * GlobalParameters.Instance.ballRadius,
            hit.normal,
            hit.collider.gameObject
            );

        lastHitGO = castInfoSO.Value.hitObject;*/
    }
}
