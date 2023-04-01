using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnactiveState : AiState
{
    public AiStateID GetStateID()
    {
        return AiStateID.Unactive;
    }
    
    public void Enter(AiAgent agent)
    {
        agent.characterController.enabled = true;
        agent.navMeshAgent.enabled = false;
        agent.tag = "Player";
    }
    
    public void Update(AiAgent agent)
    {
        if (agent.active == true) agent.stateMachine.ChangeState(AiStateID.Idle); 
    }

    public void Exit(AiAgent agent)
    {
        agent.characterController.enabled = false;
        agent.navMeshAgent.enabled = true;
        agent.tag = "AiPlayer";
    }    
}
