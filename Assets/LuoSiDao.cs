using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuoSiDao :ItemBase
{
    public override void inter()
    {
        base.inter();
        Cat.instance.GetLuoSi();
        enable = false;
        gameObject.SetActive(false);
    }
}
