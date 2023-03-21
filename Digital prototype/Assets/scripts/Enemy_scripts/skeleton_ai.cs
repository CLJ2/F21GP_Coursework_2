using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class skeleton_ai : MonoBehaviour
{
    [Serialize]
    private UnityEngine.AI.NavMeshAgent agent;

    //Call coroutines here that are to be running continueously
    void Awake()
    {
        StartCoroutine(Behaviour());
    }


    
    private IEnumerator Behaviour()
    {

    }

}
