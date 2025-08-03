using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongfengKou :ItemBase
{
    public static TongfengKou instance;
    public GameObject open;
    public GameObject luoSi;
    public GameObject one;
    public GameObject two;
    private void Start()
    {
        instance = this;
    }
    //有螺丝才能交互
    public override void inter()
    {
        base.inter();
        Cat.instance.luosiItem.SetActive(false);
        luoSi.SetActive(true);
    }
    public void Open()
    {
        one.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() => {

            two.GetComponent<SpriteRenderer>().DOFade(1, 1).OnComplete(()=> {

               open.GetComponent<Paiqikouopen>().enable = true;
            });
        });
    }

}
