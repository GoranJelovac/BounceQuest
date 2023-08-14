using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public static Events Instance { get; private set; }

    public UnityAction ClearLevel;
    public UnityAction<string> DebugEvent;
    public UnityAction LevelDefineStart;
    public UnityAction<SideDefinition[]> LevelGenerationEvent;
    public UnityAction<bool> LoadingLevelDone;
    public UnityAction<string> LoadLevel;
    public UnityAction<SidesEvent> LoadSides;
    public UnityAction<Save> SaveEvent;
    //public UnityAction<SideDefinition[]> SaveSides;
    public UnityAction<FieldDefinition[]> SpawnFields;
    public UnityAction LevelGenerationDone;
    public UnityAction<Vector2, Vector2> Cast;
    public UnityAction<Vector2, int, bool> FullDepthCast;

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


