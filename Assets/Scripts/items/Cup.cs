using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : ItemBase
{
    private Rigidbody rb;
    public GameObject crash;
    public bool firstCollision = true;
    public GameObject delay;
    public GameObject crashIcon;
    public override void inter()
    {
        Cat.instance.Push(); 
        enable = false;
        delay.transform.DOMove(new Vector3(0, 0, 0), 1f).OnComplete(() => { Cat.instance.StopPush(); });
        transform.DOLocalMove(new Vector3(-80.5f, -40.43f, transform.localPosition.z),2f).OnComplete(()=> {

          
            crashIcon.SetActive(true);
            transform.GetComponent<SpriteRenderer>().DOFade(0, 0.2f).OnComplete(()=> {  
                
                crashIcon.SetActive(false);
                crash.SetActive(true);
               
            });
            gameObject.SetActive(false);
            //吸引主人
            AttentionEvent attentionEvent = new AttentionEvent(transform, AttentionEventType.ItemBroken);
            EventHandler.ItemBroke(this);
            EventHandler.AttentionEventHappen(attentionEvent);

        });
    }

    public override void PlayRecoverAnimation()
    {
        Animator animator = crash.GetComponent<Animator>();
        if (animator != null)
            animator.Play("ItemRecover");
    }
}
