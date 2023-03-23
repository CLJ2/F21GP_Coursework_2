using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [Header("General Spell things")]
    [Tooltip("Projectile or effect to use in the spell")]
    [SerializeField]
    protected GameObject projectile;

    public abstract void beginSpell();

    public void endSpell()
    {
        gameObject.SendMessage("abilityFinished");
    }
}
