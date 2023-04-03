using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health;   //Stpres current agent health
    EnemyAiAgent agent; //Stores agent Ai object
    UIHealthBar healthBar;  //Stores agent healthbar

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<EnemyAiAgent>();   //Get the agent Ai component
        health = agent.config.maxhealth;    //set current health to maxhealth of the agent upon spawning
        healthBar = GetComponentInChildren<UIHealthBar>();  //get healthbar component

        var rigidBodies = GetComponentsInChildren<Rigidbody>(); //get all rigidbody components
        foreach (var rigidBody in rigidBodies)
        {
            EnemyHitbox hitbox = rigidBody.gameObject.AddComponent<EnemyHitbox>();  //add a hit box to every rigidbody component on agent
            hitbox.health = this;   //provide the hit box with EnemyHealth object
        }
        healthBar.gameObject.SetActive(false);
    }

    //calculate health when being damaged
    public void TakeDamage(float damage, Vector3 direction)
    {
        if(healthBar.gameObject.activeSelf == false) healthBar.gameObject.SetActive(true);
        health -= damage;
        healthBar.SetHealthBarPecentage(health/agent.config.maxhealth);
        if (health < 0)
        {
            Die(direction);
        }
    }

    //change agent to death state
    private void Die(Vector3 direction)
    {
        healthBar.gameObject.SetActive(false);
        EnemyDeathState state = agent.stateMachine.GetState(EnemyAiStateID.Dead) as EnemyDeathState;
        state.deathDirection = direction;
        agent.stateMachine.ChangeState(EnemyAiStateID.Dead);
    }
}
