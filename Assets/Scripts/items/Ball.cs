using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : ItemBase
{
    public override void inter()
    {
        base.inter();
        Cat.instance.PlayBall();
    }
}
