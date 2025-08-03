using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterBusyState : MasterState
{
    private float _singleTimer = 0f;
    private int currentIndex = 0;
    
    private float _busyTimer = 0f;
    private float _busyTotalTime;

    private AttentionEventType busyType;
    // Start is called before the first frame update
    public MasterBusyState(MasterController master, MasterStateMachine masterStateMachine) : base(master, masterStateMachine)
    {
        StateId = "Busy";
    }

    public override void EnterState()
    {
        base.EnterState();
        _busyTimer = 0f;
        DoBusyAction();
        
        _singleTimer = 0f;
        currentIndex = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
        master.Animator.SetBool("IsUsingMagic", false);
        master.Animator.SetBool("IsBack", false);
        master.isOpeningDoor = false;
        EventHandler.ChangeDoorState(false);
        switch (master.AttentionEvent.EventType)
        {
            case AttentionEventType.ItemBroken:
                //master.Animator.SetBool("IsUsingMagic", false);
                break;
            case AttentionEventType.DogDestruction:
                //master.Animator.SetBool("IsUsingMagic", false);
                break;
            case AttentionEventType.GuestArrive:
                //master.Animator.SetBool("IsBack", false);
                //EventHandler.ChangeDoorState(false);
                //master.isOpeningDoor = false;
                break;
            case AttentionEventType.WildCatMeow:
                //master.Animator.SetBool("IsBack", false);
                EventHandler.MasterLeaveWindow();
                break;
            default:
                break;
        }
        
        master.ItemToFixList.Clear();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if (master.AttentionEvent.EventType == AttentionEventType.ItemBroken && master.ItemToFixList.Count == 0)
        {
            master.StateMachine.ChangeState(master.IdleState);
            return;
        }
        
        if (_busyTimer > _busyTotalTime)
        {
            _busyTimer = 0f;
            master.StateMachine.ChangeState(master.IdleState);
            return;
        }
        _busyTimer += Time.deltaTime;
        
        
        // 更新计时器
        _singleTimer += Time.deltaTime;

        // 检查是否达到播放动画的时间
        if (_singleTimer >= 3.8f && _busyTimer > currentIndex * 5f && currentIndex != -1)
        {
            PlayAnimationForCurrentItem();
            _singleTimer = 0f; // 重置计时器
        }
        _singleTimer += Time.deltaTime;
    }
    
    private void PlayAnimationForCurrentItem()
    {
        if (currentIndex < master.ItemToFixList.Count)
        {
            ItemBase currentItem = master.ItemToFixList[currentIndex];
            currentItem.PlayRecoverAnimation(); 
            
            currentIndex++;
        }
        else
        {
            currentIndex = -1;
        }
    }


    public void DoBusyAction()
    {
        busyType = master.AttentionEvent.EventType;
        switch (master.AttentionEvent.EventType)
        {
            case AttentionEventType.ItemBroken:
                _busyTotalTime = master.ItemToFixList.Count * 5f;
                master.Animator.SetBool("IsUsingMagic", true);
                break;
            case AttentionEventType.DogDestruction:
                _busyTotalTime = master.ItemToFixList.Count * 5f;
                master.Animator.SetBool("IsUsingMagic", true);
                break;
            case AttentionEventType.GuestArrive:
                _busyTotalTime = 15f;
                master.Animator.SetBool("IsBack", true);
                EventHandler.ChangeDoorState(true);
                DialogueManager.instance.StartDialogue();
                master.isOpeningDoor = true;
                break;
            case AttentionEventType.WildCatMeow:
                _busyTotalTime = 10f;
                master.Animator.SetBool("IsBack", true);
                break;
            default:
                _busyTotalTime = 0;
                break;
        }

    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
