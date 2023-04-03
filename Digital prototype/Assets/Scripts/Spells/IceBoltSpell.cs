using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBoltSpell : Spell
{
    [Header("IceBolt")]
    [Tooltip("A small delay to allow for casting animation")]
    [SerializeField]
    private float iceBoltDelay = 0.3f;
    [Tooltip("How far in front of the wizard should the iceBolt be conjured")]
    [SerializeField]
    private float iceBoltForwardPosition = 1.9f;
    [Tooltip("How high up(from the wizards feet) should the iceBolt be conjured")]
    [SerializeField]
    private float iceBoltHeight = 1.0f;

    // animation IDs
    private int animIDAbility;
    //Other components
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

    //Sets all animation parameters to ID's for faster comparison
    protected override void AssignAnimationIDs()
    {
        animIDAbility = Animator.StringToHash("UseAbility");
    }

    IEnumerator throwIceBolt()
    {
        yield return new WaitForSeconds(iceBoltDelay);
        animator.SetBool(animIDAbility, false);
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 positionAdjustment = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        GameObject current = GameObject.Instantiate(projectile);
        current.transform.position = transform.position + (positionAdjustment * iceBoltForwardPosition);
        positionAdjustment =  new Vector3(current.transform.position.x, current.transform.position.y + iceBoltHeight, current.transform.position.z);
        current.transform.position = positionAdjustment;
        current.transform.rotation = mainCamera.transform.rotation;
        endSpell();
    }

    public override void beginSpell()
    {
        StartCoroutine(RotateOverTime());
        animator.SetBool(animIDAbility, true);
        StartCoroutine(throwIceBolt());
    }
}
