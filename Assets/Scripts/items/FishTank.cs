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
    /// 桌子被撞时掉地上碎
    /// </summary>
    public void DropFromDesk()
    {
        transform.DOMove(new Vector3(0, 0, 0), 2).OnComplete(() => { 
        //todo 碎掉
        
        });
    }
    public override void inter()
    {
        base.inter();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 检测水杯是否落地
        if (collision.gameObject.CompareTag("Floor"))
        {
            // 停止所有运动
            rb.velocity = Vector3.zero; // 将速度设置为零
            rb.angularVelocity = Vector3.zero; // 将角速度设置为零
            rb.isKinematic = true; // 设置为运动学模式，禁止物理影响
        }
        else if (collision.gameObject.CompareTag("Dog"))
        {
            if (!firstCollision) return;
            Dog.instance.Dodge(transform.position);
            firstCollision = false;
        }

    }
}
