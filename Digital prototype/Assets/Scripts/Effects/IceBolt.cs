using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBolt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * 0.03f;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            var hitBox = collision.gameObject.GetComponent<EnemyHitbox>();
            hitBox.OnHit(10, Vector3.one);
        }
        Destroy(gameObject);
    }
}
