using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public EnemyHealth health;

    public void OnHit(RaycastHit hit, Vector3 direction)    //the raycast hit should be replaced with a spell/
    {
        //health.TakeDamage(hit.damage);    uncomment line once spell class is added
    }
}
