using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public GameObject model1;
    public GameObject model2;
    public GameObject eyes;

    public GameObject controller1;
    public GameObject controller2;


    public bool readyToWarp = false;
    public bool modelBeingGrabbed;

    public Vector3 modelStartPos;

    public float modelDistanceToHead;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //assign vars

        modelStartPos = model1.GetComponent<ModelControl>().startPos;

        // check if controller 1 is grabbing the model

        if (controller1.GetComponent<HandController>().isGrabbingModel == true)
        {

            modelDistanceToHead = Vector3.Distance(controller1.transform.position, eyes.transform.position);
           // Debug.Log(modelDistanceToHead);
            if (modelDistanceToHead < .25f)
            {
                readyToWarp = true;                
            }

            else
            {
                readyToWarp = false;
            }
        }

        if (controller2.GetComponent<HandController>().isGrabbingModel == true)
        {

            modelDistanceToHead = Vector3.Distance(controller2.transform.position, eyes.transform.position);
            if (modelDistanceToHead < .25f)
            {
                readyToWarp = true;
            }

            else
            {
                readyToWarp = false;
            }
        }
        
    }

    public void enableModel1()
    {
        model1.SetActive(true);
        model2.SetActive(false);
    }

    public void enableModel2()
    {
        model2.SetActive(true);
        model1.SetActive(false);
    }
}
