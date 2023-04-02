using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningOrigin : MonoBehaviour
{
    [Tooltip("Where the lightning should start from at all times")]
    public Transform origin;

    // Start is called before the first frame update
    void Awake()
    {
        origin = GameObject.Find("airMagicPoint").transform;
        transform.position = origin.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = origin.position;
    }
}
