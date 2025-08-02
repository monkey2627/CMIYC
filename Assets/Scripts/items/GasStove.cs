using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStove : ItemBase
{
    public GameObject fire;
    public GameObject flog;
    public Crucible crucible;
    /// <summary>
    /// 关闭燃气灶，使坩埚停止冒烟
    /// </summary>
    public override void inter()
    {
        base.inter();
        if (!enable) return;
        fire.GetComponent<SpriteRenderer>().DOFade(0, 0.2f).OnComplete(() =>
        {
            flog.GetComponent<SpriteRenderer>().DOFade(0, 0.3f);
            crucible.enable = true;
        });

        enable = false;
    }
    public override void Show()
    {
        if(enable)
        base.Show();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
