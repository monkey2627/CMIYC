using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlace : ItemBase
{
    public int LayerHide;//Layer�����Ҫ�ص���ǰһ��
    public override void inter()
    {
        base.inter();
        Cat.instance.Hide(LayerHide);
        if(gameObject.name == "Jerry")
        {
            TipPopManager.instance.ShowTip("Let the record show: I decline all associations with that ahem cartoonishly inept namesake.");
        }
    }

}
