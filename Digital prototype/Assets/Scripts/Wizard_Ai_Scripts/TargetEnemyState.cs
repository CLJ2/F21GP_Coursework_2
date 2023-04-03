using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetEnemyState : AiState
{
    public AiStateID GetStateID()
    {
        return AiStateID.TargetEnemy;
    }

    public void Enter(AiAgent agent)
    {
        Debug.Log("Attack Enemy");
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
                if (distance < closestEnemyDistance && enemy.GetComponent<EnemyHealth>().health > 0)
                {
                    closestEnemyDistance = distance;
                    closestEnemy = enemy;
                }
            }
            if (Vector3.Distance(closestEnemy.transform.position, agent.transform.position) < agent.config.attackEnemyStateDistance) 
            {
                Debug.Log(agent.navMeshAgent.isActiveAndEnabled);
                agent.navMeshAgent.destination = closestEnemy.transform.position; 
            }
            if (Vector3.Distance(closestEnemy.transform.position, agent.transform.position) < agent.config.attackRange)
            {
                agent.navMeshAgent.destination = agent.transform.position;
                agent.transform.LookAt(closestEnemy.transform.position);
                //add attack here
                if (closestEnemy.GetComponent<EnemyHealth>().health <= 0) agent.stateMachine.ChangeState(AiStateID.Idle);
            }
            if (closestEnemy != null || closestEnemy.GetComponent<EnemyHealth>().health <= 0) agent.stateMachine.ChangeState(AiStateID.Idle);
            agent.timer = agent.config.Timer;
        }
        Debug.Log("target");
    }

    public void Exit(AiAgent agent)
    {

    }
}