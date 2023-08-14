using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidesDefinator : MonoBehaviour
{
    [SerializeField, SerializeReference] private IInputs _userInputs;
    [SerializeField] private FieldsDefinator _fieldDefinitor;

    private const int sidesNumber = 4;
    //TODO: srediti kad bude grafika
    private const float minHeight = 0.11f;
    //TODO: srediti kad bude grafika
    private const float maxHeight = 0.2f;

    private void OnEnable()
    {
        Events.Instance.LevelDefineStart += DefineSides;
    }

    private void OnDisable()
    {
        Events.Instance.LevelDefineStart -= DefineSides;
    }

    private void DefineSides()
    {
        InstantiatingSides();
        DefineCorners();
        DefineOtherFields();
    }

    private void DefineOtherFields()
    {
        for (int sideNumber = 0; sideNumber < 4; sideNumber++)
        {
            SideDefinition currentSide = Sides.Instance.sides[sideNumber];
            for (int fieldNumber = 1; fieldNumber < currentSide.Fields.Length - 2; fieldNumber++)
            {
                FieldDefinition fieldDefinition = currentSide.Fields[fieldNumber];
                _fieldDefinitor.DefineField(fieldDefinition);
            }
            _fieldDefinitor.DefineField(currentSide.Fields[currentSide.Fields.Length - 2], null, true);
        }
    }

    private void DefineCorners()
    {
        for (int i = 0; i < 4; i++)
        {
            SideDefinition currentSide = Sides.Instance.sides[i];
            if (currentSide.StartAdventage)
            {
                _fieldDefinitor.DefineField(
                    currentSide.GetFirstField()
                    );
                _fieldDefinitor.DefineField(
                    currentSide.LeftSide.GetLastField(),
                    currentSide.GetFirstField()
                    );
            }
            else
            {
                _fieldDefinitor.DefineField(
                    currentSide.LeftSide.GetLastField()
                    );
                _fieldDefinitor.DefineField(
                    currentSide.GetFirstField(),
                    currentSide.LeftSide.GetLastField()
                    );
            }
        }
    }

    private void InstantiatingSides()
    {
        int[] fieldsBySides = GetFieldsNumberBySides();
        bool[] adventages = DecideWhatEdgeHasAdventage();

        Sides.Instance.sides[(int)Side.Bottom].Init(
            fieldsBySides[(int)Side.Bottom]
            );
        Sides.Instance.sides[(int)Side.Right].Init(
            fieldsBySides[(int)Side.Right]
            );
        Sides.Instance.sides[(int)Side.Top].Init(
            fieldsBySides[(int)Side.Top]
            );
        Sides.Instance.sides[(int)Side.Left].Init(
            fieldsBySides[(int)Side.Left]
            );

        Sides.Instance.sides[(int)Side.Bottom].AssignNeighborsSides(
            Sides.Instance.sides[(int)Side.Left],
            Sides.Instance.sides[(int)Side.Right]
            );
        Sides.Instance.sides[(int)Side.Right].AssignNeighborsSides(
             Sides.Instance.sides[(int)Side.Bottom],
             Sides.Instance.sides[(int)Side.Top]
             );
        Sides.Instance.sides[(int)Side.Top].AssignNeighborsSides(
            Sides.Instance.sides[(int)Side.Right],
            Sides.Instance.sides[(int)Side.Left]
            );
        Sides.Instance.sides[(int)Side.Left].AssignNeighborsSides(
            Sides.Instance.sides[(int)Side.Top],
            Sides.Instance.sides[(int)Side.Bottom]
            );

        Sides.Instance.sides[(int)Side.Bottom].DefineAdventages(adventages[0], adventages[1]);
        Sides.Instance.sides[(int)Side.Right].DefineAdventages(!adventages[1], !adventages[2]);
        Sides.Instance.sides[(int)Side.Top].DefineAdventages(adventages[2], adventages[3]);
        Sides.Instance.sides[(int)Side.Left].DefineAdventages(!adventages[3], !adventages[0]);
    }

    private int[] GetFieldsNumberBySides()
    {
        RangeInt[] range = _userInputs.GetFieldRange();

        int[] numberOfFieldsOnSide = new int[range.Length];

        for (int i = 0; i < range.Length; i++)
        {
            numberOfFieldsOnSide[i] = Utils.RandomBetweenTwoInts(
                range[i].Min,
                range[i].Max
                );
        }
        return numberOfFieldsOnSide;
    }

    private bool[] DecideWhatEdgeHasAdventage()
    {
        bool[] res = new bool[4];

        for (int i = 0; i < 4; i++)
        {
            res[i] = Random.value > 0.5;
        }

        return res;
    }
}
