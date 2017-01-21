using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_PG : MonoBehaviour {

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    
    public Vector3 PuntoSpawn1;
    public Vector3 PuntoSpawn2;
    public Vector3 PuntoSpawn3;
    

    // Use this for initialization
    void Start () {
        GameObject ObjSpawn1 = (GameObject)Instantiate(P1, PuntoSpawn1,transform.rotation);
        GameObject ObjSpawn2 = (GameObject)Instantiate(P2, PuntoSpawn2, transform.rotation);
        GameObject ObjSpawn3 = (GameObject)Instantiate(P3, PuntoSpawn3, transform.rotation);
       
}
	


    
}
