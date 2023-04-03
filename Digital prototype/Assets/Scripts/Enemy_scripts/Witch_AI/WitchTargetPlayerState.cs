using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchTargetPlayer : WitchAiState
{

    [Tooltip("A small delay to allow for casting animation")]
    [SerializeField]
    private float fireballDelay = 0.5f;
    [Tooltip("How far in front of the wizard should the fireball be conjured")]
    [SerializeField]
    private float fireballForwardPosition = 1.9f;
    [Tooltip("How high up(from the wizards feet) should the fireball be conjured")]
    [SerializeField]
    private float fireballHeight = 1.0f;


    public WitchAiStateID GetID()
    { 
        return WitchAiStateID.TargetPlayer;
    }
    
    public void Enter(WitchAiAgent agent)
    {
        //this acts similar to the start function
        agent.animator.CrossFade("idle_combat", 0.5f);
    }

    public void Update(WitchAiAgent agent)
    {
        if (!agent.enabled) return; //if agent is not enabled, wait frame

        agent.timer -= Time.deltaTime;  //timer is used to make sure the agent only ever calculates a new destination every second instead of every frame
        agent.spellTimer -= Time.deltaTime; 
        if(agent.timer < 0) //if the timer has reached 0
        {
            
            float srtDistance = (agent.playerTransform.position - agent.navMeshAgent.destination).sqrMagnitude; //calc distance to the player
            if ((srtDistance < agent.config.minDistance*agent.config.minDistance || srtDistance > agent.config.maxDistance*agent.config.maxDistance) && agent.dead == false)    //if the distance is greater than the maxdistance^2
            {
                agent.navMeshAgent.isStopped = false;
                
                float angle= Mathf.Atan2(agent.playerTransform.position.z - agent.transform.position.z , agent.playerTransform.position.x - agent.transform.position.x);
                Vector3 destination = new Vector3(Mathf.Cos(angle)*agent.config.wantedDistance, agent.transform.position.y, Mathf.Sin(angle)*agent.config.wantedDistance);
                agent.navMeshAgent.destination = agent.playerTransform.position - destination;    //move to the player
            }
            agent.timer = agent.config.maxTime; //reset timer
        }

        if (Vector3.Distance(agent.playerTransform.position, agent.transform.position) < agent.config.attackRange && agent.spellTimer < 0 && !agent.navMeshAgent.hasPath)  //if close enough to the player & timer is complete, attack player
        {
            
            if (Random.Range(0, 4) < 2) agent.StartCoroutine(healSpell(agent));
            else agent.StartCoroutine(attackSpell(agent));
            agent.spellTimer = agent.config.spellCooldown;
        }
    }

    IEnumerator healSpell(WitchAiAgent agent){
        yield return new WaitForSeconds(1);
        if (agent.dead == false && agent.isFrozen == false){
            agent.transform.LookAt(agent.playerTransform.position);
            agent.animator.CrossFade("attack_short_001",0.5f);
            agent.animator.CrossFadeQueued("idle_combat", 0.5f);
            GameObject current = GameObject.Instantiate(agent.healing_spell);
            current.transform.position = new Vector3(agent.transform.position.x,agent.transform.position.y + 2,agent.transform.position.z);
            current.transform.LookAt(agent.playerTransform.position);
        } 
    }

    IEnumerator attackSpell(WitchAiAgent agent){
        yield return new WaitForSeconds(1);
        if (agent.dead == false && agent.isFrozen == false){
            agent.transform.LookAt(agent.playerTransform.position);
            agent.animator.CrossFade("attack_short_001",0.5f);
            agent.animator.CrossFadeQueued("idle_combat", 0.5f);
            float yRot = agent.transform.rotation.eulerAngles.y;
            Vector3 positionAdjustment = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
            GameObject current = GameObject.Instantiate(agent.attack_spell);
            current.GetComponentInChildren<fireballCollision>().fromEnemy = true;
            current.transform.position = agent.transform.position + (positionAdjustment * fireballForwardPosition);
            positionAdjustment =  new Vector3(current.transform.position.x, current.transform.position.y + fireballHeight, current.transform.position.z);
            current.transform.position = positionAdjustment;
            current.transform.LookAt(new Vector3(agent.playerTransform.position.x, agent.playerTransform.position.y + 1, agent.playerTransform.position.z));
        }
    }

    public void Exit(WitchAiAgent agent)
    {

    }  
}
