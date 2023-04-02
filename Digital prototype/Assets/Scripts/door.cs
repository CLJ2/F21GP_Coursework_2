using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public bool locked;
    // Start is called before the first frame update
    void Start()
    {
        locked = true;
    }
    
    public void open()
    {
        locked = false;
    }
}
