using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DownedState : AiState
{
    public AiStateID GetStateID()
    {
        return AiStateID.Downed;
    }

    public void Enter(AiAgent agent)
    {
        agent.animator.SetBool("Downed", true);
    }

    public void Update(AiAgent agent)
    {
        if (agent.health > 0.0f)
        {
            Debug.Log("healing");
            agent.stateMachine.ChangeState(AiStateID.Idle);
        }
    }

    public void Exit(AiAgent agent)
    {
 
    }
}