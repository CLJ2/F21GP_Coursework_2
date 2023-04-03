using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceExplosionSpell : Spell
{
    [Header("Ice Explosion")]
    [Tooltip("A small delay to allow for casting animation")]
    [SerializeField]
    private float iceExplosionDelay = 0.3f;
    [Tooltip("How big is the explosion")]
    [SerializeField]
    private float iceExplosionScale= 2.0f;

    // animation IDs
    private int animIDAbility;
    //Other components
    private GameObject mainCamera;

    //Awake is called when the script instance is first loaded
    private void Awake()
    {
        // get a reference to our main camera
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    //Sets all animation parameters to ID's for faster comparison
    protected override void AssignAnimationIDs()
    {
        animIDAbility = Animator.StringToHash("UseAbility");
    }

    IEnumerator castIceExplosion()
    {
        yield return new WaitForSeconds(iceExplosionDelay);
        animator.SetBool(animIDAbility, false);
        GameObject current = GameObject.Instantiate(projectile);
        current.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ParticleSystem[] ps = current.GetComponentsInChildren<ParticleSystem>();

        for (int i = 0; i < ps.Length; i++)
        {
            if (ps[i].gameObject.name == "Ground")
            {
                ParticleSystem groundParticles = ps[i];
                groundParticles.transform.localScale = groundParticles.transform.localScale*iceExplosionScale;
            }
            else if (ps[i].gameObject.name == "Ground_dark")
            {
                ParticleSystem groundDarkParticles = ps[i];
                groundDarkParticles.transform.localScale = groundDarkParticles.transform.localScale*iceExplosionScale;
            }
            else if (ps[i].gameObject.name == "Sphere")
            {
                ParticleSystem sphereParticles = ps[i];
                sphereParticles.transform.localScale = sphereParticles.transform.localScale*iceExplosionScale;
            }
            else if (ps[i].gameObject.name == "Impact")
            {
                ParticleSystem impactParticles = ps[i];
                impactParticles.transform.localScale = impactParticles.transform.localScale*iceExplosionScale;
            }
            else if (ps[i].gameObject.name == "Fire_up")
            {
                ParticleSystem fireUpParticles = ps[i];
                fireUpParticles.transform.localScale = fireUpParticles.transform.localScale*iceExplosionScale;
            }
            else if (ps[i].gameObject.name == "Spark")
            {
                ParticleSystem sparksParticles = ps[i];
                sparksParticles.transform.localScale = sparksParticles.transform.localScale*iceExplosionScale;
            }
        }
        endSpell();
    }

    public override void beginSpell()
    {
        StartCoroutine(RotateOverTime());
        animator.SetBool(animIDAbility, true);
        StartCoroutine(castIceExplosion());
    }
}
