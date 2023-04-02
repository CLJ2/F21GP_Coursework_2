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
        agent.animator.CrossFade("idle_combat", 0.5f);
    }

    public void Update(WitchAiAgent agent)
    {
        if (!agent.enabled) return; //if agent is not enabled, wait frame

        agent.timer -= Time.deltaTime;  //timer is used to make sure the agent only ever calculates a new destination every second instead of every frame
        agent.spellTimer -= Time.deltaTime; 
        if(agent.timer < 0) //if the timer has reached 0
        {
            float srtDistance = (agent.playerTransform.position - agent.navMeshAgent.destination).sqrMagnitude; //calc distance to the player
            if (srtDistance < agent.config.minDistance*agent.config.minDistance || srtDistance > agent.config.maxDistance*agent.config.maxDistance)    //if the distance is greater than the maxdistance^2
            {
                agent.navMeshAgent.isStopped = false;
                
                float angle= Mathf.Atan2(agent.playerTransform.position.z - agent.transform.position.z , agent.playerTransform.position.x - agent.transform.position.x);
                Vector3 destination = new Vector3(Mathf.Cos(angle)*agent.config.wantedDistance, agent.transform.position.y, Mathf.Sin(angle)*agent.config.wantedDistance);
                agent.navMeshAgent.destination = agent.playerTransform.position - destination;    //move to the player
            }
            agent.timer = agent.config.maxTime; //reset timer
        }

        if (Vector3.Distance(agent.playerTransform.position, agent.transform.position) < agent.config.attackRange && agent.spellTimer < 0)  //if close enough to the player & timer is complete, attack player
        {
            if (Random.Range(0, 4) < 2) healSpell(agent);
            else attackSpell(agent);
            agent.spellTimer = agent.config.spellCooldown;
        }
    }

    public void healSpell(WitchAiAgent agent){
        Debug.Log("healing!");
        agent.animator.CrossFade("attack_short_001",0.5f);
        agent.animator.CrossFadeQueued("idle_combat", 0.5f);
        
    }

    public void attackSpell(WitchAiAgent agent){
        Debug.Log("attacking!");
        agent.animator.CrossFade("attack_short_001",0.5f);
        agent.animator.CrossFadeQueued("idle_combat", 0.5f);
    }

    public void Exit(WitchAiAgent agent)
    {

    }  
}
