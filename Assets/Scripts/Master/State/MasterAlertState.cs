using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MasterAlertState : MasterState
{
    private float _obsTimer;
    private bool hasTransit = false;
    public MasterAlertState(MasterController master, MasterStateMachine masterStateMachine) : base(master, masterStateMachine)
    {
        StateId = "Alert";
    }

    public override void EnterState()
    {
        base.EnterState();
        _obsTimer = 0;
        hasTransit = false;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (master.ifSeeCat)
        {
            master.StateMachine.ChangeState(master.CatchState);
            return;
        }
        
        Transform targetTrans = master.AttentionEvent.EventPlaceTrans;
        Vector3 targetPos = new Vector3(targetTrans.position.x, master.transform.position.y, master.transform.position.z);

        if (master.hasNewEvent && master.HasTransit)
        {
            targetPos.x = master.TransitionX;
            if (Mathf.Abs(master.transform.position.x - targetPos.x) < 0.05f)
            {
                master.hasNewEvent = false;
                master.HasTransit = false;
                master.transform.position = new Vector3(targetPos.x, master.transform.position.y, master.NormalZ);
                targetPos.z = master.NormalZ;
                master.SetSortingLayer("Four");
            }
        }else if (master.AttentionEvent.TransitionPlaceX > -1e5f)
        {
            if (!hasTransit)
            {
                targetPos.x = master.AttentionEvent.TransitionPlaceX;
                if (Mathf.Abs(master.transform.position.x - targetPos.x) < 0.05f)
                {
                    hasTransit = true;
                    master.HasTransit = true;
                    master.TransitionX = targetPos.x;
                    master.transform.position = new Vector3(targetPos.x, master.transform.position.y, targetTrans.position.z);
                    master.SetSortingLayer("One");
                }
            }
            
        }
        
        // 前往关注事件位置
        master.transform.position = Vector3.MoveTowards(
            master.transform.position, 
            targetPos,
            master.MoveSpeed * Time.deltaTime
        );
        master.Animator.SetBool("IsIdle", false);
       // master.Animator.SetFloat("FaceDirection", targetPos.x > master.transform.position.x ? 1f:0f);
        ChangeFaceDirection(targetPos);
        float distance = Vector3.Distance(master.transform.position, targetPos);
        if (distance < 0.05f)
        {
            master.Animator.SetBool("IsIdle", true);
            master.transform.position = targetPos;
            master.IsObserving = true;
            if (_obsTimer > master.ObserveTime)   // Master has finish alert
            {
                master.IsObserving = false;
                _obsTimer = 0;
                master.SearchAroundSelf();
                if (!master.HasDogAround)
                {
                    /*// 有东西要修理 / 有客人来访
                    if (master.ItemToFixList.Count > 0 || master.AttentionEvent.EventType == AttentionEventType.GuestArrive)
                        master.StateMachine.ChangeState(master.BusyState);
                    else
                        master.StateMachine.ChangeState(master.IdleState);*/
                    master.StateMachine.ChangeState(master.BusyState);
                }
                else
                {
                    master.StateMachine.ChangeState(master.ScoldState);
                }
                return;

            }
            _obsTimer += Time.deltaTime;

        }
    }


    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
