using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class IdleState : AiState
{
    public AiStateID GetStateID()
    {
        return AiStateID.Idle;
    }

    public void Enter(AiAgent agent)
    {
        Debug.Log("Idle");
    }

    public void Update(AiAgent agent)
    {
        if (agent.active == false) agent.stateMachine.ChangeState(AiStateID.Unactive);

        agent.timer -= Time.deltaTime;
        if (agent.timer < 0.0f)
        {
            agent.player = GameObject.FindGameObjectWithTag("Player");
            if (Vector3.Distance(agent.transform.position, agent.player.transform.position) > agent.config.followStateDistance)
            {
                agent.stateMachine.ChangeState(AiStateID.FollowPlayer);
            }

            agent.enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in agent.enemies)
            {
                if (Vector3.Distance(enemy.transform.position, agent.transform.position) < agent.config.attackEnemyStateDistance)
                {
                    agent.stateMachine.ChangeState(AiStateID.TargetEnemy);
                }
            }

            agent.timer = agent.config.Timer;
        }

    }

    public void Exit(AiAgent agent)
    {

    }
}
