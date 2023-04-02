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
        agent.timer = agent.config.Timer;
        Debug.Log("following player");
    }

    public void Update(AiAgent agent)
    {
        if (agent.active == false) agent.stateMachine.ChangeState(AiStateID.Unactive);

        agent.player = GameObject.FindGameObjectWithTag("Player");
        agent.timer -= Time.deltaTime;
        if (agent.timer < 0.0f)
        {
            agent.navMeshAgent.destination = agent.player.transform.position;
            agent.timer = agent.config.Timer;

        }
        //Debug.Log(Vector3.Distance(agent.player.transform.position, agent.transform.position) < agent.config.followStateStopDistance);
        //Debug.Log(agent.config.followStateStopDistance);
        if (Vector3.Distance(agent.player.transform.position, agent.transform.position) < agent.config.followStateStopDistance)
        {
            agent.navMeshAgent.destination = agent.transform.position;
            agent.stateMachine.ChangeState(AiStateID.Idle);
        }
    }

    public void Exit(AiAgent agent)
    {

    }
}