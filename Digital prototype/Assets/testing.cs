using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{

    public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown("k"))
        {
            Debug.Log("K");
            anim.CrossFade("attack_short_001",0.5f);
            anim.CrossFadeQueued("idle_combat", 0.5f);
        }
    }
}
