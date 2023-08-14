using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _outline;
    [SerializeField] private Collider2D[] colliders;

    public FieldDefinition definition;

    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider2D>();
    }

    public void ChangeSize(Vector2 newSize)
    {
        _outline.size = newSize;
        UpdateColliders(newSize);
    }

    public void UpdateColliders(Vector2 newSize)
    {
        float radius = 0.2f;
        ((BoxCollider2D)colliders[0]).size = new Vector2(newSize.x, newSize.y - 2 * radius);
        ((BoxCollider2D)colliders[0]).offset = new Vector2(((BoxCollider2D)colliders[0]).offset.x, ((BoxCollider2D)colliders[0]).size.y / 2 + radius);

        ((BoxCollider2D)colliders[1]).size = new Vector2(newSize.x - 2 * radius, newSize.y);
        ((BoxCollider2D)colliders[1]).offset = new Vector2(((BoxCollider2D)colliders[1]).offset.x, ((BoxCollider2D)colliders[1]).size.y / 2);

        Vector2 dir = new Vector2(1, 1);
        for (int i = 2; i < 6; i++)
        {
            ((CircleCollider2D)colliders[i]).offset = new Vector2(dir.x * (newSize.x / 2 - radius), newSize.y / 2 + dir.y * (newSize.y / 2 - radius));
            dir = Utils.RotateVector2(dir, 90f);
        }
    }
}
