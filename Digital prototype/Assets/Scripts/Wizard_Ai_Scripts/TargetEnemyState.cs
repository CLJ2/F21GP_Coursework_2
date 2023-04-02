using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemyState : AiState
{
    public AiStateID GetStateID()
    {
        return AiStateID.TargetEnemy;
    }

    public void Enter(AiAgent agent)
    {
        //Debug.Log("Attack Enemy");
    }

    public void Update(AiAgent agent)
    {
        if (agent.active == false) agent.stateMachine.ChangeState(AiStateID.Unactive);

        float closestEnemyDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        agent.timer -= Time.deltaTime;
        if (agent.timer < 0.0f)
        {
            foreach (GameObject enemy in agent.enemies)
            {
                float distance = Vector3.Distance(agent.transform.position, enemy.transform.position);
                if (distance < closestEnemyDistance)
                {
                    closestEnemyDistance = distance;
                    closestEnemy = enemy;
                }
            }
            agent.navMeshAgent.destination = closestEnemy.transform.position;
            if (Vector3.Distance(closestEnemy.transform.position, agent.transform.position) < agent.config.attackRange)
            {
                agent.navMeshAgent.destination = agent.transform.position;
                //add attack here
                agent.stateMachine.ChangeState(AiStateID.Idle);
            }
            agent.timer = agent.config.Timer;
        }
    }

    public void Exit(AiAgent agent)
    {

    }
}