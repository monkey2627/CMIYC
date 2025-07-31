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
    /// 不可移动的物体被撞击时，其上物体掉下
    /// </summary>
    public void DropThings()
    {

    }
    /// <summary>
    /// 猫咪观察时展示
    /// </summary>
    public void Show()
    {

    }
    public void Hide()
    {

    }
}
