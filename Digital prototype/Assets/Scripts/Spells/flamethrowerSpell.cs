using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flamethrowerSpell : Spell
{
    [Header("Flamethrower")]
    [Tooltip("A small delay to allow for casting animation")]
    [SerializeField]
    private float flamethrowerDelay;
    [Tooltip("The length of the ability cast(This affects animation time not actual duration)")]
    [SerializeField]
    private float flamethrowerLength;
    [Tooltip("The origin point of the magic(Crystal in staff)")]
    [SerializeField]
    private Transform origin;

    // animation IDs
    private int animIDAbility;
    private int animIDFinishedAbility;
    //Other components
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        AssignAnimationIDs();
        animator = GetComponent<Animator>();
    }


    //Sets all animation parameters to ID's for faster comparison
    private void AssignAnimationIDs()
    {
        animIDAbility = Animator.StringToHash("UseSecondaryAbility");
        animIDFinishedAbility = Animator.StringToHash("FinishedSecondaryAbility");
    }

    //Throws flames in from the staff for roughly 1 second
    IEnumerator startFlamethrower()
    {
        animator.SetBool(animIDAbility, true);
        animator.SetBool(animIDFinishedAbility, false);
        yield return new WaitForSeconds(flamethrowerDelay);
        animator.SetBool(animIDAbility, false);
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 positionAdjustment = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        GameObject current = GameObject.Instantiate(projectile);
        current.transform.position = origin.position;
        current.transform.rotation = origin.rotation;
        yield return new WaitForSeconds(flamethrowerLength);
        animator.SetBool(animIDFinishedAbility, true);
        gameObject.SendMessage("enableMove");
        endSpell();
    }

    public override void beginSpell()
    {
        StartCoroutine(startFlamethrower());
        gameObject.SendMessage("disableMove");
    }
}
