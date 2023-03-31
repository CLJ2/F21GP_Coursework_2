using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class handles spell/weapons hitting the enemy agent
public class EnemyHitbox : MonoBehaviour
{
    public EnemyHealth health;  //stores EnemyHealth object

    public void OnHit(float damage, Vector3 direction)    //the raycast hit should be replaced with a spell/weapon object. direction is the direction of the attack
    {
        health.TakeDamage(damage, direction); 
    }
}
