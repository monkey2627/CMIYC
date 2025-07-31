using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScoldState : MasterState
{
    private float _scoldTimer;
    public MasterScoldState(MasterController master, MasterStateMachine masterStateMachine) : base(master, masterStateMachine)
    {
        StateId = "Scold";
    }


    public override void EnterState()
    {
        base.EnterState();
        _scoldTimer = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_scoldTimer >= master.ScoldTime)
        {
            _scoldTimer = 0;
            if (master.ItemToFixList.Count > 0)
            {
                master.StateMachine.ChangeState(master.BusyState);
            }
            else
            {
                master.StateMachine.ChangeState(master.IdleState);
            }
        }
        
        _scoldTimer += Time.deltaTime;
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
