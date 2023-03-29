using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerState : AiState
{
    public AiStateID GetStateID()
    {
        return AiStateID.FollowPlayer;
    }
    
    public void Enter(AiAgent agent)
    {
        agent.timer = agent.config.followPlayerCheckTimer;
        agent.GetComponent<CharacterController>().enabled = false;
    }
    
    public void Update(AiAgent agent)
    {
        if (agent.active == false) agent.stateMachine.ChangeState(AiStateID.Unactive);

        agent.player = GameObject.FindGameObjectWithTag("Player");
        agent.timer -= Time.deltaTime;
        if(agent.timer < 0.0f)
        {
            agent.navMeshAgent.destination = agent.player.transform.position;
            agent.timer = agent.config.followPlayerCheckTimer;
        }
    }

    public void Exit(AiAgent agent)
    {
        agent.GetComponent<CharacterController>().enabled = true;
    }
}