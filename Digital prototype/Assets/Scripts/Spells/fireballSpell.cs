using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballSpell : Spell
{
    [Header("Fireball")]
    [Tooltip("A small delay to allow for casting animation")]
    [SerializeField]
    private float fireballDelay = 0.3f;
    [Tooltip("How far in front of the wizard should the fireball be conjured")]
    [SerializeField]
    private float fireballForwardPosition = 1.9f;
    [Tooltip("How high up(from the wizards feet) should the fireball be conjured")]
    [SerializeField]
    private float fireballHeight = 1.0f;

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
        AssignAnimationIDs();
        animator = GetComponent<Animator>();
    }

    //Sets all animation parameters to ID's for faster comparison
    private void AssignAnimationIDs()
    {
        animIDAbility = Animator.StringToHash("UseAbility");
    }

    IEnumerator throwFireball()
    {
        yield return new WaitForSeconds(fireballDelay);
        animator.SetBool(animIDAbility, false);
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 positionAdjustment = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        GameObject current = GameObject.Instantiate(projectile);
        current.transform.position = transform.position + (positionAdjustment * fireballForwardPosition);
        positionAdjustment =  new Vector3(current.transform.position.x, current.transform.position.y + fireballHeight, current.transform.position.z);
        current.transform.position = positionAdjustment;
        current.transform.rotation = mainCamera.transform.rotation;
        endSpell();
    }

    public override void beginSpell()
    {
        animator.SetBool(animIDAbility, true);
        StartCoroutine(throwFireball());
    }
}
