using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetPlayer : EnemyAiState
{
    public EnemyAiStateID GetID()
    { 
        return EnemyAiStateID.TargetPlayer;
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
                agent.navMeshAgent.isStopped = false;
                agent.navMeshAgent.destination = agent.playerTransform.position;
                Debug.Log(agent.navMeshAgent.destination);
            }
            agent.timer = agent.config.maxTime;
        }
        if (Vector3.Distance(agent.playerTransform.position, agent.transform.position) < agent.config.attackRange)
        {
            //agent.player.takeDamage();    uncomment once player can take damage;
            Debug.Log("attack");
        }
    }

    public void Exit(EnemyAiAgent agent)
    {

    }  
}
