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
    public ThirdPersonController thirdPersonController;
    public float health;
    public Animator animator;
    public Spell attack;

    private float animationMovementBlend;
    private int animIDSpeed;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        thirdPersonController = GetComponent<ThirdPersonController>();

        AssignAnimationIDs();

        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new UnactiveState());
        stateMachine.RegisterState(new IdleState());
        stateMachine.RegisterState(new FollowPlayerState());
        stateMachine.RegisterState(new TargetEnemyState());
        stateMachine.RegisterState(new DownedState());
        stateMachine.ChangeState(initialState);
    }

    private void AssignAnimationIDs()
    {
        animIDSpeed = Animator.StringToHash("Speed");
    }
        // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        animationMovementBlend = Mathf.Lerp(animationMovementBlend, navMeshAgent.velocity.magnitude, Time.deltaTime * navMeshAgent.acceleration);
        if (animationMovementBlend < 0.01f)
        {
            animationMovementBlend = 0f;
        }
        animator.SetFloat(animIDSpeed, animationMovementBlend);    //animator not working for ai
    }
}
