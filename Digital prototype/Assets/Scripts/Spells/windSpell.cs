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
    private Ragdoll ragdoll;
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
                ragdoll = hitData.transform.root.gameObject.GetComponent<Ragdoll>();
                /* original_rot = ragdoll.transform.localEulerAngles;
                original_pos = ragdoll.transform.localPosition; */
                ragdoll.ActivateRagdoll();
                ragdoll.ApplyForce(Vector3.up * windForce);
                
                ragdoll.ApplyForce(-ragdoll.transform.forward * windForce);
                
                /* if (hitData.rigidbody != null)
                    hitData.rigidbody.AddForce(mainCamera.transform.forward * 400); */
                    
            }
        }
        endSpell();
         if (ragdoll != null) {
            yield return new WaitForSeconds(3);
            
            ragdoll.DeactivateRagdoll();
            /* ragdoll.transform.position = original_pos;
            ragdoll.transform.eulerAngles = original_rot; */

        } 
        
    }

    public override void beginSpell()
    {
        animator.SetBool(animIDAbility, true);
        StartCoroutine(castWind());
    }

}
