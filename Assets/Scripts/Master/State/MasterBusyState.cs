using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterBusyState : MasterState
{
    private float _busyTimer = 0f;
    private float _busyTotalTime;
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
    }

    public override void ExitState()
    {
        base.ExitState();
        master.Animator.SetBool("IsUsingMagic", false);
        master.Animator.SetBool("IsBack", false);
        master.isOpeningDoor = false;
        EventHandler.ChangeDoorState(false);
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
        }
        _busyTimer += Time.deltaTime;
        
    }

    public void DoBusyAction()
    {
        switch (master.AttentionEvent.EventType)
        {
            case AttentionEventType.ItemBroken:
                _busyTotalTime = master.ItemToFixList.Count * 5f;
                master.Animator.SetBool("IsUsingMagic", true);
                break;
            case AttentionEventType.GuestArrive:
                _busyTotalTime = 10f;
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
