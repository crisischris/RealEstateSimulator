using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandController : MonoBehaviour
{


    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;

    public GameObject playerHead;
    public GameObject uiMenu;
    public GameObject controllerModel;


    public float distanceFromHeadThreshold;
    public float currentdistanceFromHead;

    public Vector3 controllerPosition;
    public Vector3 playerHeadPosition;

    public float controllerRotationX;
    public float controllerRotationY;
    public float controllerRotationZ;


    public bool menuXrotate = false;
    public bool menuYrotate = false;
    public bool menuZrotate = false;
    public bool menuRotateReady = false;

    private int menuTimerCounter = 0;
    private int menuActiveInSeconds = 1;




    // Use this for initialization
    void Start()
    {

        trackedObj = GetComponent<SteamVR_TrackedObject>();
        controllerModel.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {


        //assign Vars

        controllerPosition = trackedObj.transform.position;
        playerHeadPosition = playerHead.transform.position;

        device = SteamVR_Controller.Input((int)trackedObj.index);

        currentdistanceFromHead = Vector3.Distance(controllerPosition, playerHeadPosition);



        controllerRotationX = trackedObj.transform.localEulerAngles.x;
        controllerRotationY = trackedObj.transform.localEulerAngles.y;
        controllerRotationZ = trackedObj.transform.localEulerAngles.z;


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



        if (currentdistanceFromHead <= distanceFromHeadThreshold && currentdistanceFromHead != 0 && menuRotateReady == true)
        {
            menuTimerCounter = 1;
            controllerModel.SetActive(false);
            uiMenu.SetActive(true);
            StartCoroutine(menuMinActiveTime());
        }

        else
        {
            if (menuTimerCounter <= 0)
            {
                uiMenu.SetActive(false);
                controllerModel.SetActive(true);
            }
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log(currentdistanceFromHead);
            Debug.Log("controller rotation X = " + controllerRotationX);
            Debug.Log("controller rotation Y = " + controllerRotationY);
            Debug.Log("controller rotation Z = " + controllerRotationZ);
        }

    }


    // provides a min number of seconds for UI menu to stay up
    // this will prevent a flashing UI on and off if the user accidentally triggers menu active

    IEnumerator menuMinActiveTime()
    {
        menuTimerCounter = 1;
        yield return new WaitForSeconds(menuActiveInSeconds);
        menuTimerCounter = 0;
    }
}

