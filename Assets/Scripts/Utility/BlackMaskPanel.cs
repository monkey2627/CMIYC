using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BlackMaskPanel : MonoBehaviour
{
    public static BlackMaskPanel instance;
    public CanvasGroup mask;
    public Action callBack;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mask.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Show();
    }

    public void Show()
    {
        mask.alpha = 1f;
        mask.gameObject.SetActive(true);
        mask.DOFade(0, 1f).From().onComplete += maskOff;
    }

    void maskOff()
    {
        if (callBack != null) callBack();
        mask.DOFade(0, 1f).SetDelay(0.5f).onComplete += () => {
            mask.gameObject.SetActive(false);
        };
    }

}
