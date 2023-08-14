using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSide : HorizontalSide
{
    protected override void DefinePosition()
    {
        transform.position = new Vector3(
            -GlobalParameters.Instance.ScreenWidthWorld / 2,
            -GlobalParameters.Instance.ScreenHeightWorld / 2,
            transform.position.z
            );
    }
}
