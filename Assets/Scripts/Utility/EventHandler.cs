using System;
using UnityEngine;

public static class EventHandler
{
    public static event Action<AttentionEvent> OnAttentionEventHappen;
    public static event Action OnCatCaught;
    public static event Action<ItemBase> OnItemBroke;
    public static event Action<bool> OnStoveStateChange;
    
    public static event Action<bool> OnChangeDoorState;
    
    public static event Action OnClockUsed;
    
    public static event Action OnMasterLeaveWindow;
    
    public static void AttentionEventHappen(AttentionEvent attentionEvent)
    {
        if (OnAttentionEventHappen != null)
            OnAttentionEventHappen(attentionEvent);
    }
    
    public static void CatchCat()
    {
        if (OnCatCaught != null)
            OnCatCaught();
    }
    
    public static void ItemBroke(ItemBase item)
    {
        if (OnItemBroke != null)
            OnItemBroke(item);
    }
    
    public static void StoveStateChange(bool isOn)
    {
        if (OnStoveStateChange != null)
            OnStoveStateChange(isOn);
    }
    
    public static void ChangeDoorState(bool isOpen)
    {
        if (OnChangeDoorState != null)
            OnChangeDoorState(isOpen);
    }
    
    public static void ClockUsed()
    {
        if (OnClockUsed != null)
            OnClockUsed();
    }
    
    public static void MasterLeaveWindow()
    {
        if (OnMasterLeaveWindow != null)
            OnMasterLeaveWindow();
    }

}
