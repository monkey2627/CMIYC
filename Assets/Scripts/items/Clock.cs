using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : ItemBase
{
    public Transform Door;
    public override void inter()
    {
        base.inter();
        // 时间转换演出
        
        // 客人来访
        EventHandler.AttentionEventHappen(new AttentionEvent(Door, AttentionEventType.GuestArrive));
    }
}
