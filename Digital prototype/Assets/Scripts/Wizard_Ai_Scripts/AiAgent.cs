using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateID initialState;
    public NavMeshAgent navMeshAgent;
    public bool active;
    public GameObject player;
    public AiConfig config;
    public float timer;
    public GameObject[] enemies;
    public CharacterController characterController;
    public float health;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new UnactiveState());
        stateMachine.RegisterState(new IdleState());
        stateMachine.RegisterState(new FollowPlayerState());
        stateMachine.RegisterState(new TargetEnemyState());
        stateMachine.RegisterState(new DownedState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }
}
