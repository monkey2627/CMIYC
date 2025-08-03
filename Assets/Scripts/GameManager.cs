using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool skipStart = true;
    public GameObject operationTipPanle;
    public GameObject endPanel;
    public static GameManager instance;
    public SceneBase[] scenes;
    public int sceneNow;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartNewGame();

        if (!skipStart)
        {
            TipPopManager.instance.ShowTip("Day 387 of House Arrest\nThe same scratching post. The same stupid window view. The same two-legged saying “who’s a good kitty?”" +
                                           " like I don’t understand EVERY condescending word!" +
                                           " Today ends differently. Time to show them why ancient Egyptians worshipped my kind……Meow~");
            TipPopManager.instance.OnTipClosed += ShowHintPanel;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            Win();
        if(Input.GetMouseButtonDown(0) && isEnding)
        {
            StartNewGame();
        }
    }
    public void StartNewGame()
    {
       
    }

    public void ShowHintPanel()
    {
        operationTipPanle.SetActive(true);
        CanvasGroup canvasGroup = operationTipPanle.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);

        TipPopManager.instance.OnTipClosed -= ShowHintPanel;
    }
    
    public GameObject Ending;
    public GameObject delay;
    public bool isEnding = false;
    /// <summary>
    /// 通关之后调用
    /// </summary>
    public void Win()
    {
        //一系列处理
        TipPopManager.instance.ShowTip(
            "You may possess opposable thumbs, two-legged...yet my escape route remains PERFECTLY untraceable. You’ll never catch me again! Meow~");
        TipPopManager.instance.OnTipClosed += ShowCG;
        
       
    }

    public void ShowCG()
    {
        endPanel.SetActive(true);
        CanvasGroup canvasGroup = endPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);

        TipPopManager.instance.ShowTip("Maybe next time……");
        TipPopManager.instance.OnTipClosed -= ShowCG;
    }

    public void OnCatCaught()
    {
        endPanel.SetActive(true);
    }

    private void OnEnable()
    {
        EventHandler.OnCatCaught += OnCatCaught;
    }

    private void OnDisable()
    {
        EventHandler.OnCatCaught -= OnCatCaught;
    }
    
}
