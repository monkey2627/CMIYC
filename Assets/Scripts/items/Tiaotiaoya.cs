using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiaotiaoya :ItemBase
{
    public override void inter()
    {
        base.inter();
        Cat.instance.FetchTiaoTiaoYa();
    }
}
