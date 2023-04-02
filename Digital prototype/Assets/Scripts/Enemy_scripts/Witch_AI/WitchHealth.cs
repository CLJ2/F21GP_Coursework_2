using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchHealth : MonoBehaviour
{
    private float health;   //Stpres current agent health
    WitchAiAgent agent; //Stores agent Ai object
    WitchUIHealthBar healthBar;  //Stores agent healthbar

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<WitchAiAgent>();   //Get the agent Ai component
        health = agent.config.maxhealth;    //set current health to maxhealth of the agent upon spawning
        healthBar = GetComponentInChildren<WitchUIHealthBar>();  //get healthbar component

        var rigidBodies = GetComponentsInChildren<Rigidbody>(); //get all rigidbody components
        foreach (var rigidBody in rigidBodies)
        {
            WitchHitbox hitbox = rigidBody.gameObject.AddComponent<WitchHitbox>();  //add a hit box to every rigidbody component on agent
            hitbox.health = this;   //provide the hit box with WitchHealth object
        }
        healthBar.gameObject.SetActive(false);
    }

    //calculate health when being damaged
    public void TakeDamage(float damage, Vector3 direction)
    {
        if(healthBar.gameObject.active == false) healthBar.gameObject.SetActive(true);
        health -= damage;
        healthBar.SetHealthBarPecentage(health /agent.config.maxhealth);
        if (health < 0)
        {
            Die(direction);
        }
    }

    //change agent to death state
    private void Die(Vector3 direction)
    {
        WitchDeathState state = agent.stateMachine.GetState(WitchAiStateID.Dead) as WitchDeathState;
        state.deathDirection = direction;
        agent.stateMachine.ChangeState(WitchAiStateID.Dead);
    }
}
