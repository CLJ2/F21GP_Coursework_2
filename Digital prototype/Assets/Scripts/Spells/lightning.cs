
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
    //Other components
    private GameObject mainCamera;
    private GameObject lightningEndObj;

    //Awake is called when the script instance is first loaded
    private void Awake()
    {
        // get a reference to our main camera
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        //get reference to lightning end object (this is to do with the way the asset functions)
        if (lightningEndObj == null) {
            lightningEndObj = GameObject.FindGameObjectWithTag("Lightning");
        }
    }

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
        GameObject boltStart = bolt.transform.GetChild(0).gameObject; //Gets first child, so dont change order of projectile prefab
        GameObject boltEnd = bolt.transform.GetChild(1).gameObject; //Gets second child, so dont change order of projectile prefab
        yield return new WaitForSeconds(boltDuration);
        Destroy(bolt);
        endSpell();
    }

    public override void beginSpell()
    {
        StartCoroutine(throwLightningBolt());
    }
}
