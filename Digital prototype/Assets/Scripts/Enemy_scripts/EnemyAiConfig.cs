using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

//configuration for enemy agents
[CreateAssetMenu()]
public class EnemyAiConfig : ScriptableObject
{
    public float maxSightDistance = 5.0f;
    public float attackRange = 1.0f;

    public float maxTime = 1.0f;
    public float minDistance = 1.0f;
    public float hideDistance = 2.0f;
    public float hideRange = 10.0f;

    public float deathForce = 5.0f;

    public float maxhealth = 50.0f;

    public int unhideChance = 2;
    public int attackPlayerOnSightChance = 4;

    public float damage = 3.0f;
    public float freezeDuration = 5;
    public float knockdownDuration = 3;
}
