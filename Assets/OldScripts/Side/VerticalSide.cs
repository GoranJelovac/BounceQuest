using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VerticalSide : SideDefinition
{
    public override float GetWidthWorld()
    {
        return GlobalParameters.Instance.ScreenHeightWorld;
    }

    public override float GetHeightWorld()
    {
        return GlobalParameters.Instance.ScreenWidthWorld;
    }
}
