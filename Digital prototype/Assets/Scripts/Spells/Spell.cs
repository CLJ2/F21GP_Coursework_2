using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [Header("General Spell things")]
    [Tooltip("Projectile or effect to use in the spell")]
    [SerializeField]
    protected GameObject projectile;

    protected Animator animator;

    public abstract void beginSpell();                  //Begins the spell effects
    protected abstract void AssignAnimationIDs();       //Sets all animation parameters to ID's for faster comparison

    void Start()
    {
        AssignAnimationIDs();
        animator = GetComponent<Animator>();
    }

    public void endSpell()
    {
        gameObject.SendMessage("abilityFinished");
    }

    // Rotates the player to face the camera direction smoothly
    public IEnumerator RotateOverTime() {
        float timer = 0.0f;
        Transform rootTransform = transform.root;
        Vector3 fwd = new Vector3(rootTransform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, rootTransform.localEulerAngles.z);
        while (timer < 0.5f) {
            timer += Time.deltaTime;
            float t = timer / 0.5f;
            t = t * t * t *(t *(6f *t -15f) + 10f);
            rootTransform.rotation = Quaternion.Slerp(rootTransform.rotation, Quaternion.Euler(fwd), t);
            yield return null;
        }
        yield return null;
    }
}
