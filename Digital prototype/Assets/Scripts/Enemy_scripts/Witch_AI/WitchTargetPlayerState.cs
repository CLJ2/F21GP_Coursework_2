using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchTargetPlayer : WitchAiState
{
    public WitchAiStateID GetID()
    { 
        return WitchAiStateID.TargetPlayer;
    }
    
    public void Enter(WitchAiAgent agent)
    {
        //this acts similar to the start function
        Debug.Log("attackstate entered");
    }

    public void Update(WitchAiAgent agent)
    {
        if (!agent.enabled) return; //if agent is not enabled, wait frame

        agent.timer -= Time.deltaTime;  //timer is used to make sure the agent only ever calculates a new destination every second instead of every frame
        if(agent.timer < 0) //if the timer has reached 0
        {
            float srtDistance = (agent.playerTransform.position - agent.navMeshAgent.destination).sqrMagnitude; //calc distance to the player
            if (srtDistance < agent.config.minDistance)    //if the distance is greater than the maxdistance^2
            {
                Debug.Log(srtDistance);
                agent.navMeshAgent.isStopped = false;
                
                float angle= Mathf.Atan2(agent.playerTransform.position.z - agent.transform.position.z , agent.playerTransform.position.x - agent.transform.position.x);
                Vector3 destination = new Vector3(Mathf.Cos(angle)*agent.config.wantedDistance, agent.transform.position.y, Mathf.Sin(angle)*agent.config.wantedDistance);
                Debug.Log(destination);
                Debug.Log(agent.playerTransform.position);
                Debug.Log(agent.playerTransform.position- destination);
                Debug.Log(agent.transform.position);
                Debug.Log(agent.config.wantedDistance);
                Debug.Log(angle);
                
                agent.navMeshAgent.destination = agent.playerTransform.position - destination;    //move to the player
            }
            agent.timer = agent.config.maxTime; //reset timer
        }
        if (Vector3.Distance(agent.playerTransform.position, agent.transform.position) < agent.config.attackRange)  //if close enough to the player, attack player
        {
            //agent.player.takeDamage();    uncomment once player can take damage;
            //Debug.Log("attack");
        }
    }

    public void Exit(WitchAiAgent agent)
    {

    }  
}
