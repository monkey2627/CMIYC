using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 快递箱
/// </summary>
public class ExpressBox : ItemBase
{
    public GameObject boxup;
    public GameObject boxDown;
    public GameObject luosidao;

    /// <summary>
    /// 被猫咪抓挠，被推动，被狗破坏
    /// </summary>
    public override void inter()
    {
        base.inter();
        Cat.instance.Scratch();
        enable = false;
        luosidao.SetActive(true);
        luosidao.GetComponent<LuoSiDao>().enable = true;
        boxup.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
        transform.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<BoxCollider>().isTrigger = true;
        boxDown.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
    }
    public override void ScratchByDog()
    {
        base.ScratchByDog();
        Dog.instance.animator.SetBool("Scratch", true);
        //破坏后有几率出现毛线球
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
