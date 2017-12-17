using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public float remappedX;
    public float remappedY;

    public GameObject model1;
    public GameObject model2;
    public GameObject eyes;
    public GameObject billboard1;
    public GameObject billboard2;
    public GameObject backButton;


    public AudioSource[] sources;

    public GameObject billboard2Large;



    public GameObject controller1;
    public GameObject controller2;


    public bool readyToWarp = false;
    public bool modelBeingGrabbed;

    public Vector3 modelStartPos;
    public Vector3 billboard1StartScale;
    public Vector3 billboard2StartScale;


    public float modelDistanceToHead;

    public bool LargeMode2IsActive;


    // Use this for initialization
    void Start()
    {
        billboard1StartScale = billboard1.transform.localScale;
        billboard2StartScale = billboard2.transform.localScale;

        float remappedScaleX = Mathf.Lerp(0, 1, Mathf.InverseLerp(0, billboard1StartScale.x, remappedX));
        float remappedScaleY = Mathf.Lerp(0, 1, Mathf.InverseLerp(0, billboard1StartScale.y, remappedY));
        Vector3 hoverEnlargedScale = new Vector3(remappedScaleX, remappedScaleY, billboard1StartScale.z);


    }

    // Update is called once per frame
    void Update()
    {

        //assign vars
        modelStartPos = model1.GetComponent<ModelControl>().startPos;


        //-------------------------------------------------------------------------
        //                        UI MENU FUNCTIONALITY 
        //
        //   check if controllers are grabbing the model.  If true, warp to grabbed model
        //   zone by bringing the model close to the face.
        //-------------------------------------------------------------------------


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

    //-------------------------------------------------------------------------
    //                        UI MENU FUNCTIONALITY 
    //
    //   these functions work with HandController.  Enabling / Disabling appropriate model
    //   when selected with the laser
    //-------------------------------------------------------------------------

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

    public void billboardHover()
    {
          

    }

    public void LargeMode2()
    {
        billboard2Large.SetActive(true);
        backButton.SetActive(true);

        billboard1.SetActive(false);
        billboard2.SetActive(false);

        LargeMode2IsActive = true;

}

    public void BackToTileMode()
    {
        billboard1.SetActive(true);
        billboard2.SetActive(true);
        billboard2Large.SetActive(false);
        backButton.SetActive(false);

        LargeMode2IsActive = false;

    }
}
