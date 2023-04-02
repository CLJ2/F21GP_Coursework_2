
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class lightning : Spell
{
    [Header("LightningBolt")]
    [Tooltip("A delay to allow for casting animation. This should be tweaked alongiside animation speed in animator")]
    [SerializeField]
    private float lightningBoltDelay = 0.3f;
    [Tooltip("The length of the ability")]
    [SerializeField]
    private float boltDuration;
    [Tooltip("The origin point of the magic(Crystal in staff)")]
    [SerializeField]
    private Transform origin;

    private Transform destination;

    // animation IDs
    private int animIDAbility;

    // Start is called before the first frame update
    void Start()
    {
        AssignAnimationIDs();
        animator = GetComponent<Animator>();

        //input sick custom audio
    }

    //Sets all animation parameters to ID's for faster comparison
    protected override void AssignAnimationIDs()
    {
        animIDAbility = Animator.StringToHash("Charge");
    }

    // shoots lightning bolt
    IEnumerator throwLightningBolt()
    {
        gameObject.SendMessage("disableMove");
        animator.SetBool(animIDAbility, true);
        yield return new WaitForSeconds(lightningBoltDelay);
        animator.SetBool(animIDAbility, false);
        gameObject.SendMessage("enableMove");
        GameObject bolt = GameObject.Instantiate(projectile);
        yield return new WaitForSeconds(boltDuration);
        Destroy(bolt);
        endSpell();
    }

    public override void beginSpell()
    {
        StartCoroutine(throwLightningBolt());
    }
}
