using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : ItemBase
{
    private Rigidbody rb;
    public bool firstCollision = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        // ���ˮ���Ƿ����
        if (collision.gameObject.CompareTag("Floor"))
        {
            // ֹͣ�����˶�
            rb.velocity = Vector3.zero; // ���ٶ�����Ϊ��
            rb.angularVelocity = Vector3.zero; // �����ٶ�����Ϊ��
            rb.isKinematic = true; // ����Ϊ�˶�ѧģʽ����ֹ����Ӱ��
        }else if (collision.gameObject.CompareTag("Dog"))
        {
            if (!firstCollision) return;
            Dog.instance.Dodge(transform.position);
            firstCollision = false;
        }
    }

}
