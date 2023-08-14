using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    public SideDefinitionSerializable[] SideDefinitions;
    public string levelName;
    public FieldColor[] goal;

    public Save(SideDefinitionSerializable[] aSideDefinitions, string aLevelName, FieldColor[] aGoal)
    {
        SideDefinitions = aSideDefinitions;
        levelName = aLevelName;
        goal = aGoal;
    }

}
