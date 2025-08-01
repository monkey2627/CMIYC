using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttentionEvent
{
    public Transform EventPlaceTrans;
    public AttentionEventType EventType;
    public AttentionEvent(Transform eventPlaceTrans, AttentionEventType attentionEventType)
    {
        EventPlaceTrans = eventPlaceTrans;
        EventType = attentionEventType;
    }
}


public enum AttentionEventType
{
    ItemBroken,         // 物品掉落-破碎
    DogBark,            // 狗吠
    WildCatMeow,        // 窗外的野猫叫
    DogDestruction,     // 狗搞破坏
    FogSpread,          // 灰雾扩散
    GuestArrive,        // 客人来访
}
