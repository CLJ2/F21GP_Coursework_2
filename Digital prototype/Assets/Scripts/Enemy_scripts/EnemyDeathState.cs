using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyAiState
{
    public Vector3 deathDirection;

    public EnemyAiStateID GetID()
    {
        return EnemyAiStateID.Dead;
    }

    public void Enter(EnemyAiAgent agent)
    {
        agent.navMeshAgent.destination = agent.transform.position;
        agent.ragdoll.ActivateRagdoll();    //upon death, turn on the ragdoll physics
        deathDirection.y = 0.5f;    //give direction of death a heigh to make the agent lift up at the start of death
        agent.ragdoll.ApplyForce(deathDirection * agent.config.deathForce); //apply the force to the agent
        //Debug.Log(agent.healthBar.enabled);
        //agent.healthBar.gameObject.SetActive(false);    //turn off health bar
    }

    public void Update(EnemyAiAgent agent)
    {

    }
    
    public void Exit(EnemyAiAgent agent)
    {
        
    }
}
