using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSide : VerticalSide
{
    protected override void DefinePosition()
    {
        transform.position = new Vector3(
            -GlobalParameters.Instance.ScreenWidthWorld / 2,
            GlobalParameters.Instance.ScreenHeightWorld / 2,
            transform.position.z
            );

        transform.eulerAngles = new Vector3(0, 0, 270);
    }

    public override float GetWidthWorld()
    {
        return GlobalParameters.Instance.ScreenHeightWorld;
    }

    public override float GetHeightWorld()
    {
        return GlobalParameters.Instance.ScreenWidthWorld;
    }
}
