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
        float closestBarricadeDistance = Mathf.Infinity;
        GameObject closestBarricade = null;
        foreach (GameObject barricade in agent.barricades)
        {
            float distance = Vector3.Distance(barricade.transform.position, agent.transform.position);
            if (distance < closestBarricadeDistance)
            {
                closestBarricade = barricade;
                closestBarricadeDistance = distance;
            }
        }
        agent.barricade = closestBarricade;
    }
    
    public void Update(EnemyAiAgent agent)
    {
        if (!agent.enabled) return; //is agent is not enabled, skip
        if (agent.barricade == null) agent.stateMachine.ChangeState(EnemyAiStateID.Idle);
        if (Vector3.Distance(agent.barricade.transform.position, agent.transform.position) > agent.config.hideRange) agent.stateMachine.ChangeState(EnemyAiStateID.Idle);

        if (Vector3.Distance(agent.barricade.transform.position, agent.transform.position) > agent.config.hideRange)
        {
            if (Vector3.Distance(agent.transform.position, agent.barricade.transform.position) < agent.config.hideDistance)  //if the agent is less than the hide distance
            {
                agent.navMeshAgent.isStopped = true;    //stop agent from moving
                agent.timer -= Time.deltaTime;  //update timer
                if (agent.timer < 0.0f) //if timer is less than 0
                {
                    if (Random.Range(0, 11) < agent.config.unhideChance) agent.stateMachine.ChangeState(EnemyAiStateID.TargetPlayer);   //random chance the agent targets player
                    else agent.timer = agent.config.maxTime;    //if agent doesnt target player, reset timer
                }
            }
        }
    }

    public void Exit(EnemyAiAgent agent)
    {
        
    }
}
