using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : ItemBase
{
    public SpriteRenderer viewRenderer;
    public Sprite daskSprite;
    public GameObject meowItem;
    public override void inter()
    {
        base.inter();
        enable = false;
        meowItem.SetActive(true);
        //吸引主人
        AttentionEvent attentionEvent = new AttentionEvent(transform, AttentionEventType.WildCatMeow);
        EventHandler.AttentionEventHappen(attentionEvent);
        TipPopManager.instance.ShowTip("I PAY the rent with my CUTENESS and THIS is my view? Look at them, leaping through the grass without a care... LET ME OUT!!");
    }

    public void ClockUsed()
    {
        viewRenderer.sprite = daskSprite;
    }

    public void LeaveWindow()
    {
        enable = true;
    }
    
    private void OnEnable()
    {
        EventHandler.OnClockUsed += ClockUsed;
        EventHandler.OnMasterLeaveWindow += LeaveWindow;
    }
    
    private void OnDisable()
    {
        EventHandler.OnClockUsed -= ClockUsed;
        EventHandler.OnMasterLeaveWindow -= LeaveWindow;
    }
}
