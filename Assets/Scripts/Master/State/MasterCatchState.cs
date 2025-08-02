using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCatchState : MasterState
{
    private bool isCatch = false;
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
            EventHandler.AttentionEventHappen(new AttentionEvent(master.transform, AttentionEventType.CatchCatFail));
            master.StateMachine.ChangeState(master.AlertState);
            return;
        }

        if (isCatch)
        {
            return;
        }

        Transform targetTrans = Cat.instance.transform;
        // 去追猫
        master.transform.position = Vector3.MoveTowards(
            master.transform.position, 
            new Vector3(targetTrans.position.x, master.transform.position.y, targetTrans.position.z), 
            master.MoveSpeed * Time.deltaTime * 2 // 以两倍速抓猫
        );
        master.Animator.SetBool("IsIdle", false);
        float distance = Mathf.Abs(master.transform.position.x - targetTrans.position.x);
        if (distance < master.DogLength)
        {
            isCatch = true;
            master.Animator.SetBool("IsIdle", true);
            // 抓到猫了，游戏失败！
            Debug.Log(" 抓到猫了，游戏失败！");
            EventHandler.CatchCat();
        }
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
