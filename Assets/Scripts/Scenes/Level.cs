using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : SceneBase
{
    public void ChangeLayer(Layer layer)
    {
      
            foreach (var item in items)
            {
            
                item.Move2Layer(layer);
            }
       
    }
}
