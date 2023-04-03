using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windSpell : Spell
{
    [Header("Wind")]
    [Tooltip("A small delay to allow for casting animation")]
    [SerializeField]
    private float windDelay = 0.3f;
    [Tooltip("How far in front of the wizard should the wind affect")]
    [SerializeField]
    private float windForwardLength = 15f; 
    [Tooltip("What layers does the wind affect")]
    [SerializeField]
    private LayerMask EnemyLayer;
    [Tooltip("How strong is the wind")]
    [SerializeField]
    private float windForce = 400f;
    [Tooltip("How long does the wind knockdown for")]
    [SerializeField]
    private float windKnockdownTime = 5f;

    // animation IDs
    private int animIDAbility;
    //Other components
    private GameObject mainCamera;
    private EnemyAiAgent enemyAI;
    private WitchAiAgent WitchAI;
    private Vector3 original_rot;
    private Vector3 original_pos;

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
        //Get Enemy layer
        EnemyLayer = LayerMask.GetMask("Enemy", "witch_doctor");
    }

    //Sets all animation parameters to ID's for faster comparison
    protected override void AssignAnimationIDs()
    {
        animIDAbility = Animator.StringToHash("UseAbility");
    }

    IEnumerator castWind()
    {
        yield return new WaitForSeconds(windDelay);

        animator.SetBool(animIDAbility, false);

        Vector2 screenCenter = new Vector2(Screen.width/2f, Screen.height/2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hitData;

        if(Physics.Raycast(ray, out hitData, windForwardLength, EnemyLayer)) {
            if (hitData.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                enemyAI = hitData.transform.root.gameObject.GetComponent<EnemyAiAgent>();
                /* enemyAI.ragdoll.ActivateRagdoll();
                enemyAI.ragdoll.ApplyForce(Vector3.back * windForce); */
                animator.ResetTrigger("Recover");
                enemyAI.animator.SetTrigger("Fall");
                //wait a couple seconds
                //isfrozen
                //deactivate ragdoll
                
                enemyAI.isFrozen = true;
                enemyAI.frozenTimer = enemyAI.config.knockdownDuration;
                enemyAI.isKnocked = true;

            }
            if (hitData.collider.gameObject.layer == LayerMask.NameToLayer("witch_doctor")) {
                WitchAI = hitData.transform.root.gameObject.GetComponent<WitchAiAgent>();

                Debug.Log("knocked back witch");
                WitchAI.animator.Stop();
                //WitchAI.gameObject.transform.root.GetChild(0).rotation = (Quaternion.Euler(-180,WitchAI.gameObject.transform.rotation.y , WitchAI.gameObject.transform.rotation.z));
                WitchAI.gameObject.transform.root.GetChild(0).rotation = Quaternion.Lerp(WitchAI.gameObject.transform.rotation,Quaternion.Euler(-180,WitchAI.gameObject.transform.rotation.y , WitchAI.gameObject.transform.rotation.z),0.5f);
                //wait a couple seconds
                //isfrozen
                //deactivate ragdoll
                
                WitchAI.isFrozen = true;
                WitchAI.frozenTimer = WitchAI.config.knockdownDuration;
                WitchAI.isKnocked = true;

            }
        }
        endSpell();
    }

    public override void beginSpell()
    {
        StartCoroutine(RotateOverTime());
        animator.SetBool(animIDAbility, true);
        StartCoroutine(castWind());
    }

}
