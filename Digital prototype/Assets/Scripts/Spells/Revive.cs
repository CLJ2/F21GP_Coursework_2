using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Revive : Spell
{
    private int animIDInteract;
    private float radius = 2;

    void Start()
    {
        AssignAnimationIDs();
        animator = GetComponent<Animator>();
    }

    public override void beginSpell()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, radius, 1<<9);
        if (colliders != null)
        {
            //Debug.Log(colliders.Length);
            foreach(Collider collider in colliders)
            {
                //Debug.Log(collider.gameObject);
                StartCoroutine(ReviveAbility(collider.gameObject));
            }
        }
        endSpell();
    }

    IEnumerator ReviveAbility(GameObject ally)
    {
        if (ally.GetComponent<WizardHealth>().health <= 0 && !ally.Equals(gameObject))
        {
            //Debug.Log(ally.name);
            yield return new WaitForSeconds(1);
            ally.GetComponent<WizardHealth>().health = ally.GetComponent<AiAgent>().config.maxHealth;
        }
    }

    protected override void AssignAnimationIDs()
    {
        animIDInteract = Animator.StringToHash("Interact");
    }

    public void endSpell()
    {
        gameObject.SendMessageUpwards("abilityFinished");
    }
}
