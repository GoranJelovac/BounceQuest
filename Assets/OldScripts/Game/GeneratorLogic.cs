using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorLogic : MonoBehaviour
{
    [SerializeField] private IInputs _userInputs;
    [SerializeField] private GoalSO goalSO;
    [SerializeField] private InputField solutionInput;

    private void OnEnable()
    {
        Events.Instance.LoadingLevelDone += LoadingDone;
    }

    private void OnDisable()
    {
        Events.Instance.LoadingLevelDone -= LoadingDone;

    }

    public void GenerateButtonClicked()
    {
        Events.Instance.ClearLevel.Invoke();

        Events.Instance.LevelDefineStart.Invoke();

        Events.Instance.LevelGenerationEvent.Invoke(Sides.Instance.sides);
    }

    private SideDefinitionSerializable[] SerializeSides()
    {
        SideDefinitionSerializable[] SideDefinitions = new SideDefinitionSerializable[Sides.Instance.sides.Length];
        for (int i = 0; i < Sides.Instance.sides.Length; i++)
        {
            SideDefinitions[i] = Sides.Instance.sides[i].Convert();
        }
        return SideDefinitions;
    }

    public void SaveButtonClicked()
    {
        //Events.Instance.SaveSides.Invoke(_sideDefinitionArraySO.Value);
        string levelName = GetLevelName();
        Save save = new Save(SerializeSides(), levelName, goalSO.goal);
        Events.Instance.SaveEvent.Invoke(save);
    }

    public void LoadLevelButtonClicked()
    {
        string levelName = GetLevelName();
        Events.Instance.LoadLevel.Invoke(levelName);
    }

    private string GetLevelName()
    {
        int round = _userInputs.GetRoundData();
        int battle = _userInputs.GetBattleData();
        string levelName = round + "-" + battle;
        return levelName;
    }

    private void LoadingDone(bool success)
    {
        if (!success)
        {
            Debug.LogError("konju error je!");
            return;
        }

        Events.Instance.LevelGenerationEvent.Invoke(Sides.Instance.sides);
    }

    public void RefreshButtonClicked()
    {
        LoadingDone(true);
    }

    public void ShowSolutionButtonClicked()
    {

    }
}
