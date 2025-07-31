using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAlertState : MasterState
{
    private float _obsTimer;
    
    public MasterAlertState(MasterController master, MasterStateMachine masterStateMachine) : base(master, masterStateMachine)
    {
        StateId = "Alert";
    }

    public override void EnterState()
    {
        base.EnterState();
        _obsTimer = 0;
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
        
        Vector3 targetPos = master.NoiseSource.position;
        
        // 前往关注事件位置
        master.transform.position = Vector3.MoveTowards(
            master.transform.position, 
            targetPos, 
            master.MoveSpeed * Time.deltaTime
        );
        
        float distance = Vector3.Distance(master.transform.position, targetPos);
        if (distance < 0.05f)
        {
            master.transform.position = targetPos;
            master.IsObserving = true;
            if (_obsTimer > master.ObserveTime)   // Master has finish alert
            {
                master.IsObserving = false;
                _obsTimer = 0;
                
                if (!master.HasDogAround)
                {
                    if (master.ItemToFixList.Count > 0)     // has something to fix
                        master.StateMachine.ChangeState(master.BusyState);
                    else
                        master.StateMachine.ChangeState(master.IdleState);
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
