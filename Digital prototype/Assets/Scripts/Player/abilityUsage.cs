using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballSpell : MonoBehaviour
{
    [Header("Fireball")]
    [Tooltip("Fireball to launch")]
    [SerializeField]
    private GameObject fireball;
    [Tooltip("Ability cooldown in seconds")]
    [SerializeField]
    private float fireballCooldown = 1.0f;
    [Tooltip("A small delay to allow for casting animation")]
    [SerializeField]
    private float fireballDelay = 0.3f;
    [Tooltip("How far in front of the wizard should the fireball be conjured")]
    [SerializeField]
    private float fireballForwardPosition = 1.9f;
    [Tooltip("How high up(from the wizards feet) should the fireball be conjured")]
    [SerializeField]
    private float fireballHeight = 1.0f;

    private PlayerMovementInputs input;
    private float abilityTimeoutDelta;

    // animation IDs
    private int animIDAbility;
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
        abilityTimeoutDelta = fireballCooldown;

        AssignAnimationIDs();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input.GetUseAbility() == true)
        {
            if (abilityTimeoutDelta <= 0.0f)
            {
                animator.SetBool(animIDAbility, true);
                abilityTimeoutDelta = fireballCooldown;
                StartCoroutine(throwFireball());
            }
            else
            {
                input.SetUseAbility(false);
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
        animIDAbility = Animator.StringToHash("UseAbility");
    }

    IEnumerator throwFireball()
    {
        yield return new WaitForSeconds(fireballDelay);
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 positionAdjustment = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        GameObject current = GameObject.Instantiate(fireball);
        current.transform.position = transform.position + (positionAdjustment * fireballForwardPosition);
        positionAdjustment =  new Vector3(current.transform.position.x, current.transform.position.y + fireballHeight, current.transform.position.z);
        current.transform.position = positionAdjustment;
        current.transform.rotation = mainCamera.transform.rotation;
    }
}
