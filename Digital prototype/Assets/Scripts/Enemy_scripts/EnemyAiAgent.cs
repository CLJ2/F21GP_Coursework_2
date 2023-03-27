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
    public Ragdoll ragdoll;
    public UIHealthBar healthBar;
    public GameObject[] players;
    public GameObject[] barricades;
    public GameObject barricade;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        players = GameObject.FindGameObjectsWithTag("Player");
        barricades = GameObject.FindGameObjectsWithTag("Barricades");
        
        stateMachine = new EnemyAiStateMachine(this);
        stateMachine.RegisterState(new EnemyTargetPlayer());
        stateMachine.RegisterState(new EnemyDeathState());
        stateMachine.RegisterState(new EnemyIdleState());
        stateMachine.RegisterState(new EnemyHideState());
        stateMachine.ChangeState(initialState);

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }
}
