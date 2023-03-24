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
        //add death handling here
        agent.ragdoll.ActivateRagdoll();
        agent.healthBar.gameObject.SetActive(false);
        direction.y = 0.5f;
        agent.ragdoll.ApplyForce(direction * agent.config.deathForce);
    }

    public void Update(EnemyAiAgent agent)
    {

    }
    
    public void Exit(EnemyAiAgent agent)
    {
        
    }
}
