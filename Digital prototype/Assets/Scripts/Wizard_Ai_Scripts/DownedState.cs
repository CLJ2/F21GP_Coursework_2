using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownedState : AiState
{
    public AiStateID GetStateID()
    {
        return AiStateID.Downed;
    }
    
    public void Enter(AiAgent agent)
    {
        
    }
    
    public void Update(AiAgent agent)
    {
        if (agent.health > 0) agent.stateMachine.ChangeState(AiStateID.Idle);
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
