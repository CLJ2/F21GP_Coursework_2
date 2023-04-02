using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class iceExplosionEffects : MonoBehaviour
{
    private bool hasCollided = false;
    private NavMeshAgent nma;
    private Animator animator;
    private float waitTime = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        waitTime-=Time.deltaTime;
    }

    public void OnTriggerEnter(Collider collider)
    {
        EnemyAiAgent ai;
        Transform t = collider.transform.root;
        if (t.gameObject.layer == 10 && hasCollided == false)
        {
            hasCollided = true;
            /* var hitBox = collider.gameObject.GetComponent<EnemyHitbox>();
            hitBox.OnHit(10, Vector3.one); */
            nma = t.GetComponent<NavMeshAgent>();
            animator = t.GetComponent<Animator>();
            /* nma.isStopped = true;
            nma.velocity = Vector3.zero;
            animator.speed = 0; */
            //animator.speed = 0;
            Debug.Log("hit");
            //StartCoroutine(RestartAI());
            ai = t.GetComponent<EnemyAiAgent>();
            ai.navMeshAgent.isStopped = true;
            ai.navMeshAgent.ResetPath();
            
            ai.navMeshAgent.isStopped = false;
        }  
        Destroy(gameObject);
    }

    public IEnumerator RestartAI() {
        yield return new WaitForSeconds(5f);
        hasCollided = false;
        Debug.Log("hit2s");
        nma.isStopped = false;
        animator.speed = 1;
        //nma.ResetPath();
    }
}
