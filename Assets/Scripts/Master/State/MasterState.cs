using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterState
{
    protected MasterController master;
    protected MasterStateMachine masterStateMachine;
    protected string StateId = "Base";
    protected float _Timer = 0f;

    public MasterState(MasterController master, MasterStateMachine masterStateMachine)
    {
        this.master = master;
        this.masterStateMachine = masterStateMachine;
    }

    public virtual void EnterState()
    {
        Debug.Log("Enter State [" + StateId + "]");
        _Timer = 0f;
    }
    public virtual void ExitState(){}
    
    public virtual void UpdateState()
    {
        if (_Timer > 0.5f)
        {
            //Debug.Log("lasttate:" + masterStateMachine.LastState.StateId);
            //Debug.Log("curtate:" + masterStateMachine.CurState.StateId);
            _Timer = 0;
        }
        _Timer += Time.deltaTime;
    }

    public virtual void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
    }

}
