using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class MasterController : MonoBehaviour
{
    [Tooltip("主人移动速度")]
    public float MoveSpeed;
    
    [Tooltip("每天初始的常态行为")]
    public IdleBehaviorEnum IdleBehavior;
    
    public Transform Cat;
    public Transform Dog;

    [Tooltip("警戒行为逗留时间")]
    public float ObserveTime = 2f;
    
    [Tooltip("训斥行为持续时间")]
    public float ScoldTime = 3.0f;

    public const float DogLength = 1f;
    
    public bool HasDogAround = false;
    
    public bool IsObserving = false;
    
    public bool ifSeeCat = false;
    
    public List<ItemBase> ItemToFixList = new List<ItemBase>(3);
    public List<ItemBase> BrokenItemList = new List<ItemBase>(3);
    
    //public List<Transform> NoiseSourceList = new List<Transform>(3);
    public Transform NoiseSource;
    
    private Animator Animator;

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
        EventHandler.OnNoiseEventHappen += OnHearNoise;
    }
    
    private void OnDisable()
    {
        EventHandler.OnNoiseEventHappen -= OnHearNoise;
    }

    private void Awake()
    {
        StateMachine = new MasterStateMachine();
        
        IdleState = new MasterIdleState(this, StateMachine);
        AlertState = new MasterAlertState(this, StateMachine);
        BusyState = new MasterBusyState(this, StateMachine);
        ScoldState = new MasterScoldState(this, StateMachine);
        CatchState = new MasterCatchState(this, StateMachine);
        
        if (Cat == null)
            Cat = FindObjectOfType<Cat>().transform;
        if (Dog == null)
            Dog = FindObjectOfType<Dog>().transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
       StateMachine.CurState.UpdateState();
       CheckIfSeeCat();
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurState.AnimationTriggerEvent(triggerType);
    }

    public void CheckIfSeeCat()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ifSeeCat = !ifSeeCat;
        }
    }
    
    /// <summary>
    /// 在主人身边一定半径搜寻是否有狗和待修的物品
    /// </summary>
    public void SearchAroundSelf()
    {
        // Check Items and Dog
        float posDiff = Dog.position.x - this.transform.position.x;
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
    /// <param name="noisePlace">发出动静的位置</param>
    public void OnHearNoise(Transform noisePlace)
    {
        /*// 当前已经到达某个关注事件的位置，处理中
        if (StateMachine.CurState == AlertState && IsObserving == true)
        {
            return;
        }*/
        
        NoiseSource = noisePlace;
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
