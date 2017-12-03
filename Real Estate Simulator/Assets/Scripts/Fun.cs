using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fun : MonoBehaviour {

    public List<GameObject> Orbs;
    public LineRenderer lr;
    private Vector3[] linePoints;

	// Use this for initialization
	void Start () {

        lr = gameObject.AddComponent<LineRenderer>();

        for (int i = 0; i < Orbs.Count; i++)
        {
            Orbs[i].transform.position = new Vector3(Random.Range(-5, 5), Random.Range(0, 5), Random.Range(-5, 5));
           
        }

	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < Orbs.Count; i++)
        {
            linePoints[i] = new Vector3(Orbs[i].transform.position.x, Orbs[i].transform.position.y, Orbs[i].transform.position.z);
            lr.SetPositions(linePoints);
            lr.startWidth = .01f;
            lr.startColor = new Color(1, 1, 1);
        }

        

    }
}
