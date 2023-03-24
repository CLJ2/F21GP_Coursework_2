using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyAiState
{
    public EnemyAiStateID GetID()
    {
        return EnemyAiStateID.Idle;
    }
    
    public void Enter(EnemyAiAgent agent)
    {
        
    }

    public void Update(EnemyAiAgent agent)
    {
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if (playerDirection.magnitude > agent.config.maxSightDistance) return;

        Vector3 agentDirection = agent.transform.forward;
        playerDirection.Normalize();
        float dotProduct = Vector3.Dot(agentDirection, playerDirection);
        if (dotProduct > 0.0f) 
        {
            if (Random.Range(0, 11) < 5) agent.stateMachine.ChangeState(EnemyAiStateID.TargetPlayer);
            else agent.stateMachine.ChangeState(EnemyAiStateID.Hide);
        }
    }
    
    public void Exit(EnemyAiAgent agent)
    {
            
    }   
}
