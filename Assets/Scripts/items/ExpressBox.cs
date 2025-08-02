using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 快递箱
/// </summary>
public class ExpressBox : ItemBase
{
    /// <summary>
    /// 被猫咪抓挠，被推动，被狗破坏
    /// </summary>
    public override void inter()
    {
        base.inter();
        Cat.instance.Scratch();
    }
    public override void ScratchByDog()
    {
        base.ScratchByDog();
        Dog.instance.animator.SetBool("Scratch", true);
        //破坏后有几率出现毛线球
    }
}
