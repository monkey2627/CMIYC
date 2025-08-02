using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exihibition : ItemBase
{
    public GameObject statue;
    public GameObject broken;
    public  static Exihibition instance;
    public Vector3[] vectors;
    private void Start()
    {
        instance = this;
        vectors = new Vector3[3];
        vectors[0] = new Vector3(0, 24.6f, 0);
        vectors[1] = new Vector3(0, 28.7f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        Cat.instance.isInExihibt = true;
        Cat.instance.rb.useGravity = false;
        Cat.instance.rb.isKinematic = true;
        Cat.instance.animator.SetBool("Jump", false);
        Cat.instance.animator.SetBool("Walk", false);
        Cat.instance.animator.SetBool("OnFloor", true);
        if (Cat.instance.transform.position.y >= 21.7 && Cat.instance.transform.position.y <= 26.8)
        {
            Cat.instance.transform.position = new Vector3(Cat.instance.transform.position.x, vectors[0].y, Cat.instance.transform.position.z);
            Cat.instance.exihibitLayer = 0;
        }
        else if (Cat.instance.transform.position.y > 26.8 && Cat.instance.transform.position.y <= 30.8)
        {
            Cat.instance.transform.position = new Vector3(Cat.instance.transform.position.x, vectors[1].y, Cat.instance.transform.position.z);
            Cat.instance.exihibitLayer = 1;
        }
    }
    public override void DropThings()
    {
        statue.GetComponent<Statue>().enable = false;
        statue.transform.DOLocalMove(new Vector3(statue.transform.position.x, -3.21f, statue.transform.position.z), 0.2f).OnComplete(
            ()=> {
                broken.SetActive(true);
                //ÎüÒıÖ÷ÈË
                AttentionEvent attentionEvent = new AttentionEvent(transform, AttentionEventType.ItemBroken);
                EventHandler.AttentionEventHappen(attentionEvent);
            });
    }
}
