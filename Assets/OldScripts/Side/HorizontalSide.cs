using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HorizontalSide : SideDefinition
{
    public override float GetWidthWorld()
    {
        return GlobalParameters.Instance.ScreenWidthWorld;
    }

    public override float GetHeightWorld()
    {
        return GlobalParameters.Instance.ScreenHeightWorld;
    }
}
