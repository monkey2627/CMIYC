using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnob : ItemBase
{
    /// <summary>
    /// �ﵽ�Ű���ֱ��ʤ��
    /// </summary>
    public override void inter()
    {
        base.inter();
        GameManager.instance.Win();
    }
}
