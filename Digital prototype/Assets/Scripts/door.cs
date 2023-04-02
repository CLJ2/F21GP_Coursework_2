using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    [Tooltip("How fasst the door opens")]
    [SerializeField]
    private int degreesPerSecond;
    [Tooltip("Desired rotation")]
    [SerializeField]
    private int desiredRotation;

    private float amountRotated;

    public bool locked;
    private bool rotate;
    // Start is called before the first frame update
    void Start()
    {
        locked = true;
        rotate = false;
    }
    
    public void open()
    {
        locked = false;
        rotate = true;
    }

    void Update()
    {
        if (rotate)
        {
            transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
            amountRotated += degreesPerSecond * Time.deltaTime;
            if (amountRotated >= desiredRotation)
            {
                rotate = false;
            }
        }
    }
}
