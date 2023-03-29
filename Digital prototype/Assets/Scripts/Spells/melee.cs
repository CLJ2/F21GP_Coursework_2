using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : Spell
{

    // animation IDs
    private int animIDMelee;

    protected override void AssignAnimationIDs()
    {
        animIDMelee = Animator.StringToHash("Melee");
    }

    public override void beginSpell()
    {
        StartCoroutine(meleeAttack());
    }

    IEnumerator meleeAttack()
    {
        //The durations here are based off the animation length so arent really customisable
        animator.SetBool(animIDMelee, true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool(animIDMelee, false);
        yield return new WaitForSeconds(1.0f);
        endSpell();
    }
}
