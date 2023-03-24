using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxhealth;
    public float health;
    EnemyAiAgent agent;
    UIHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<EnemyAiAgent>();
        health = maxhealth;
        healthBar = GetComponentInChildren<UIHealthBar>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            EnemyHitbox hitbox = rigidBody.gameObject.AddComponent<EnemyHitbox>();
            hitbox.health = this;
        }
    }

    public void TakeDamage(float damage, Vector3 direction)
    {
        health -= damage;
        healthBar.SetHealthBarPecentage(health/maxhealth);
        if (health < 0)
        {
            Die(direction);
        }
    }

    private void Die(Vector3 direction)
    {
        EnemyDeathState state = agent.stateMachine.GetState(EnemyAiStateID.Dead) as EnemyDeathState;
        state.deathDirection = direction;
        agent.stateMachine.ChangeState(EnemyAiStateID.Dead);
    }
}
