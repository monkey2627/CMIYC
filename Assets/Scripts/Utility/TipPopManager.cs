using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TipPopManager : MonoBehaviour
{
    public static TipPopManager instance { get; private set; }
    
    public float stayTime = 0.6f;
    public float showTime = 0.4f;
    public float hideTime = 0.4f;
    
    public Transform tipPanel;
    public Text tipText;
    
    private CanvasGroup tipCanvasGroup;

    private ConcurrentQueue<string> tipQueue = new ConcurrentQueue<string>();
    private Coroutine showTipCoroutine;
    private bool isShowing = false;
    private Tweener showTweener;
    private Tweener fadeTweener;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        tipCanvasGroup = tipPanel.GetComponent<CanvasGroup>();
        tipPanel.gameObject.SetActive(false);
        tipCanvasGroup.alpha = 0;
    }

    public void ShowTip(string tip)
    {
        tipQueue.Enqueue(tip);
        ShowNextTip();
    }

    private void ShowNextTip()
    {
        if (tipQueue.TryDequeue(out string currentTip))
        {
            isShowing = true;
            tipText.text = currentTip;
            tipPanel.gameObject.SetActive(true);
            showTweener.Kill();
            fadeTweener.Kill();
            showTweener = tipCanvasGroup.DOFade(1, showTime)
                .OnComplete(() => StartCoroutine(HideTipAfterDelay(currentTip)));
        }
        else
        {
            isShowing = false;
        }
    }

    private IEnumerator HideTipAfterDelay(string tip)
    {
        yield return new WaitForSeconds(stayTime + hideTime);
        fadeTweener = tipCanvasGroup.DOFade(0, hideTime)
            .OnComplete(() => tipPanel.gameObject.SetActive(false));
        ShowNextTip();
    }
    
    private void CloseTip()
    {
        if (isShowing)
        {
            StopAllCoroutines();
            tipCanvasGroup.DOFade(0, hideTime)
                .OnComplete(() => tipPanel.gameObject.SetActive(false));
        }
    }

}
