using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ItemBase
{
    /// <summary>
    /// 第一关可以咬住打开，第二关需要密码；第三关锁住打不开 
    /// </summary>
    public override void inter()
    {
        base.inter();
        if (GameManager.instance.level == 0)
        {
            //打开
        }else if (GameManager.instance.level == 1)
        {

        }else if (GameManager.instance.level == 2)
        {
            return;
        }
    }
}
