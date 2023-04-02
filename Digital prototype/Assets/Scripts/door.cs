using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    [Tooltip("How fasst the door opens")]
    [SerializeField]
    private int degreesPerSecond;
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

    void Update()
    {
        if (!locked)
        {
            transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
        }
    }
}
