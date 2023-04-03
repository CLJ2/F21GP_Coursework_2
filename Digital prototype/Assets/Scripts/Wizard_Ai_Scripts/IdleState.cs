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
        //Debug.Log("Idle");
    }

    public void Update(AiAgent agent)
    {
        if (agent.active == false) agent.stateMachine.ChangeState(AiStateID.Unactive);

        agent.timer -= Time.deltaTime;
        if (agent.timer < 0.0f)
        {
            agent.player = GameObject.FindGameObjectWithTag("Player");
            //Debug.Log(agent.config.followStateDistance);
            if (Vector3.Distance(agent.transform.position, agent.player.transform.position) > agent.config.followStateDistance)
            {
                agent.stateMachine.ChangeState(AiStateID.FollowPlayer);
            }

            agent.enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in agent.enemies)
            {
                if (enemy.GetComponent<EnemyHealth>() != null){
                    if (Vector3.Distance(enemy.transform.position, agent.transform.position) < agent.config.attackEnemyStateDistance && enemy.GetComponent<EnemyHealth>().health > 0)
                    {
                        agent.stateMachine.ChangeState(AiStateID.TargetEnemy);
                    }
                }
                else if (enemy.GetComponent<WitchHealth>() != null){
                    if (Vector3.Distance(enemy.transform.position, agent.transform.position) < agent.config.attackEnemyStateDistance && enemy.GetComponent<WitchHealth>().health > 0)
                    {
                        agent.stateMachine.ChangeState(AiStateID.TargetEnemy);
                    }
                }
            }

            agent.timer = agent.config.Timer;
        }
    }

    public void Exit(AiAgent agent)
    {

    }
}
