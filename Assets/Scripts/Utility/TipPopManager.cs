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
    private Text tipText;
    private Button closeBtn;
    
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
        tipText = tipPanel.GetComponentInChildren<Text>();
        closeBtn = tipPanel.GetComponentInChildren<Button>();
        tipCanvasGroup = tipPanel.GetComponent<CanvasGroup>();
        tipPanel.gameObject.SetActive(false);
        tipCanvasGroup.alpha = 0;
        closeBtn.onClick.AddListener(CloseTip);
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
            /*showTweener = tipCanvasGroup.DOFade(1, showTime)
                .OnComplete(() => StartCoroutine(HideTipAfterDelay(currentTip)));*/
            showTweener = tipCanvasGroup.DOFade(1, showTime);
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
            fadeTweener = tipCanvasGroup.DOFade(0, hideTime)
                .OnComplete(() => tipPanel.gameObject.SetActive(false));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            TipPopManager.instance.ShowTip(
                "You’ve always hated this painting. How dare your LOWLY SERVANT forget who the true master is—not even " +
                "a single PORTRAIT of you in this house?! Time to teach her what real art looks like…… Meow~");
        else if (Input.GetKeyDown(KeyCode.N))
            ShowTip("ehqitttt2222222222222");
    }
}

