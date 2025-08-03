using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MasterIdleState : MasterState
{
    private Vector3 IdlePosition = Vector3.zero;
    private bool hasTransit = false;
    public MasterIdleState(MasterController master, MasterStateMachine masterStateMachine) : base(master, masterStateMachine)
    {
        StateId = "Idle";
    }
    
    public override void EnterState()
    {
        base.EnterState();
        
        if (IdlePosition == Vector3.zero)
            IdlePosition = master.transform.position;
        hasTransit = false;
    }

    public override void ExitState()
    {
        base.ExitState();
        master.Animator.SetBool("IsPlay", false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (Input.GetKeyDown(KeyCode.Y))
        {
            master.StateMachine.ChangeState(master.AlertState);
            return;
        }
        master.Animator.SetBool("IsIdle", false);
        master.Animator.SetBool("IsPlay", false);
        //master.Animator.SetFloat("FaceDirection", IdlePosition.x > master.transform.position.x ? 1f:0f);
        Vector3 targetPos = IdlePosition;
        
        if (master.HasTransit)
        {
            targetPos.x = master.TransitionX;
            targetPos.z = master.AttentionEvent.EventPlaceTrans.position.z;
            if (Mathf.Abs(master.transform.position.x - targetPos.x) < 0.05f)
            {
                master.HasTransit = false;
                master.transform.position = new Vector3(targetPos.x, master.transform.position.y, master.NormalZ);
                master.SetSortingLayer("Four");
            }
        }
        
        ChangeFaceDirection(targetPos);
        // 前往常态行为地点
        master.transform.position = Vector3.MoveTowards(
            master.transform.position, 
            targetPos, 
            master.MoveSpeed * Time.deltaTime
        );

        float distance = Vector3.Distance(master.transform.position, IdlePosition);
        if (distance < 0.05f) 
        {
            master.Animator.SetBool("IsIdle", true);
            master.Animator.SetBool("IsPlay", true);
            master.transform.position = IdlePosition;
            DoIdleAction();
        }
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public void DoIdleAction()
    {
        switch(master.IdleBehavior)
        {
            case IdleBehaviorEnum.WatchTV:
                break;
            case IdleBehaviorEnum.Cook:
                break;
            case IdleBehaviorEnum.PlayGame:
                break;
        }
    }
   
}
