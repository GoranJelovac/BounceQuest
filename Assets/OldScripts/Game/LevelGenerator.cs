using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private SideDefinition[] _lastGeneratedSides;

    private void OnEnable()
    {
        Events.Instance.LevelGenerationEvent += LevelGenerate;
        Events.Instance.ClearLevel += ClearLevel;
    }

    private void OnDisable()
    {
        Events.Instance.LevelGenerationEvent -= LevelGenerate;
        Events.Instance.ClearLevel -= ClearLevel;
    }

    private void LevelGenerate(SideDefinition[] sides)
    {
        ClearLevel();
        _lastGeneratedSides = sides;
        for (int i = 0; i < sides.Length; i++)
        {
            Events.Instance.SpawnFields.Invoke(sides[i].Fields);
        }
        Events.Instance.LevelGenerationDone.Invoke();
    }

    private void DestroyFields(FieldDefinition[] fieldDefs)
    {
        if (fieldDefs == null)
            return;

        for (int i = 0; i < fieldDefs.Length; i++)
        {
            Destroy(fieldDefs[i].field?.gameObject);
        }
    }

    private void ClearLevel()
    {
        if (_lastGeneratedSides == null)
            return;

        for (int i = 0; i < _lastGeneratedSides.Length; i++)
        {
            DestroyFields(_lastGeneratedSides[i].Fields);
        }
    }
}
