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
    public GameObject player;
    public GameObject[] barricades;
    public GameObject barricade;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.gameObject.transform;
        barricades = GameObject.FindGameObjectsWithTag("Barricades");
        
        stateMachine = new EnemyAiStateMachine(this);
        stateMachine.RegisterState(new EnemyTargetPlayer());
        stateMachine.RegisterState(new EnemyDeathState());
        stateMachine.RegisterState(new EnemyIdleState());
        stateMachine.RegisterState(new EnemyHideState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
