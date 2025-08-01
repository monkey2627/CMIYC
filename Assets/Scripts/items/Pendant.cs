using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendant : ItemBase
{

    public float initialForce = 10.0f; // 初始推动力
    public float duration = 5.0f; // 摇晃持续时间
    public float damping = 0.5f; // 阻尼系数，控制幅度减小的速度

    private Rigidbody rb; // 吊灯的 Rigidbody 组件
    private Vector3 initialPosition; // 吊灯的初始位置
    private bool isSwinging = false; // 是否正在摇晃
    public override void inter()
    {
        base.inter();
        if (isSwinging) return;
        initialPosition = transform.localPosition; // 记录初始位置
        StartCoroutine(SwingLamp());
        if (Dog.instance.IsInLivingRoom())
        {
            Dog.instance.Move2Pendant();
        }
    }
    IEnumerator SwingLamp()
    {
        isSwinging = true;

        // 记录开始时间
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            // 计算当前时间的比例
            float t = (Time.time - startTime) / duration;

            // 计算当前的力，逐渐减小
            float currentForce = initialForce * (1 - t);

            // 应用力，使吊灯左右摇晃
            rb.AddForce(Vector3.left * currentForce * Mathf.Sin(t * Mathf.PI * 2));

            yield return null;
        }

        // 摇晃结束，停止所有运动并回到初始位置
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.localPosition = initialPosition;

        isSwinging = false;
    }
}
