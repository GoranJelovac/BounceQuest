using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{

    public static int ConTextToInt(string s1)
    {
        int result;

        if (!int.TryParse(s1, out result))
        {
            Debug.LogError("KONJU!");
            return 1;
        }
        return result;

    }

    public static int RandomBetweenTwoInts(int i1, int i2)
    {
        return Random.Range(i1, i2 + 1);
    }

    public static int RandomBetweenTwoStrings(string s1, string s2)
    {
        int i1 = ConTextToInt(s1);
        int i2 = ConTextToInt(s2);
        return RandomBetweenTwoInts(i1, i2);
    }

    public static float RoundOnXDecimals(float number, int howManyDecimals)
    {
        float power = Mathf.Pow(10, howManyDecimals);
        float res = Mathf.Round(number * power) / power;
        return res;
    }

    public static void ShuffleArray(float[] arrayForShuffle)
    {
        float tempFloat;

        for (int i = 0; i < arrayForShuffle.Length; i++)
        {
            int rnd = Random.Range(0, arrayForShuffle.Length);
            tempFloat = arrayForShuffle[rnd];
            arrayForShuffle[rnd] = arrayForShuffle[i];
            arrayForShuffle[i] = tempFloat;
        }
    }

    // Seach all array if any member has greater width than side height and swap places
    // Example: if left last field has height 2, first field on bottom side should have width more than 2
    // SeachingFirst true = first field on that side
    // SeachingFirst false = last field on that side
    // First field cant be swaped with Last field
    public static bool SeachArrayForWidth(float[] floatArray, float minHeight, bool searchingFirst)
    {
        bool vecbio = false;
        int posForReplace = 0;

        for (int i = 0; i < floatArray.Length - 1; i++)
        {
            if (!vecbio)
            {
                vecbio = true;
                if (searchingFirst)
                {
                    posForReplace = 0;
                }
                else
                {
                    posForReplace = floatArray.Length - 1;
                    i++;
                }
            }

            float temp;
            if (floatArray[i] > minHeight)
            {
                temp = floatArray[i];
                floatArray[i] = floatArray[posForReplace];
                floatArray[posForReplace] = temp;
                return true;
            }
        }
        return false;
    }

    // Rotate in degrees
    public static Vector2 RotateVector2(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        ) * Mathf.PI / 180f;
    }

    public static string ConvertColorsToString(List<FieldColor> colors)
    {
        return string.Join(" ", colors);
    }

    public static FieldColor ConvertStringToColor(string str)
    {
        return (FieldColor)System.Enum.Parse(typeof(FieldColor), str);
    }

    public static List<FieldColor> ConvertStringToColors(string colorsString)
    {
        string[] colors = colorsString.Split(' ');
        List<FieldColor> result = new List<FieldColor>();

        foreach (string color in colors)
        {
            result.Add(ConvertStringToColor(color));
        }

        return result;
    }

    public static int CountWords(string str)
    {
        string[] array = str.Split(" ");
        return array.Length;
    }

    public static float GetDegree(Vector2 dir)
    {
        float value = (float)((System.Math.Atan2(dir.y, dir.x) / System.Math.PI) * 180f);
        if (value < 0)
        {
            value += 360f;
        }
        return RoundOnXDecimals(value, 1);
    }

    public static string ShowSolutionSimplified(string str)
    {
        string[] temp = new string[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            switch (str[i])
            {
                case 'Y':
                    temp[i] = "Yellow";
                    break;
                case 'B':
                    temp[i] = "Blue";
                    break;
                case 'G':
                    temp[i] = "Green";
                    break;
                case 'R':
                    temp[i] = "Red";
                    break;
                case 'P':
                    temp[i] = "Purple";
                    break;
                default:
                    Debug.Log("Nesto cudno se desilo");
                    break;
            }
        }

        return string.Join(" ", temp);
    }
}
