using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : Spell
{
    [Tooltip("A collider to enable for melee attacks. It should only be enabled when needed as if its enabled all the time it causes problems")]
    [SerializeField]
    private CapsuleCollider difficultCollider;
    // animation IDs
    private int animIDMelee;

    private int enemyLayer = 10;
    private int witchLayer = 11;
    private bool attacking = false;
    private bool colliderIssues = false;

    void Start()
    {
        AssignAnimationIDs();
        animator = GetComponentInParent<Animator>();
        if (Object.ReferenceEquals(difficultCollider, null))
        {
            colliderIssues = true;
        }
        
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
        if (colliderIssues)
        {
            difficultCollider.enabled = true;
        }
        //The durations here are based off the animation length so arent really customisable
        animator.SetBool(animIDMelee, true);
        attacking = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetBool(animIDMelee, false);
        yield return new WaitForSeconds(1.0f);
        attacking = false;
        if (colliderIssues)
        {
            difficultCollider.enabled = false;
        }
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
