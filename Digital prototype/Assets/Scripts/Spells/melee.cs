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

    }
}
