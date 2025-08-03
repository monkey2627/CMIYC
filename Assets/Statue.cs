using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : ItemBase
{
    public GameObject broken;
    private void Update()
    {
        if(GetComponent<Rigidbody>().velocity.y < -0.5f)
        {
            Debug.Log("sjsah");
            Drop();

        }
    }
    public void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Statue>().enable = false;
        transform.DOLocalMove(new Vector3(transform.localPosition.x, -3.21f,transform.localPosition.z), 3f).OnComplete(
            () => {
                broken.SetActive(true);
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                gameObject.SetActive(false);
                gameObject.transform.position = new Vector3(transform.localPosition.x, -3.21f, transform.localPosition.z);
                //吸引主人
                AttentionEvent attentionEvent = new AttentionEvent(transform, AttentionEventType.ItemBroken);
                EventHandler.ItemBroke(this);
                EventHandler.AttentionEventHappen(attentionEvent);
            });
    }
    /// <summary>
    /// 猫把展品推下去
    /// </summary>
    public override void inter()
    {
        base.inter();
        //显示文本
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            Cat.instance.Push();
        }
    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Cat"))
        {
            Cat.instance.StopPush();
        }
    }
}
