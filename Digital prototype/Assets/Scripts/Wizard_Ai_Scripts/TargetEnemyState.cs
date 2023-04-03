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
                if (enemy.GetComponent<EnemyHealth>() != null){
                    if (distance < closestEnemyDistance && enemy.GetComponent<EnemyHealth>().health > 0)
                    {
                        closestEnemyDistance = distance;
                        closestEnemy = enemy;
                    }
                }
                else if (enemy.GetComponent<WitchHealth>() != null){
                    if (distance < closestEnemyDistance && enemy.GetComponent<WitchHealth>().health > 0)
                    {
                        closestEnemyDistance = distance;
                        closestEnemy = enemy;
                    }
                }
                else Debug.Log("health issues ");
            }
            if (Vector3.Distance(closestEnemy.transform.position, agent.transform.position) < agent.config.attackEnemyStateDistance) 
            {
                agent.navMeshAgent.destination = closestEnemy.transform.position; 
            }
            if (Vector3.Distance(closestEnemy.transform.position, agent.transform.position) < agent.config.attackRange)
            {
                agent.navMeshAgent.destination = agent.transform.position;
                agent.transform.LookAt(closestEnemy.transform.position);
                //add attack here
                if (closestEnemy.GetComponent<EnemyHealth>() != null){
                    if (closestEnemy.GetComponent<EnemyHealth>().health <= 0) agent.stateMachine.ChangeState(AiStateID.Idle);
                }
                else if (closestEnemy.GetComponent<WitchHealth>() != null){
                    if (closestEnemy.GetComponent<WitchHealth>().health <= 0) agent.stateMachine.ChangeState(AiStateID.Idle);
                }
                else Debug.Log("health issues");
                    
            }
            if (closestEnemy.GetComponent<EnemyHealth>() != null){
                if (closestEnemy != null || closestEnemy.GetComponent<EnemyHealth>().health <= 0) agent.stateMachine.ChangeState(AiStateID.Idle);
            }
            else if (closestEnemy.GetComponent<WitchHealth>() != null){
                if (closestEnemy != null || closestEnemy.GetComponent<WitchHealth>().health <= 0) agent.stateMachine.ChangeState(AiStateID.Idle);
            }
            else Debug.Log("issue changing nearest enemy to idle");
            agent.timer = agent.config.Timer;
        }
    }

    public void Exit(AiAgent agent)
    {

    }
}