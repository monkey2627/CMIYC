using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCatchState : MasterState
{
    public MasterCatchState(MasterController master, MasterStateMachine masterStateMachine) : base(master, masterStateMachine)
    {
        StateId = "Catch";
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
        if (master.ifSeeCat == false)
        {
            master.StateMachine.ChangeState(master.AlertState);
            return;
        }
        
        // 去追猫
        master.transform.position = Vector3.MoveTowards(
            master.transform.position, 
            master.Cat.position, 
            master.MoveSpeed * Time.deltaTime * 2 // 以两倍速抓猫
        );
        float distance = Vector3.Distance(master.transform.position, master.Cat.position);
        if (distance < MasterController.DogLength)
        {
            // 抓到猫了，游戏失败！
            Debug.Log(" 抓到猫了，游戏失败！");
        }
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
