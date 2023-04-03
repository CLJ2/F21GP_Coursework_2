using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchIdleState : WitchAiState
{
    public WitchAiStateID GetID()
    {
        return WitchAiStateID.Idle;
    }
    
    public void Enter(WitchAiAgent agent)
    {
        
    }

    public void Update(WitchAiAgent agent)
    {
        GameObject player = agent.playersArray[Random.Range(0, agent.playersArray.Length)];
        agent.playerTransform = player.transform;
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if (playerDirection.magnitude > agent.config.maxSightDistance) return;
        
        agent.stateMachine.ChangeState(WitchAiStateID.TargetPlayer);
    }
    
    public void Exit(WitchAiAgent agent)
    {
            
    }   
}
