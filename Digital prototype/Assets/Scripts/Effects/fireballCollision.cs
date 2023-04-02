using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballCollision : MonoBehaviour
{

    private int enemyLayer = 10;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            var hitBox = collision.gameObject.GetComponent<EnemyHitbox>();
            hitBox.OnHit(2, Vector3.one);
        }
    }
}
