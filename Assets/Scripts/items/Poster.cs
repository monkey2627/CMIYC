using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poster : ItemBase
{
    public override void inter()
    {
        base.inter();
        TipPopManager.instance.ShowTip("Requiescat in treats... after I escape this PRISON!");
    }

    
}
