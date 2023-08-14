using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void OnEnable()
    {
        Events.Instance.LoadingLevelDone += LoadingLevelDone;
    }

    private void OnDisable()
    {
        Events.Instance.LoadingLevelDone -= LoadingLevelDone;

    }

    private void Start()
    {
        Events.Instance.LoadLevel("11-11");
        //_loadFieldsEvent.RaiseEvent(); fff

        /*FieldDefinition[] fields = new FieldDefinition[10];

        // Strana, pozicija, sirina, visina, boja.
        fields[0] = new FieldDefinition(FieldSide.Bottom, 0.25f, new Vector2(0.5f, 1), FieldColors.Blue);
        fields[1] = new FieldDefinition(FieldSide.Bottom, 0.01f, new Vector2(0.01f, 0.01f), FieldColors.Red);
        fields[2] = new FieldDefinition(FieldSide.Bottom, 0.01f, new Vector2(0.01f, 0.01f), FieldColors.Green);
        fields[3] = new FieldDefinition(FieldSide.Right, 0.65f, new Vector2(0.5f, 0.7f), FieldColors.Red);
        fields[4] = new FieldDefinition(FieldSide.Right, 0.35f, new Vector2(0.5f, 0.35f), FieldColors.Blue);
        fields[5] = new FieldDefinition(FieldSide.Top, 0.5f, new Vector2(0.5f, 1), FieldColors.Blue);
        fields[6] = new FieldDefinition(FieldSide.Top, 0.01f, new Vector2(0.01f, 0.01f), FieldColors.Red);
        fields[7] = new FieldDefinition(FieldSide.Top, 0.01f, new Vector2(0.01f, 0.01f), FieldColors.Green);
        fields[8] = new FieldDefinition(FieldSide.Left, 0.65f, new Vector2(0.5f, 0.7f), FieldColors.Blue);
        fields[9] = new FieldDefinition(FieldSide.Left, 0.35f, new Vector2(0.5f, 0.35f), FieldColors.Red);

        _levelGenerationEvent.RaiseEvent(fields);
        _saveFieldsEvent.RaiseEvent(fields);*/
    }

    private void LoadingLevelDone(bool success)
    {
        if (!success)
        {
            Debug.LogError("Pero konju, error je!");
            return;
        }

        Events.Instance.LevelGenerationEvent.Invoke(Sides.Instance.sides);
    }

}
