using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topLevelSpawner : MonoBehaviour {

    public Vector3[] posArray=new Vector3[]{ 
            new Vector3(-43.5413933f,5.21150017f,-3.5192957f),
            new Vector3(-41.5413055f,5.21150017f,0.5192966f),
            new Vector3(-63.5413055f,5.21150017f,-5.5192966f),
            new Vector3(-55.5413933f,5.21150017f,0.480704308f),
            new Vector3(-51.5413933f,5.21150017f,7.4807043f),
            new Vector3(-55.5413933f,5.21150017f,8.48070431f),
            new Vector3(-63.5413094f,5.21150017f,8.48070335f)
    };
    Quaternion rotation =new Quaternion(0f,0.707106829f,0f,0.707106829f);
    public GameObject skellybob;
    public GameObject witchDR;
    public bool isSpawned=false;
    public Transform target;
    private Vector3 direction;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer==9 && isSpawned==false){
            foreach(Vector3 enemyPos in posArray){
                GameObject skeleton = GameObject.Instantiate(skellybob, enemyPos, rotation);   
                skeleton.transform.LookAt(target);         
            }
            isSpawned=true;
            Debug.Log(isSpawned);
        }       
    }
}
