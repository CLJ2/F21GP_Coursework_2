using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flamethrowerDamage : MonoBehaviour
{

    [Tooltip("Damage of each fire particle")]
    [SerializeField]
    private float damage;

    private int enemyLayer = 10;
    private int witchLayer = 11;

    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("Collision");
        if (other.gameObject.layer == enemyLayer)
        {
            var hitBox = other.gameObject.GetComponent<EnemyHitbox>();
            hitBox.OnHit(damage, Vector3.one);
        }
         if (other.gameObject.layer == witchLayer)
        {
            var hitBox = other.gameObject.GetComponent<WitchHitbox>();
            hitBox.OnHit(damage, Vector3.one);
        }
    }
}
