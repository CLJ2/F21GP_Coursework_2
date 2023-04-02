using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchAiAgent : MonoBehaviour
{
    public WitchAiStateMachine stateMachine;
    public WitchAiStateID initialState;
    public NavMeshAgent navMeshAgent;
    public Transform playerTransform;
    public WitchAiConfig config;
    public float timer = 0.0f;
    public float spellTimer = 0.0f;
    public Ragdoll ragdoll;
    public UIHealthBar healthBar;
    public GameObject player;
    public GameObject[] barricades;
    public GameObject barricade;
    public Animation animator;
    public GameObject healing_spell;
    public GameObject attack_spell;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.gameObject.transform;
        barricades = GameObject.FindGameObjectsWithTag("Barricades");
        animator = GetComponent<Animation>();
        
        stateMachine = new WitchAiStateMachine(this);
        stateMachine.RegisterState(new WitchTargetPlayer());
        stateMachine.RegisterState(new WitchDeathState());
        stateMachine.RegisterState(new WitchIdleState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
