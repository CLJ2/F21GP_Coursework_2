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

    // animation IDs
    private int animIDAbility;
    //Other components
    private GameObject mainCamera;
    private EnemyAiAgent enemyAI;
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
        EnemyLayer = LayerMask.GetMask("Enemy");
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
                enemyAI.animator.SetTrigger("Fall");
                //wait a couple seconds
                //isfrozen
                //deactivate ragdoll
                yield return new WaitForSeconds(3);
                //enemyAI.ragdoll.DeactivateRagdoll();
                enemyAI.animator.SetTrigger("Recover");
            }
        }
        endSpell();
    }

    public override void beginSpell()
    {
        animator.SetBool(animIDAbility, true);
        StartCoroutine(castWind());
    }

}