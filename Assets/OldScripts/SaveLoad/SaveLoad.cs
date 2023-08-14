using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private GoalSO goalSO;

    private void OnEnable()
    {
        Events.Instance.SaveEvent += SaveGame;
        Events.Instance.LoadLevel += LoadGame;
    }

    private void OnDisable()
    {
        Events.Instance.SaveEvent -= SaveGame;
        Events.Instance.LoadLevel -= LoadGame;
    }

    public void SaveGame(Save save)
    {
        string fullFileName = Path.Combine(Application.streamingAssetsPath, save.levelName);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(fullFileName);
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadGame(string levelName)
    {
        string fullFileName = Path.Combine(Application.streamingAssetsPath, levelName);

#if UNITY_EDITOR
        if (!File.Exists(fullFileName))
        {
            Events.Instance.DebugEvent.Invoke($"Nema {fullFileName} nivo konju!");
            Events.Instance.LoadingLevelDone.Invoke(false);
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(fullFileName, FileMode.Open);
        Save save = (Save)bf.Deserialize(file);
        file.Close();

        ConvertSideDefinitions(save.SideDefinitions);
        goalSO.goal = save.goal;
        Events.Instance.LoadingLevelDone.Invoke(true);

#elif UNITY_ANDROID
        StartCoroutine(LoadAndroidFields(fullFileName));
#endif
    }

    private void ConvertSideDefinitions(SideDefinitionSerializable[] sideDefinitionSerializables)
    {
        for (int i = 0; i < sideDefinitionSerializables.Length; i++)
        {
            sideDefinitionSerializables[i].Convert((Side)i);
        }
    }

    private IEnumerator LoadAndroidFields(string fullFileName)
    {
        var loadingRequest = UnityWebRequest.Get(fullFileName);

        loadingRequest.SendWebRequest();
        while (!loadingRequest.isDone)
        {
            yield return null;
            if (loadingRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Events.Instance.DebugEvent.Invoke("Connection Error - JA");

                yield break;
            }
        }
        Events.Instance.DebugEvent.Invoke(loadingRequest.result.ToString());
        BinaryFormatter bf = new BinaryFormatter();
        Stream stream = new MemoryStream(loadingRequest.downloadHandler.data);
        Save save = (Save)bf.Deserialize(stream);
        ConvertSideDefinitions(save.SideDefinitions);
        goalSO.goal = save.goal;

        Events.Instance.DebugEvent.Invoke(save == null ? "nula je" : "nije");
        //Events.Instance.DebugEvent.Invoke(save.SideDefinitions.Length.ToString());

        Events.Instance.LoadingLevelDone.Invoke(true);
    }
}
