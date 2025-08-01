using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : ItemBase
{
    public override void inter()
    {
        base.inter();
        TipPopManager.instance.ShowTip("Painting");
    }
}
