using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class MasterController : MonoBehaviour
{
    public static MasterController instance;
    
    [Tooltip("主人移动速度")]
    public float MoveSpeed;
    
    [Tooltip("每天初始的常态行为")]
    public IdleBehaviorEnum IdleBehavior;

    [Tooltip("警戒行为逗留时间")]
    public float ObserveTime = 2f;
    
    [Tooltip("训斥行为持续时间")]
    public float ScoldTime = 3.0f;

    public float DogLength = 1f;
    
    public float FaceDirection = 1;
    
    public bool HasDogAround = false;
    
    public bool IsObserving = false;
    
    public bool ifSeeCat = false;
    
    public bool isOpeningDoor = false;
    
    public List<ItemBase> ItemToFixList = new List<ItemBase>(3);
    public List<ItemBase> BrokenItemList = new List<ItemBase>(3);
    
    [SerializeField]
    public AttentionEvent AttentionEvent;
    [SerializeField]
    public List<AttentionEvent> AttentionEventList = new List<AttentionEvent>(3); // 未处理完的关注事件
    
    public Animator Animator;
    
    public float NormalZ = 0f;
    public bool HasTransit = false;
    public float TransitionX;
    public bool hasNewEvent = false;

    public AttentionEvent testEvet;

    #region StateMachine

    public MasterStateMachine StateMachine;
    public MasterState IdleState;
    public MasterState AlertState;
    public MasterState BusyState;
    public MasterState ScoldState;
    public MasterState CatchState;

    #endregion


    private void OnEnable()
    {
        EventHandler.OnAttentionEventHappen += OnAttentionEventHappen;
    }
    
    private void OnDisable()
    {
        EventHandler.OnAttentionEventHappen -= OnAttentionEventHappen;
    }

    private void Awake()
    {
        instance = this;
        
        StateMachine = new MasterStateMachine();
        
        IdleState = new MasterIdleState(this, StateMachine);
        AlertState = new MasterAlertState(this, StateMachine);
        BusyState = new MasterBusyState(this, StateMachine);
        ScoldState = new MasterScoldState(this, StateMachine);
        CatchState = new MasterCatchState(this, StateMachine);
        
        Animator = GetComponent<Animator>();
        
        NormalZ = this.transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        StateMachine.Initialize(IdleState);
        DogLength = Dog.instance.gameObject.GetComponent<BoxCollider>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
       StateMachine.CurState.UpdateState();
       CheckIfSeeCat();
       if (Input.GetKeyDown(KeyCode.R))
       {
           EventHandler.AttentionEventHappen(testEvet); 
       }
    }

    public void CheckIfSeeCat()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ifSeeCat = !ifSeeCat;
        }

        if (Cat.instance.isHiding)
        {
            ifSeeCat = false;
            return;
        }

        if (Mathf.Sign(Cat.instance.transform.position.x - transform.position.x) == Mathf.Sign(FaceDirection))
        {
            //ifSeeCat = true;
        }
    }
    
    /// <summary>
    /// 在主人身边一定半径搜寻是否有狗和待修的物品
    /// </summary>
    public void SearchAroundSelf()
    {
        // Check Items and Dog
        float posDiff = Dog.instance.transform.position.x - this.transform.position.x;
        if (Math.Abs(posDiff) < 3 * DogLength)
            HasDogAround = true;
        else
            HasDogAround = false;
    }

    public void OnItemBroke(ItemBase item)
    {
        BrokenItemList.Add(item);
    }

    /// <summary>
    /// 当出现了关注事件，尝试更新警戒行为的目的地
    /// </summary>
    /// <param name="attentionEvent">发生的关注事件</param>
    public void OnAttentionEventHappen(AttentionEvent attentionEvent)
    {
        /*// 当前已经到达某个关注事件的位置，处理中
        if (StateMachine.CurState == AlertState && IsObserving == true)
        {
            AttentionEventList.Add(attentionEvent);
            return;
        }*/
        
        AttentionEvent = attentionEvent;
        hasNewEvent = true;
        
        if (StateMachine.CurState == IdleState)
            StateMachine.ChangeState(AlertState);
    }
    
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurState.AnimationTriggerEvent(triggerType);
    }

    public void SetSortingLayer(string layerName)
    {
        this.GetComponent<SpriteRenderer>().sortingLayerName = layerName;
    }
}

public enum IdleBehaviorEnum
{
    WatchTV = 0,
    Cook = 1,
    PlayGame = 2,
}


public enum AnimationTriggerType
{
        
}
