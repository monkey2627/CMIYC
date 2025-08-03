using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : ItemBase
{
    public SpriteRenderer viewRenderer;
    public Sprite daskSprite;
    public override void inter()
    {
        base.inter();
        if (interactCnt >= 0)
        {
            TipPopManager.instance.ShowTip("Target group spotted in the eastern bushes—potential escape accomplices. If only they understand Meow-rse code... " +
                                           "Just go tap that DOORBELL! Hmm... You'll pay them with your secretly stashed tuna treats afterward.");
        }
        else
        {
            TipPopManager.instance.ShowTip("Operation initiated. All paws proceed as planned. Meow~");
        }
        interactCnt++;
    }

    public void ClockUsed()
    {
        viewRenderer.sprite = daskSprite;
    }
    
    private void OnEnable()
    {
        EventHandler.OnClockUsed += ClockUsed;
    }
    
    private void OnDisable()
    {
        EventHandler.OnClockUsed -= ClockUsed;
    }
}
