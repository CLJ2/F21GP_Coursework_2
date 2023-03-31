using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDamage : MonoBehaviour
{
    [Tooltip("Layers for lightning to hit")]
    [SerializeField]
    private LayerMask collisionLayer;
    [Tooltip("Radius of lightning beam")]
    [SerializeField]
    private float radius;
    [Tooltip("Damage of lightning")]
    [SerializeField]
    private float damage;

    private GameObject boltStart;
    private GameObject boltEnd;

    // Start is called before the first frame update
    void Start()
    {
        boltStart = transform.GetChild(0).gameObject; //Gets first child, so dont change order of projectile prefab
        boltEnd = transform.GetChild(1).gameObject; //Gets second child, so dont change order of projectile prefab
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = boltStart.transform.position - boltEnd.transform.position;
        RaycastHit[] enemies = Physics.SphereCastAll(boltStart.transform.position, radius, temp.normalized, temp.magnitude, collisionLayer);
        foreach (RaycastHit enemy in enemies)
        {
            var hitBox = enemy.transform.gameObject.GetComponent<EnemyHitbox>();
            hitBox.OnHit(damage, temp.normalized);
        }
       
    }
}
