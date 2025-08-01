using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : ItemBase
{
    public override void inter()
    {
        base.inter();
        TipPopManager.instance.ShowTip("Computer");
    }
}
