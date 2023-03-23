using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("Primary Ability")]
    [Tooltip("The primary spell of the wizard")]
    public Spell primaryAbility;
    [Tooltip("Primary ability cooldown")]
    [SerializeField]
    private float primaryCooldown;
    [Header("Secondary Ability")]
    [Tooltip("The secondary spell of the wizard")]
    public Spell secondaryAbility;
    [Tooltip("Secondary ability cooldown")]
    [SerializeField]
    private float secondaryCooldown;

    //Whether an ability is being used
    private bool abilityActive;
    private float primaryTimeoutDelta;
    private float secondaryTimeoutDelta;

    //The player input
    private PlayerMovementInputs input;

    // Start is called before the first frame update
    void Start()
    {
        abilityActive = false;
        primaryTimeoutDelta = 0.0f;
        secondaryTimeoutDelta = 0.0f;
        //Get the inputs
        input = GetComponent<PlayerMovementInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.GetUseAbility() == true)
        {
            if(abilityActive == false && primaryTimeoutDelta <= 0.0f)
            {
                abilityActive = true;
                primaryAbility.beginSpell();
                primaryTimeoutDelta = primaryCooldown;
            }
            input.SetUseAbility(false);
            
        }
        else if (input.GetUseSecondaryAbility() == true)
        {
            if (abilityActive == false && secondaryTimeoutDelta <= 0.0f)
            {
                abilityActive = true;
                secondaryAbility.beginSpell();
                secondaryTimeoutDelta = secondaryCooldown;
            }
            input.SetUseSecondaryAbility(false);
        }

        if (primaryTimeoutDelta > 0.0f)
        {
            primaryTimeoutDelta -= Time.deltaTime;
        }
        if (secondaryTimeoutDelta > 0.0f)
        {
            secondaryTimeoutDelta -= Time.deltaTime;
        }
    }

    public void abilityFinished()
    {
        abilityActive = false;
    }
}
