using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sides : MonoBehaviour
{
    public static Sides Instance { get; private set; }
    public SideDefinition[] sides;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
