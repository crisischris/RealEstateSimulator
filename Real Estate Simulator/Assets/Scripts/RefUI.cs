using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RefUI : MonoBehaviour

{
    /*

    
    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;

    public GameObject playerHead;
    public GameObject uiMenu;
    public GameObject mainHandModel;
    public GameObject altHandModel;
    public GameObject menuHand;
    public GameObject altHand;
    public GameObject objectBeingGrabbed;

    public Color whiteMat = new Color(0,0,0,0);

    //model home objects

    public GameObject modelHome1;
    public GameObject modelHome2;



    public float distanceFromHeadThreshold = .6f;
    public float currentdistanceFromHeadMainHand;
    public float currentdistanceFromHeadAltHand;

    public Vector3 controllerPositionMainHand;
    public Vector3 controllerPositionAltHand;
    public Vector3 playerHeadPosition;

    public Vector3 modelHome1StartPos;
    public Vector3 modelHome2StartPos;

    public float controllerRotationX;
    public float controllerRotationY;
    public float controllerRotationZ;

    public float distanceFromStartingPos;

    public float mappedScale;


    public bool menuXrotate = false;
    public bool menuYrotate = false;
    public bool menuZrotate = false;
    public bool menuRotateReady = false;
    public bool isGrabbing = false;


    //public string[] targetTags = new string[] {"ModelHome","Respawn"};




    // Use this for initialization
    void Start()
    {

        trackedObj = GetComponent<SteamVR_TrackedObject>();
        mainHandModel.SetActive(true);
        altHandModel.SetActive(true);

       

    }

    // Update is called once per frame
    void Update()
    {

        //assign Vars

         Vector3 modelHome1CurrentPos = new Vector3(modelHome1.transform.position.x, modelHome1.transform.position.y, modelHome1.transform.position.z);

       //StartCoroutine(objectsWaitToFall());

        modelHome1StartPos = modelHome1.transform.position;
        modelHome2StartPos = modelHome2.transform.position;

        controllerPositionMainHand = menuHand.transform.position;
        controllerPositionAltHand = altHand.transform.position;

        playerHeadPosition = playerHead.transform.position;

        device = SteamVR_Controller.Input((int)trackedObj.index);

        currentdistanceFromHeadMainHand = Vector3.Distance(controllerPositionMainHand, playerHeadPosition);
        currentdistanceFromHeadAltHand = Vector3.Distance(controllerPositionAltHand, playerHeadPosition);

        


        controllerRotationX = menuHand.transform.localEulerAngles.x;
        controllerRotationZ = menuHand.transform.localEulerAngles.z;
        controllerRotationY = menuHand.transform.localEulerAngles.y;


        menuXrotate = false;
        menuYrotate = false;
        menuZrotate = false;


        //Check controller's rotation


            if (controllerRotationX <= 30 || controllerRotationX >= 330)
            {

                menuXrotate = true;

            }

            else
            {
                menuXrotate = false;
            }

            if (controllerRotationZ <= 360 && controllerRotationZ >= 260)
            {

                menuZrotate = true;

            }

            else
            {
                menuZrotate = false;
            }

            if (menuXrotate == true && menuZrotate == true)
            {
                menuRotateReady = true;
            }

            else
            {
                menuRotateReady = false;
            }

            //Check distance from controller to HMD

            if (currentdistanceFromHeadMainHand <= distanceFromHeadThreshold && currentdistanceFromHeadMainHand != 0 && menuRotateReady == true)
            {
                mainHandModel.SetActive(false);
                uiMenu.SetActive(true);
                //StartCoroutine(menuMinActiveTime());
            }

            else
            {
                    uiMenu.SetActive(false);
                    mainHandModel.SetActive(true);
                
            }

        // debug controller transform location.

        /*
          if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log(currentdistanceFromHeadMainHand);
            Debug.Log("controller rotation X = " + controllerRotationX);
            Debug.Log("controller rotation Y = " + controllerRotationY);
            Debug.Log("controller rotation Z = " + controllerRotationZ);
        }

    
        // scale object being grabbed by a remapped
        // scale of the distance of the object to HMD

       

        if (isGrabbing == true)
        {
           
            if (modelHome1.transform.position != modelHome1StartPos)
            {

                float mappedScale = Mathf.Lerp(1f, .25f, Mathf.InverseLerp(0, .5f, currentdistanceFromHeadAltHand));
                modelHome1.transform.localScale = new Vector3(mappedScale, mappedScale, mappedScale);
                LineRenderer lr = modelHome1.GetComponent<LineRenderer>();
                lr.enabled = true;
                lr.SetPosition(1, modelHome1StartPos);
                lr.startWidth = .025f;
                lr.startColor = whiteMat;



                Debug.Log(mappedScale);
               
            }

            if (modelHome2.transform.position != modelHome2StartPos)
            {

                float mappedScale = Mathf.Lerp(1, .25f, Mathf.InverseLerp(0, .5f, currentdistanceFromHeadAltHand));
                modelHome2.transform.localScale = new Vector3(mappedScale, mappedScale, mappedScale);
                Debug.Log(mappedScale);
               
            }
            
        }

        if (isGrabbing == false && modelHome1.transform.position != modelHome1StartPos)
        {
            if (modelHome1.transform.position != modelHome1StartPos)
                moveModelHomesBack();
        }

    }


    // provides a min number of seconds for UI menu to stay up
    // this will prevent a flashing UI on and off if the user accidentally triggers menu active

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Home"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                device.TriggerHapticPulse(3000);
                Debug.Log("controller pressed and hit button");
                SteamVR_LoadLevel.Begin("Home", false, 2);
            }
        }

        if (col.gameObject.CompareTag("ModelHome"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                dropObject(col);
                

            }
            else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(col);
                mainHandModel.SetActive(false);
                altHandModel.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
            if (col.gameObject.tag.Contains("Model"))

            {
                device.TriggerHapticPulse(1000);
            }
    }

    void GrabObject(Collider col)
    {
        col.transform.SetParent(gameObject.transform);
        col.GetComponent<Rigidbody>().isKinematic = true;
        device.TriggerHapticPulse(1000);        
        isGrabbing = true;
        mainHandModel.SetActive(false);
        altHandModel.SetActive(false);
    }

    void dropObject(Collider col)
    {
        col.transform.SetParent(null);
        Rigidbody rigidBody = col.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.velocity = device.velocity;
        rigidBody.angularVelocity = device.angularVelocity;
        isGrabbing = false;
        mainHandModel.SetActive(true);
        altHandModel.SetActive(true);
        objectBeingGrabbed = null;
    }

    public void moveModelHomesBack()
    {
        modelHome1.transform.position = modelHome1StartPos;
        modelHome2.transform.position = modelHome2StartPos;
        Debug.Log("this should be moving back");

    }


    IEnumerator objectsWaitToFall()
    {
        yield return new WaitForSeconds(2);
        modelHome1StartPos = modelHome1.transform.position;
        modelHome2StartPos = modelHome2.transform.position;
        Debug.Log(modelHome1StartPos);


    }
*/
}

