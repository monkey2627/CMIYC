using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crucible : ItemBase
{
    bool isSmoking = false;
    List<Herb> herbs = new List<Herb>(3);   // 坩埚内当前存在的药草
    public override void inter()
    {
        base.inter();
        if (!isSmoking)
        {
            // 判断是否叼着东西
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
