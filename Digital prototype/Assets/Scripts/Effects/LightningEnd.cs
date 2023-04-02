using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEnd : MonoBehaviour
{

    [Tooltip("Layers for lightning to ignore")]
    [SerializeField]
    private LayerMask avoidLayers;
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
        avoidLayers = ~avoidLayers;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Mathf.Infinity, avoidLayers))
        {
            transform.position = hit.point;
        }
        else
        {
            Debug.Log("ERROR");
        }
    }
}
