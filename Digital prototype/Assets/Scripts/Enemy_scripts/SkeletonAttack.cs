using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    private EnemyAiAgent agent;
    private float timer;
    private float timerReset = 0.75f;

    void Start()
    {
        agent = GetComponentInParent<EnemyAiAgent>();
        timer = timerReset;
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (timer < 0)
        {
            if (collider.gameObject.tag == "Player")
            {
                collider.GetComponent<WizardHealth>().TakeDamage(agent.config.damage);
            }
            if (collider.gameObject.tag == "AiPlayer")
            {
                collider.GetComponent<WizardHealth>().TakeDamage(agent.config.damage);
            }
            timer = timerReset;
        }
    }
}
