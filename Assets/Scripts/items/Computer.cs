using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : ItemBase
{
    public override void inter()
    {
        base.inter();
        TipPopManager.instance.ShowTip("The two-legged is playing “CAT-ch Me If You Can”. The protagonist looks... suspiciously like yourself.");
    }
}
