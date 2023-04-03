using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballCollision : MonoBehaviour
{

    private int playerLayer = 9;
    private int enemyLayer = 10;
    private int witchLayer = 11;
    private int fireBallDamage = 5;
    public bool fromEnemy = false;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            var hitBox = collision.gameObject.GetComponent<EnemyHitbox>();
            hitBox.OnHit(fireBallDamage, Vector3.one);
        }
        if (collision.gameObject.layer == witchLayer)
        {
            var hitBox = collision.gameObject.GetComponent<WitchHitbox>();
            hitBox.OnHit(fireBallDamage, Vector3.one);
        }
        if (collision.gameObject.layer ==  playerLayer && fromEnemy == true)
        {
            collision.gameObject.GetComponent<WizardHealth>().TakeDamage(fireBallDamage);
        }
    }
}
