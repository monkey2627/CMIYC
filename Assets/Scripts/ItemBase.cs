using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 moveDirection;
    public 
    void Start()
    {
        
    }
    public virtual void inter()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Move(Vector3 direction)
    {
        if (!isMoving)
        {
            isMoving = true;
            moveDirection = direction;
            transform.position += direction * 1 * Time.deltaTime;
        }
    }
    /// <summary>
    /// �����ƶ������屻ײ��ʱ�������������
    /// </summary>
    public void DropThings()
    {

    }
    /// <summary>
    /// è��۲�ʱչʾ
    /// </summary>
    public void Show()
    {

    }
    public void Hide()
    {

    }
}
