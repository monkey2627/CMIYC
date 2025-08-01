using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : ItemBase
{
    public bool isOn = false;


    public override void inter()
    {
        base.inter();
        
        if (isOn)
        {
            isOn = false;
            EventHandler.StoveStateChnage(isOn);
        }
    }
}
