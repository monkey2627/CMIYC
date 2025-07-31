using System;
using UnityEngine;

public static class EventHandler
{
    public static event Action<Transform> OnNoiseEventHappen;
    public static event Action OnCatCaught;
    public static event Action<ItemBase> OnItemBroke;
    
    public static void NoiseEventHappen(Transform noiseSource)
    {
        if (OnNoiseEventHappen != null)
            OnNoiseEventHappen(noiseSource);
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
}
