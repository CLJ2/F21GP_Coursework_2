using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing_circle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,2.5f);
         Collider[] hitColliders = Physics.OverlapSphere(transform.position, 7.5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == 10){
                Debug.Log("healy you hahaha");
                //give health here
            }
        }
    }
    void Update()
    {
       
    }
}
