using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : ItemBase
{
    public Transform Door;
    public override void inter()
    {
        base.inter();
        if (interactCnt > 0)
            return;
        
        // 时间转换演出
        
        // 客人来访
        EventHandler.ClockUsed();
        EventHandler.AttentionEventHappen(new AttentionEvent(Door, AttentionEventType.GuestArrive, -57));
        DialogueManager.instance.DoorDingDong();
        interactCnt++;
    }
}
