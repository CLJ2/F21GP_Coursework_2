using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing_circle : MonoBehaviour
{
    // Start is called before the first frame update
    int healAmount =-2; 
    void Start()
    {
        Destroy(gameObject,2.5f);
         Collider[] hitColliders = Physics.OverlapSphere(transform.position, 7.5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == 10){
                Debug.Log("healy skelebob hahaha");
                var hitBox = hitCollider.gameObject.GetComponent<EnemyHitbox>();
                if (hitCollider.gameObject.GetComponentInParent<EnemyHealth>().health + healAmount >= hitCollider.gameObject.GetComponentInParent<EnemyAiAgent>().config.maxhealth){
                    Debug.Log("too much health");
                }
                else hitBox.OnHit(healAmount, Vector3.one);
            }
        }
    }
    void Update()
    {
       
    }
}
