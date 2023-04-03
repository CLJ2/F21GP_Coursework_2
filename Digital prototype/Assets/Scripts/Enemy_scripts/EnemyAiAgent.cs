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
    public List<GameObject> players = new List<GameObject>();
    public GameObject[] playersArray;
    public GameObject player;
    public GameObject[] barricades;
    public GameObject barricade;
    public Animator animator;
    public bool isFrozen = false;
    public bool isKnocked = false;
    public float frozenTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        //healthBar = GetComponentInChildren<UIHealthBar>();

        GameObject playerTemp = GameObject.FindGameObjectWithTag("Player");
        GameObject[] playerAiTemp = GameObject.FindGameObjectsWithTag("AiPlayer");
        players.Add(playerTemp);
        foreach (GameObject i in playerAiTemp)
        {
            players.Add(i);
        }
        playersArray = players.ToArray();

        //playerTransform = player.gameObject.transform;
        barricades = GameObject.FindGameObjectsWithTag("Barricades");
        animator = GetComponent<Animator>();
        
        stateMachine = new EnemyAiStateMachine(this);
        stateMachine.RegisterState(new EnemyTargetPlayer());
        stateMachine.RegisterState(new EnemyDeathState());
        stateMachine.RegisterState(new EnemyIdleState());
        stateMachine.RegisterState(new EnemyHideState());
        stateMachine.ChangeState(initialState);
        
        
        //Debug.Log(healthBar.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        if (isFrozen) {
            navMeshAgent.isStopped = true;
            frozenTimer -= Time.deltaTime;
            if(frozenTimer < 0) {
                if(isKnocked) {
                    animator.ResetTrigger("Fall");
                    animator.SetTrigger("Recover");
                    isKnocked = false;
                }
                navMeshAgent.isStopped = false;
                animator.speed = 1;
            }  
        }
    }
}
