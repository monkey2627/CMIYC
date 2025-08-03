using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnob : ItemBase
{
    /// <summary>
    /// 达到门把手直接胜利
    /// </summary>
    public override void inter()
    {
        base.inter();
        GameManager.instance.Win();
    }
}
