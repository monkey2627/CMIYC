using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����
/// </summary>
public class ExpressBox : ItemBase
{
    /// <summary>
    /// ��è��ץ�ӣ����ƶ��������ƻ�
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
        //�ƻ����м��ʳ���ë����
    }
}
