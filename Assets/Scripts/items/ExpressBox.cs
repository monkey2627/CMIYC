using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����
/// </summary>
public class ExpressBox : ItemBase
{
    public GameObject boxup;
    public GameObject boxDown;
    public GameObject luosidao;
    public GameObject cube;

    /// <summary>
    /// ��è��ץ�ӣ����ƶ��������ƻ�
    /// </summary>
    public override void inter()
    {
        Debug.Log("scratch");
        base.inter();
        cube.SetActive(false);
        Cat.instance.Scratch();
        enable = false;
        luosidao.SetActive(true);
        luosidao.GetComponent<LuoSiDao>().enable = true;
        luosidao.GetComponent<LuoSiDao>().isInThisLayer = true;
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
        //�ƻ����м��ʳ���ë����
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
