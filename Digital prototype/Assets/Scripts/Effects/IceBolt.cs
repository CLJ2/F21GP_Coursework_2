using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBolt : MonoBehaviour
{
    public int iceBoltDamage;
    // Start is called before the first frame update
    void Start()
    {
        iceBoltDamage = 15;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * 0.5f;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            var hitBox = collision.gameObject.GetComponent<EnemyHitbox>();
            hitBox.OnHit(iceBoltDamage, Vector3.one);
        }
        if (collision.gameObject.layer == 11)
        {
            var hitBox = collision.gameObject.GetComponent<WitchHitbox>();
            hitBox.OnHit(iceBoltDamage, Vector3.one);
        }
        Destroy(gameObject);
    }
}
