using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : Spell
{
    // animation IDs
    private int animIDMelee;

    private int enemyLayer = 10;
    private int witchLayer = 11;
    private bool attacking = false;

    void Start()
    {
        AssignAnimationIDs();
        animator = GetComponentInParent<Animator>();
    }

    protected override void AssignAnimationIDs()
    {
        animIDMelee = Animator.StringToHash("Melee");
    }

    public override void beginSpell()
    {
        StartCoroutine(RotateOverTime());
        StartCoroutine(meleeAttack());
    }

    IEnumerator meleeAttack()
    {
        
        //The durations here are based off the animation length so arent really customisable
        animator.SetBool(animIDMelee, true);
        attacking = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetBool(animIDMelee, false);
        yield return new WaitForSeconds(1.0f);
        attacking = false;
        endSpell();
    }

    public new void endSpell()
    {
        gameObject.SendMessageUpwards("abilityFinished");
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (attacking && (collision.gameObject.layer == enemyLayer))
        {
            var hitBox = collision.gameObject.GetComponent<EnemyHitbox>();
            hitBox.OnHit(1, Vector3.one);
        }
        if (attacking && (collision.gameObject.layer == witchLayer))
        {
            var hitBox = collision.gameObject.GetComponent<WitchHitbox>();
            hitBox.OnHit(1, Vector3.one);
        }
    }
}
