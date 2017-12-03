using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    public int rotateSpeed = 10;
    public float vectorToLock = 0;


	// Use this for initialization
	void Start () {

      

    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up * (Time.deltaTime * rotateSpeed));

        //gameObject.transform.Rotate(Vector3.up * (Time.deltaTime * rotateSpeed));
        //vectorToNotRotate = 0;



	}
}
