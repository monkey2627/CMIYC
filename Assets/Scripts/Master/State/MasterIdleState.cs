using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MasterIdleState : MasterState
{
    private Vector3 IdlePosition = Vector3.zero;
    public MasterIdleState(MasterController master, MasterStateMachine masterStateMachine) : base(master, masterStateMachine)
    {
        StateId = "Idle";
    }
    
    public override void EnterState()
    {
        base.EnterState();
        
        if (IdlePosition == Vector3.zero)
            IdlePosition = master.transform.position;
    }

    public override void ExitState()
    {
        base.ExitState();
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
        //master.Animator.SetFloat("FaceDirection", IdlePosition.x > master.transform.position.x ? 1f:0f);
        ChangeFaceDirection(IdlePosition);
        // 前往常态行为地点
        master.transform.position = Vector3.MoveTowards(
            master.transform.position, 
            IdlePosition, 
            master.MoveSpeed * Time.deltaTime
        );

        float distance = Vector3.Distance(master.transform.position, IdlePosition);
        if (distance < 0.05f) 
        {
            master.Animator.SetBool("IsIdle", true);
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
