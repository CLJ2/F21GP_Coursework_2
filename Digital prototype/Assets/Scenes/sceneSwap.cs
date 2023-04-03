using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSwap: MonoBehaviour
{
    public Transform fireWizard;
    public Transform waterWizard;
    public Transform airWizard;

    private Vector3 fireWizardSpawnS2=new Vector3(9.99818516f,16.1419506f,2.86172819f);
    private Vector3 waterWizardSpawnS2=new Vector3(6.45134544f,16.1419506f,1.90682364f);


    void OnTriggerEnter(Collider other){
        if (other.transform.gameObject.layer==LayerMask.NameToLayer("Player")){
            SceneManager.LoadScene(1);
            fireWizard.position=fireWizardSpawnS2;
            waterWizard.position=waterWizardSpawnS2;
        }
       
    }
}
