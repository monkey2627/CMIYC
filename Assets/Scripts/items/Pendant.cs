using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendant : ItemBase
{

    public float initialForce = 10.0f; // ��ʼ�ƶ���
    public float duration = 5.0f; // ҡ�γ���ʱ��
    public float damping = 0.5f; // ����ϵ�������Ʒ��ȼ�С���ٶ�

    private Rigidbody rb; // ���Ƶ� Rigidbody ���
    private Vector3 initialPosition; // ���Ƶĳ�ʼλ��
    private bool isSwinging = false; // �Ƿ�����ҡ��
    public override void inter()
    {
        base.inter();
        if (isSwinging) return;
        initialPosition = transform.localPosition; // ��¼��ʼλ��
        StartCoroutine(SwingLamp());
        if (Dog.instance.IsInLivingRoom())
        {
            Dog.instance.Move2Pendant();
        }
    }
    IEnumerator SwingLamp()
    {
        isSwinging = true;

        // ��¼��ʼʱ��
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            // ���㵱ǰʱ��ı���
            float t = (Time.time - startTime) / duration;

            // ���㵱ǰ�������𽥼�С
            float currentForce = initialForce * (1 - t);

            // Ӧ������ʹ��������ҡ��
            rb.AddForce(Vector3.left * currentForce * Mathf.Sin(t * Mathf.PI * 2));

            yield return null;
        }

        // ҡ�ν�����ֹͣ�����˶����ص���ʼλ��
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.localPosition = initialPosition;

        isSwinging = false;
    }
}
