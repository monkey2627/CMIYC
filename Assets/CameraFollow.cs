using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 要跟随的目标对象（Cat）
    public float smoothing = 20f; // 平滑系数，控制相机跟随的速度
    public Vector2 minPosition; // 相机的最小位置（左下边界）
    public Vector2 maxPosition; // 相机的最大位置（右上边界）

    private Vector3 velocity = Vector3.zero; // 用于平滑跟随的速度变量

    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogError("Target is not set.");
            return;
        }

        // 计算目标位置
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // 限制目标位置在边界内
        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

        // 平滑移动相机到目标位置
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
    }
}