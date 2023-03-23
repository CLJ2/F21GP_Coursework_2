using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flamethrowerSpell : MonoBehaviour
{
    [Header("Flamethrower")]
    [Tooltip("Fire to shoot")]
    [SerializeField]
    private GameObject flamethrower;
    [Tooltip("Ability cooldown in seconds")]
    [SerializeField]
    private float flamethrowerCooldown;
    [Tooltip("A small delay to allow for casting animation")]
    [SerializeField]
    private float flamethrowerDelay;
    [Tooltip("The length of the ability cast")]
    [SerializeField]
    private float flamethrowerLength;
    [Tooltip("The origin point of the magic(Crystal in staff)")]
    [SerializeField]
    private Transform origin;

    private PlayerMovementInputs input;
    private float abilityTimeoutDelta;

    // animation IDs
    private int animIDAbility;
    private int animIDFinishedAbility;
    //Other components
    private Animator animator;
    private GameObject mainCamera;

    //Awake is called when the script instance is first loaded
    private void Awake()
    {
        // get a reference to our main camera
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get the inputs
        input = GetComponent<PlayerMovementInputs>();
        //Initialise snowball cooldown timer
        abilityTimeoutDelta = flamethrowerCooldown;

        AssignAnimationIDs();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input.GetUseSecondaryAbility() == true)
        {
            if (abilityTimeoutDelta <= 0.0f)
            {
                abilityTimeoutDelta = flamethrowerCooldown;
                StartCoroutine(startFlamethrower());
            }
            else
            {
                input.SetUseSecondaryAbility(false);
            }
        }
        else
        {
            animator.SetBool(animIDAbility, false);
        }

        if(abilityTimeoutDelta > 0.0f)
        {
            abilityTimeoutDelta -= Time.deltaTime;
        }

    }

    //Sets all animation parameters to ID's for faster comparison
    private void AssignAnimationIDs()
    {
        animIDAbility = Animator.StringToHash("UseSecondaryAbility");
        animIDFinishedAbility = Animator.StringToHash("FinishedSecondaryAbility");
    }

    IEnumerator startFlamethrower()
    {
        animator.SetBool(animIDAbility, true);
        animator.SetBool(animIDFinishedAbility, false);
        yield return new WaitForSeconds(flamethrowerDelay);
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 positionAdjustment = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        GameObject current = GameObject.Instantiate(flamethrower);
        current.transform.position = origin.position;
        current.transform.rotation = origin.rotation;
        yield return new WaitForSeconds(flamethrowerLength);
        animator.SetBool(animIDFinishedAbility, true);
    }
}
