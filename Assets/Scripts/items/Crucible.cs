using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crucible : ItemBase
{
    bool isSmoking = false;
    HerbType herb ;   // 坩埚内当前存在的药草
    bool hasHerb = false;
    public override void inter()
    {
        base.inter();
        if (enable)
        {
           if(Cat.instance.materialNumber != -1)
            {
                Cat.instance.herbSprites[Cat.instance.herbSpritesNumber].GetComponent<SpriteRenderer>().DOFade(0, 0.3f);
                Cat.instance.herbSprites[Cat.instance.herbSpritesNumber].SetActive(false);
                hasHerb = true;
                herb = (HerbType)Cat.instance.materialNumber;
                Cat.instance.materialNumber = -1;
            }
            else if(hasHerb)
            {
                if(herb == HerbType.JumpBud)
                {
                    //todo 获得效果
                    Cat.instance.tiaotiaoyaCount = 0;
                }
            }
        }
    }

    public void OnStoveStateChange(bool isOn)
    {
        isSmoking = isOn;
    }

    void OnEnable()
    {
        EventHandler.OnStoveStateChange += OnStoveStateChange;
    }

    private void OnDisable()
    {
        EventHandler.OnStoveStateChange -= OnStoveStateChange;
    }
}
