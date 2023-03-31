using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [Header("General Spell things")]
    [Tooltip("Projectile or effect to use in the spell")]
    [SerializeField]
    protected GameObject projectile;

    protected Animator animator;

    public abstract void beginSpell();                  //Begins the spell effects
    protected abstract void AssignAnimationIDs();       //Sets all animation parameters to ID's for faster comparison

    void Start()
    {
        AssignAnimationIDs();
        animator = GetComponent<Animator>();
    }

    public void endSpell()
    {
        gameObject.SendMessage("abilityFinished");
    }
}
