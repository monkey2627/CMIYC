using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterStateMachine
{
    public MasterState CurState { get; set; }
    public MasterState LastState { get; set; }

    public void Initialize(MasterState startingState)
    {
        LastState = startingState;
        CurState = startingState;
        CurState.EnterState();
    }

    public void ChangeState(MasterState newState)
    {
        LastState = CurState;
        
        CurState.ExitState();
        CurState = newState;
        CurState.EnterState();
    }
}
