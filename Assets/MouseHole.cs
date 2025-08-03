using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHole : ItemBase
{
    public override void inter()
    {
        base.inter();
        
        TipPopManager.instance.ShowTip("Let the record show: I decline all associations with that ahem cartoonishly inept namesake.");
    }
}
