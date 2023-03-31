
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class lightning : MonoBehaviour
{
    [Header("LightningBolt")]
    [Tooltip("LightningBolt to launch")]
    [SerializeField]
    private GameObject lightningBolt;
    [Tooltip("Ability cooldown in seconds")]
    [SerializeField]
    private float lightningBoltCooldown = 1.0f;
    [Tooltip("A small delay to allow for casting animation")]
    [SerializeField]
    private float lightningBoltDelay = 0.3f;
    [Tooltip("How far in front of the wizard should the lightningBolt be conjured")]
    [SerializeField]
    private float lightningBoltForwardPosition = 1.9f;
    [Tooltip("How high up(from the wizards feet) should the lightningBolt be conjured")]
    [SerializeField]
    private float lightningBoltHeight = 1.0f;

    private PlayerMovementInputs input;
    private float abilityTimeoutDelta;

    // animation IDs
    private int animIDAbility;
    //Other components
    private Animator animator;
    private GameObject mainCamera;
    private GameObject lightningEndObj;
    private AudioSource audio;

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
        //Get the inputs
        input = GetComponent<PlayerMovementInputs>();
        //Initialise snowball cooldown timer
        abilityTimeoutDelta = lightningBoltCooldown;

        AssignAnimationIDs();
        animator = GetComponent<Animator>();

        //input sick custom audio
        audio = GetComponent<AudioSource>();
        audio.volume = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if(input.GetUseAbility() == true)
        {
            
            if (abilityTimeoutDelta <= 0.0f)
            {
                StartCoroutine(RotateOverTime());
                animator.SetBool(animIDAbility, true);
                abilityTimeoutDelta = lightningBoltCooldown;
                StartCoroutine(throwLightningBolt());
                audio.Play(0);
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

    // shoots lightning bolt
    IEnumerator throwLightningBolt()
    {
        yield return new WaitForSeconds(lightningBoltDelay);

        
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 positionAdjustment = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        
        GameObject current = GameObject.Instantiate(lightningBolt);
        current.GetComponent<LightningBoltScript>().StartObject = transform.gameObject;
        
        current.GetComponent<LightningBoltScript>().StartPosition = new Vector3(current.transform.position.x + 0.3f, current.transform.position.y + lightningBoltHeight, current.transform.position.z + 0.3f);

        current.transform.position = transform.position + (positionAdjustment * lightningBoltForwardPosition);
        //current.GetComponent<LightningBoltScript>().StartPosition = new Vector3(current.transform.position.x, current.transform.position.y + lightningBoltHeight, current.transform.position.z);
        positionAdjustment = new Vector3(current.transform.position.x, current.transform.position.y + lightningBoltHeight, current.transform.position.z);
        current.transform.position = positionAdjustment;
        current.transform.rotation = mainCamera.transform.rotation;
        //current.GetComponent<LightningBoltScript>().EndPosition = new Vector3(current.transform.position.x, current.transform.position.y + lightningBoltHeight, current.transform.position.z);
        //current.GetComponent<LightningBoltScript>().EndPosition = new Vector3(lightningEndObj.transform.position.x, lightningEndObj.transform.position.y + lightningBoltHeight, lightningEndObj.transform.position.z);
        current.GetComponent<LightningBoltScript>().EndObject = lightningEndObj;
       
       // cleaning up cloned bolts
        Destroy(current, 0.3f);
    }

    // Rotates the player to face the camera direction smoothly
    IEnumerator RotateOverTime() {
        float timer = 0.0f;
        Vector3 fwd = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
        while (timer < 0.5f) {
            timer += Time.deltaTime;
            float t = timer / 0.5f;
            t = t * t * t *(t *(6f *t -15f) + 10f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(fwd), t);
            yield return null;
        }
        yield return null;
    }
}
