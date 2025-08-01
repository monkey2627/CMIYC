using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exihibition : ItemBase
{
    public GameObject[] exhibits;
    public override void inter()
    {
        base.inter();
        //柜子上的东西都被推下来
    }
}
