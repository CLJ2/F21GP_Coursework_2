using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class skeleton_ai : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    private GameObject[] barricades;
    private Animation animations;
    private string state;
    private int stateDecider;
    private RaycastHit hit;

    //Call coroutines here that are to be running continueously
    void Awake()
    {
        animations.CrossFade("Idle");
        barricades = GameObject.FindGameObjectsWithTag("Barricades");
        StartCoroutine(Behaviour());
    }

    void Start()
    {
        state = "Idle";
    }

    private IEnumerator Behaviour()
    {
        if (state == "Idle") checkForPlayer();
        if (state == "Player Seen" || state == "Hiding")
        {
            stateDecider = Random.Range(0, 11);
            if (stateDecider < 5) targetPlayer();
            else hide();
        }
        yield return new wait;
    }

    private void targetPlayer()
    {
        state = "Targetting Player";

    }

    private void hide()
    {
        state = "Hiding";
    }

    private void roam()
    {

    }

    private void checkForPlayer()
    {
        Physics.Raycast(transform.position, transform.forward, out hit);
        if (hit.rigidbody.gameObject.tag == "Player")
        {
            state = "Player Seen";
        }
    }
}
