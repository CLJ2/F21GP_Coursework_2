using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHideState : EnemyAiState
{
    public EnemyAiStateID GetID()
    {
        return EnemyAiStateID.Hide;
    }
    
    public void Enter(EnemyAiAgent agent)
    {
        agent.barricade = agent.barricades[Random.Range(0, agent.barricades.Length)];
        agent.navMeshAgent.destination = agent.barricade.gameObject.transform.position;
        agent.timer = agent.config.maxTime;
    }
    
    public void Update(EnemyAiAgent agent)
    {
        if (!agent.enabled) return;

        if (Vector3.Distance(agent.transform.position, agent.barricade.transform.position) < 1.5f)
        {
            agent.navMeshAgent.isStopped = true;
            agent.timer -= Time.deltaTime;
            if (agent.timer < 0.0f)
            {
                if (Random.Range(0, 11) < agent.config.unhideChance) agent.stateMachine.ChangeState(EnemyAiStateID.TargetPlayer);
                else agent.timer = agent.config.maxTime;
            }
        }
    }

    public void Exit(EnemyAiAgent agent)
    {
        
    }    
}
