using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class iceExplosionEffects : MonoBehaviour
{
    [Tooltip("How much damage does the explosion do?")]
    [SerializeField] private float frostDamage = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator OnTriggerEnter(Collider collider)
    {
        //Debug.Log(collider.gameObject.name);
        EnemyAiAgent ai;
        Transform t = collider.transform.root;
        if (t.gameObject.layer == 10)
        {
            //Debug.Log("Frozen!");
            ai = t.GetComponent<EnemyAiAgent>();
            ai.isFrozen= true;
            ai.animator.speed = 0;
            ai.frozenTimer = ai.config.freezeDuration;
            t.GetComponent<EnemyHealth>().TakeDamage(frostDamage, -t.forward);
        } 
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
