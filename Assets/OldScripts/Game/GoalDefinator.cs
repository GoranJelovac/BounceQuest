using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GoalDefinator : MonoBehaviour
{
    private void OnEnable()
    {
        Events.Instance.LevelGenerationDone += PickGoal;
    }

    private void OnDisable()
    {
        Events.Instance.LevelGenerationDone -= PickGoal;
    }

    private void PickGoal()
    {
        FindAllSolutions();
    }

    private void FindAllSolutions()
    {
        Vector2 dir = Vector2.down;
        Events.Instance.FullDepthCast.Invoke(dir, 3, true);
    }
}
