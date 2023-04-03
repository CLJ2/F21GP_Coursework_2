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
        if (!agent.enabled) return; //if agent is not enabled, wait frame

        agent.timer -= Time.deltaTime;  //timer is used to make sure the agent only ever calculates a new destination every second instead of every frame
        if(agent.timer < 0) //if the timer has reached 0
        {
            float srtDistance = (agent.playerTransform.position - agent.navMeshAgent.destination).sqrMagnitude; //calc distance to the player
            if (srtDistance > agent.config.minDistance*agent.config.minDistance)    //if the distance is greater than the maxdistance^2
            {
                agent.navMeshAgent.isStopped = false;
                agent.navMeshAgent.destination = agent.playerTransform.position;    //move to the player
            }
            agent.timer = agent.config.maxTime; //reset timer
        }
        if (Vector3.Distance(agent.playerTransform.position, agent.transform.position) < agent.config.attackRange)  //if close enough to the player, attack player
        {
            agent.transform.LookAt(agent.playerTransform.position);
            agent.animator.SetTrigger("Attack");
            if (agent.player.GetComponent<WizardHealth>().health <= 0) agent.stateMachine.ChangeState(EnemyAiStateID.Idle);
        }
    }

    public void Exit(EnemyAiAgent agent)
    {

    }  
}
