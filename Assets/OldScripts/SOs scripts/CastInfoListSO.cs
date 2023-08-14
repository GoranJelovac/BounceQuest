using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/Cast Info List")]
public class CastInfoListSO : ScriptableObject
{
    public List<CastInfo> Value;
}

