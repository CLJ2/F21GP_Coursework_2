using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//configuration for enemy agents
[CreateAssetMenu()]
public class WitchAiConfig : ScriptableObject
{
    public float maxSightDistance = 5.0f;
    public float attackRange = 1.0f;

    public float maxTime = 1.0f;
    public float spellCooldown = 2.5f;
    
    public float minDistance = 15f;//the needed distance away from the player the witch will start to move further away to the player
    public float maxDistance = 30f; //the needed distance away from the player the witch will start to move closer to the player
    public float wantedDistance = 15f; //the distance away from the player the witch will move to

    public float deathForce = 5.0f;

    public float maxhealth = 50.0f;

    public int attackPlayerOnSightChance = 4;

    public float freezeDuration = 5;
    public float knockdownDuration = 3;
}
