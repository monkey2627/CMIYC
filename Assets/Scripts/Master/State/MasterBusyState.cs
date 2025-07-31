using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterBusyState : MasterState
{
    // Start is called before the first frame update
    public MasterBusyState(MasterController master, MasterStateMachine masterStateMachine) : base(master, masterStateMachine)
    {
        StateId = "Busy";
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if (master.ItemToFixList.Count == 0)
        {
            master.StateMachine.ChangeState(master.IdleState);
            return;
        }
        
        // Loop the list, Fix item
        
        // Fix All the items
        master.StateMachine.ChangeState(master.IdleState);
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
