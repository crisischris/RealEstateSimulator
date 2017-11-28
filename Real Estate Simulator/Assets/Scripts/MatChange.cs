using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChange : MonoBehaviour {


    public Material red;
    private Material objectMaterial;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.CompareTag("Controller"))
        {

            objectMaterial = gameObject.GetComponent<Renderer>().material;
            objectMaterial = red;

            Debug.Log("should turn red");
        }

    }
    */
}
