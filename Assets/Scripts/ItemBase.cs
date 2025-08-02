using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InterType
{
    Scratch,
    Open,
}
public enum Layer
{
    One,
    Two,
    Three
}
public class ItemBase : MonoBehaviour
{
    private bool isMoving = false;
    public InterType interType;
    public bool enable = true;
    /// <summary>
    /// �ù����ߵ���λ��
    /// </summary>
    public Vector3 position;
    private Vector3 moveDirection;
    public Layer[] layer;
    public bool isInThisLayer = false;
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
    /// �����ƶ������屻ײ��ʱ�������������
    /// </summary>
    public virtual void DropThings()
    {

    }
    /// <summary>
    /// è��۲�ʱչʾ
    /// </summary>
    public virtual void Show()
    {

    }
    public virtual void Hide()
    {

    }
    public virtual void ScratchByDog()
    {

    }
}
