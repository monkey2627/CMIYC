using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ItemBase
{
    /// <summary>
    /// ��һ�ؿ���ҧס�򿪣��ڶ�����Ҫ���룻��������ס�򲻿� 
    /// </summary>
    public override void inter()
    {
        base.inter();
        if (GameManager.instance.level == 0)
        {
            //��
        }else if (GameManager.instance.level == 1)
        {

        }else if (GameManager.instance.level == 2)
        {
            return;
        }
    }
}
