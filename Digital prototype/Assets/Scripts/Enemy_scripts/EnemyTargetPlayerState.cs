using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetPlayer : EnemyAiState
{
    public EnemyAiStateID GetID()
    { 
        return EnemyAiStateID.targetPlayer;
    }
    
    public void Enter(EnemyAiAgent agent)
    {
        //this acts similar to the start function
    }

     public void Update(EnemyAiAgent agent)
    {
        //implement enemy movement here
        if (!agent.enabled) return;

        agent.timer -= Time.deltaTime;
        if(agent.timer < 0)
        {
            float srtDistance = (agent.playerTransform.position - agent.navMeshAgent.destination).sqrMagnitude;
            if (srtDistance > agent.config.minDistance*agent.config.minDistance)
            {
                agent.navMeshAgent.destination = agent.playerTransform.position;
            }
            agent.timer = agent.config.maxTime;
        }
    }

    public void Exit(EnemyAiAgent agent)
    {

    }  
}
