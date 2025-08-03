using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InterType
{
    Scratch,
    Open,
    Push
}
public enum Layer
{
    Zero,
    One,
    Two,
    Three,
    Four,   // 主人所在层，和其他互不干扰
}
public class ItemBase : MonoBehaviour
{
    private bool isMoving = false;
    public InterType interType;
    public bool enable = true;
    /// <summary>
    /// 让狗狗走到的位置
    /// </summary>
    public Vector3 position;
    private Vector3 moveDirection;
    public Layer[] layer;
    public bool isInThisLayer = false;
    public int interactCnt = 0;
    public void Move2Layer(Layer l)
    {
        isInThisLayer = false;
        int min = 9;
        foreach (var item in layer)
        {
            if ((int)item < min)
                min = (int)item;
            if(item == l)
                isInThisLayer = true;
        }
        if(min > (int)l)
            gameObject.GetComponent<SpriteRenderer>().DOFade(0.3f, 0.01f);
        else
            gameObject.GetComponent<SpriteRenderer>().DOFade(1f, 0.01f);
    }
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
    public virtual void DropThings()
    {

    }
    public GameObject item;
    /// <summary>
    /// 猫咪观察时展示
    /// </summary>
    public virtual void Show()
    {
        item.SetActive(true);
    }
    public virtual void Hide()
    {
        item.SetActive(false);
    }
    public virtual void ScratchByDog()
    {

    }
}
