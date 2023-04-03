using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healing_star_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - new Vector3(0,0.02f,0);
        //transform.position = transform.position + transform.forward * 0.03f;
    }
}
