using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RangeInt
{
    public int Min;
    public int Max;
}

public class InputData
{
    public int ColorsNumber;
    public int BattleNumber;
    public int RoundNumber;
    public RangeInt[] FieldsNumberRangeOnSide;
    
    public InputData(int aColorsNumber, int aBattleNumber, int aRoundNumber, RangeInt[] aFieldsNumberRangeOnSide)
    {
        ColorsNumber = aColorsNumber;
        BattleNumber = aBattleNumber;
        RoundNumber = aRoundNumber;
        FieldsNumberRangeOnSide = aFieldsNumberRangeOnSide;
    }
}
