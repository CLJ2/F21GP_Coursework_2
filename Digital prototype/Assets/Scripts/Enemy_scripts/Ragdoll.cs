using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enemy now has colliders and can now ragdoll on death.
public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidBodies;    //stores all rigidbody components on agent
    Animator animator;  //stores animator object

    // Start is called before the first frame update
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        DeactivateRagdoll();    //upon start, deactivate ragdoll. this keeps the agent from falling over due to gravity
    }

    //method turns ragdoll off, agent stands up
    public void DeactivateRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }
        animator.enabled = true;
    }

    //method turns ragdoll off, agent falls over
    public void ActivateRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
        }
        animator.enabled = false;
    }


    //method takes the direction of a force provided and uses that to generate a ragdoll for the agent upon death
    public void ApplyForce(Vector3 direction)
    {
        var rigidBody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidBody.AddForce(direction);
    }
}
