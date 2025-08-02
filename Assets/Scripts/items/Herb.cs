using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : ItemBase
{
    public HerbType type;
    public override void inter()
    {
        base.inter();
        Cat.instance.GetMaterial((int)type);
    }
    private void Update()
    {
        if (Cat.instance.materialNumber != -1)
            enable = false;
    }
}

public enum HerbType
{
    JumpBud,//ÌøÌøÑ¿
    Canterburybells,//·çÁå²İ
    LightFeatherFlower//ÇáÓğ»¨
}