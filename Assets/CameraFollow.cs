using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ҫ�����Ŀ�����Cat��
    public float smoothing = 20f; // ƽ��ϵ�����������������ٶ�
    public Vector2 minPosition; // �������Сλ�ã����±߽磩
    public Vector2 maxPosition; // ��������λ�ã����ϱ߽磩

    private Vector3 velocity = Vector3.zero; // ����ƽ��������ٶȱ���

    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogError("Target is not set.");
            return;
        }

        // ����Ŀ��λ��
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // ����Ŀ��λ���ڱ߽���
        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

        // ƽ���ƶ������Ŀ��λ��
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
    }
}