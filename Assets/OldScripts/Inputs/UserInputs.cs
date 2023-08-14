using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class IInputs : MonoBehaviour
{
    public abstract int GetColorData();
    public abstract RangeInt[] GetFieldRange();
    public abstract int GetBattleData();
    public abstract int GetRoundData();
}

public class UserInputs : IInputs
{
    [SerializeField] private Text[] textInputFieldsRange;
    [SerializeField] private Text textInputNumberOfColors;
    [SerializeField] private Text textInputRound;
    [SerializeField] private Text textInputBattle;

    public override int GetColorData()
    {
        return Utils.ConTextToInt(textInputNumberOfColors.text);
    }

    public override int GetBattleData()
    {
        return Utils.ConTextToInt(textInputBattle.text);
    }

    public override int GetRoundData()
    {
        return Utils.ConTextToInt(textInputRound.text);
    }

    public override RangeInt[] GetFieldRange()
    {
        RangeInt[] range = new RangeInt[4];
        for (int i = 0; i < range.Length; i++)
        {
            range[i].Min = Utils.ConTextToInt(textInputFieldsRange[i * 2].text);
            range[i].Max = Utils.ConTextToInt(textInputFieldsRange[i * 2 + 1].text);
        }

        return range;
    }

    public InputData GetInputData()
    {
        InputData inputData = new InputData(
            aColorsNumber: GetColorData(),
            aBattleNumber: GetBattleData(),
            aRoundNumber: GetRoundData(),
            aFieldsNumberRangeOnSide: GetFieldRange());

        return inputData;
    }

}
