using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMenuIntro : MonoBehaviour {

    public HelperMenuManager helperMenuManager;

    public GameObject helperParent;
    public GameObject thisParent;
    public GameObject Home;
    public GameObject help;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (helperParent.activeInHierarchy)
        {
            help.SetActive(false);
        }	

        if(helperMenuManager.IntroCounter == 1)
        {
            Home.SetActive(true);

        }

	}
}
