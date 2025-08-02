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
        // 检测水杯是否落地
        if (collision.gameObject.CompareTag("Floor"))
        {
            // 停止所有运动
            rb.velocity = Vector3.zero; // 将速度设置为零
            rb.angularVelocity = Vector3.zero; // 将角速度设置为零
            rb.isKinematic = true; // 设置为运动学模式，禁止物理影响
        }else if (collision.gameObject.CompareTag("Dog"))
        {
            if (!firstCollision) return;
            Dog.instance.Dodge(transform.position);
            firstCollision = false;
        }
    }

}
