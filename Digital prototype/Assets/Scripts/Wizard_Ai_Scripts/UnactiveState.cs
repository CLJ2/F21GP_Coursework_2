using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnactiveState : AiState
{
    public AiStateID GetStateID()
    {
        return AiStateID.Unactive;
    }

    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.enabled = false;
        agent.characterController.enabled = true;
        agent.thirdPersonController.enabled = true;
        agent.tag = "Player";
    }

    public void Update(AiAgent agent)
    {
        //Debug.Log(agent);
        //Debug.Log(agent.tag);
        if (agent.active == true) agent.stateMachine.ChangeState(AiStateID.Idle);
    }

    public void Exit(AiAgent agent)
    {
        agent.characterController.enabled = false;
        agent.navMeshAgent.enabled = true;
        agent.thirdPersonController.enabled = false;
        agent.tag = "AiPlayer";
    }
}
