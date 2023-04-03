using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyManagement : MonoBehaviour
{
    private int collectableLayer = 13;
    private int doorLayer = 14;
    private bool hasKey;

    // Start is called before the first frame update
    void Start()
    {
        hasKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleKey()
    {
        hasKey = !hasKey;
    }

    public bool getHasKey()
    {
        return hasKey;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == collectableLayer)
        {
            if(!hasKey)
            {
                toggleKey();
                Destroy(collision.gameObject);
                //Maybe play a sound in future
            }
        }
        else if (collision.gameObject.layer == doorLayer)
        {
            if (hasKey)
            {
                var door = collision.gameObject.transform.parent.gameObject.GetComponent<door>();
                if (door.locked)
                {
                    toggleKey();
                    collision.gameObject.SendMessageUpwards("open");
                }
            }
        }
    }
}
