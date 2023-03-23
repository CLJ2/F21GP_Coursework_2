using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiAgent : MonoBehaviour
{
    public EnemyAiStateMachine stateMachine;
    public EnemyAiStateID initialState;
    public NavMeshAgent navMeshAgent;
    public Transform playerTransform;
    public EnemyAiConfig config;
    public float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        stateMachine = new EnemyAiStateMachine(this);
        stateMachine.RegisterState(new EnemyTargetPlayer());
        stateMachine.RegisterState(new EnemyDeathState());
        stateMachine.RegisterState(new EnemyIdleState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
