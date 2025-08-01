using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTank : ItemBase
{
    public Rigidbody rb;
    public bool firstCollision = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    /// <summary>
    /// ���ӱ�ײʱ��������
    /// </summary>
    public void DropFromDesk()
    {
        transform.DOMove(new Vector3(0, 0, 0), 2).OnComplete(() => { 
        //todo ���
        
        });
    }
    public override void inter()
    {
        base.inter();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���ˮ���Ƿ����
        if (collision.gameObject.CompareTag("Floor"))
        {
            // ֹͣ�����˶�
            rb.velocity = Vector3.zero; // ���ٶ�����Ϊ��
            rb.angularVelocity = Vector3.zero; // �����ٶ�����Ϊ��
            rb.isKinematic = true; // ����Ϊ�˶�ѧģʽ����ֹ����Ӱ��
        }
        else if (collision.gameObject.CompareTag("Dog"))
        {
            if (!firstCollision) return;
            Dog.instance.Dodge(transform.position);
            firstCollision = false;
        }

    }
}
