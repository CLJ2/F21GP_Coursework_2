using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class IdleState : AiState
{
    public AiStateID GetStateID()
    {
        return AiStateID.Idle;
    }
    
    public void Enter(AiAgent agent)
    {
        
    }
    
    public void Update(AiAgent agent)
    {
        if (agent.active == false) agent.stateMachine.ChangeState(AiStateID.Unactive);

        agent.player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(agent.config.followStateDistance);
        if (Vector3.Dot(agent.transform.position, agent.player.transform.position) > agent.config.followStateDistance)
        {
            Debug.Log("here");
            agent.stateMachine.ChangeState(AiStateID.FollowPlayer);
        }

    }

    public void Exit(AiAgent agent)
    {
        
    }
}
