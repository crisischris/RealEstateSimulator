using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

    private float height = 1;
    private float timeLag = 2;
    private float heightOffset = 8;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(transform.position.x, (height + Mathf.Sin( (Time.time * timeLag)) / heightOffset), transform.position.z);

    }
}
