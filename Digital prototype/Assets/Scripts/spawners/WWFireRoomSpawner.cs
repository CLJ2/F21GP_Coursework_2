using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWFireRoomSpawner : MonoBehaviour
{
     private Vector3[] posArray=new Vector3[]{ 
            new Vector3(-101f,-0.503276587f,-25f),
            new Vector3(-105f,-0.503276587f,-22.5042286f),
            new Vector3(-107f,-0.503276587f,-16f),
            new Vector3(-50f,20.5603294f,-27.255125f),
            new Vector3(-50f,20.5603294f,-23f),
            new Vector3(-50f,20f,-22f)
    };
   
    Quaternion rotation =new Quaternion(-0.707106829,0,0,0.707106829);
    public GameObject skellybob;
    public GameObject witchDR;
    public bool isSpawned=false;
    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer==9 && isSpawned==false){
            foreach(Vector3 enemyPos in posArray){            
                Instantiate(skellybob, enemyPos, rotation);            
            }
            isSpawned=true;
            Debug.Log(isSpawned);
        }      
         
    }
}
