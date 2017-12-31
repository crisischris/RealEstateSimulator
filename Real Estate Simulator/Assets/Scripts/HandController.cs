using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{

    public ObjectManager objectManager;
    public HelperMenuManager helperMenuManager;
    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;
    public bool helperMenuOn = false;
    public GameObject helperMenuParent;
    public Material nightBox;
    public Material dayBox;
    public bool laserHand;
    public bool menuIsActive = false;
    public GameObject sun;
    public GameObject playerHead;
    public GameObject uiMenu;
    public GameObject controllerMesh;
    public GameObject controllerPrefab;
    public GameObject nightSound;
    public GameObject daySound;
    public GameObject currentObjectBeingHit;
    private string scene = "HomeSpace";
    public Vector3 currentObjectBeingHitScale;
    public Vector3 currentObjectBeingHitScaleHover;
    public LineRenderer lr;
    public Color whiteMat = new Color(0, 0, 0, 0);
    public float distanceFromHeadThreshold = .6f;
    public float currentdistanceFromHeadMainHand;
    public float currentdistanceFromHeadAltHand;
    public float hoverEnlarged = 1.1f;
    public Vector3 controllerPositionHand;
    public Vector3 playerHeadPosition;
    public float controllerRotationX;
    public float controllerRotationY;
    public float controllerRotationZ;
    public float maxLaserLength = 7;
    public float distanceFromStartingPos;
    public float mappedScale;
    public bool menuXrotate = false;
    public bool menuYrotate = false;
    public bool menuZrotate = false;
    public bool menuRotateReady = false;
    public bool isGrabbingAnything = false;
    public bool isGrabbingModel = false;
    public bool laserForceOff = false;
    public LayerMask[] clickMask;
    public LayerMask currentLaserMaskBeingHit;
    public int grabCounter = 0;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        uiMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //assign Vars

        controllerPositionHand = controllerPrefab.transform.position;
        playerHeadPosition = playerHead.transform.position;
        device = SteamVR_Controller.Input((int)trackedObj.index);
        currentdistanceFromHeadMainHand = Vector3.Distance(controllerPositionHand, playerHeadPosition);
        controllerRotationX = controllerPrefab.transform.localEulerAngles.x;
        controllerRotationZ = controllerPrefab.transform.localEulerAngles.z;
        controllerRotationY = controllerPrefab.transform.localEulerAngles.y;
        menuXrotate = false;
        menuYrotate = false;
        menuZrotate = false;

        //-------------------------------------------------------------------------
        //                             LASER POINTER 
        //
        // this module allows the laser to enable when aiming @ an appropriate
        // layerMask.  this also checks for trigger input.
        // depending on layerMask, tigger input calls different functions
        //-------------------------------------------------------------------------


        if (helperMenuOn == false)
        {
            Time.timeScale = 1f;
            if (laserHand == true)
            {
                if (isGrabbingModel == false && menuIsActive == false)
                {
                    lr.SetPosition(0, gameObject.transform.position);
                    RaycastHit hit;
                    for (int i = 0; i < clickMask.Length; i++)
                    {
                        if (Physics.Raycast(transform.position, transform.forward, out hit, maxLaserLength, clickMask[i]))
                        {
                            lr.SetPosition(1, hit.point);
                            lr.enabled = true;
                            currentObjectBeingHit = hit.transform.gameObject;
                            currentLaserMaskBeingHit = hit.transform.gameObject.layer;

                            if (laserForceOff == true)
                            {
                                gameObject.GetComponent<LineRenderer>().enabled = false;

                            }

                            if (lr.enabled == true)
                            {
                                if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                                {
                                    if (currentLaserMaskBeingHit == LayerMask.NameToLayer("clickMask1"))
                                    {
                                        objectManager.sources[3].Play();
                                        objectManager.enableModel1();
                                        objectManager.LargeMode1();
                                        hapticTrigger();
                                    }

                                    if (currentLaserMaskBeingHit == LayerMask.NameToLayer("clickMask2"))
                                    {
                                        objectManager.sources[3].Play();
                                        objectManager.enableModel2();
                                        objectManager.LargeMode2();
                                        hapticTrigger();
                                    }

                                    if (currentLaserMaskBeingHit == LayerMask.NameToLayer("clickMask3"))
                                    {
                                        objectManager.sources[3].Play();
                                        objectManager.enableModel3();
                                        objectManager.LargeMode3();
                                        hapticTrigger();
                                    }

                                    if (currentObjectBeingHit.name == "BackButton")
                                    {
                                        objectManager.sources[3].Play();
                                        objectManager.BackToTileMode();
                                        hapticTrigger();
                                    }
                                }
                            }
                        }

                        else
                        {
                            lr.enabled = false;
                        }
                    }
                }
            }
        }


        //-------------------------------------------------------------------------
        //                             HELPER MENU
        //
        // this module allows the user to use only the grip and trigger button
        // to cylce through the helper menu.  once the menu is cycled through.
        // button maps return to normal state.
        //-------------------------------------------------------------------------


        if (helperMenuOn == true)
        {
            Time.timeScale = 0f;
            controllerMesh.SetActive(false);
            uiMenu.SetActive(false);
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                helperMenuManager.helperMenuRight();

            }

            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                helperMenuManager.helperMenuLeft();
            }
        }

        //-------------------------------------------------------------------------
        //                        UI MENU FUNCTIONALITY 
        //
        // this module allows the user to enable the interactive UI menu.
        // using grip button input, menu is enables and controller model 
        // is disabled
        //-------------------------------------------------------------------------

        if (helperMenuOn == false)
        {
            if (device.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                controllerMesh.SetActive(false);
                uiMenu.SetActive(true);
                menuIsActive = true;

                if (gameObject.GetComponent<LineRenderer>() == true)
                {
                    lr.enabled = false;
                }
            }

            else
            {
                uiMenu.SetActive(false);
                controllerMesh.SetActive(true);
                menuIsActive = false;
            }

            if (isGrabbingModel == true)
            {
                if (gameObject.GetComponent<LineRenderer>() == true)
                {
                    lr.enabled = false;
                }
            }
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Home"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                device.TriggerHapticPulse(3000);
                SteamVR_LoadLevel.Begin(scene, false, 2);
            }
        }

        if (col.gameObject.CompareTag("Night"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                device.TriggerHapticPulse(3000);

                RenderSettings.skybox = nightBox;
                sun.SetActive(false);
                nightSound.SetActive(true);
                daySound.SetActive(false);
            }
        }

        if (col.gameObject.CompareTag("Day"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                device.TriggerHapticPulse(3000);
                RenderSettings.skybox = dayBox;
                sun.SetActive(true);
                nightSound.SetActive(false);
                daySound.SetActive(true);
            }
        }

        if (col.gameObject.CompareTag("ModelHome"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                dropObjectModel(col);
                isGrabbingModel = false;
                StartCoroutine(delayLaser());
            }
            else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                grabObjectModel(col);
                isGrabbingModel = true;

                if (gameObject.GetComponent<LineRenderer>() == true)
                {
                    lr.enabled = false;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Contains("Model"))
        {
            device.TriggerHapticPulse(1000);
        }

        if (col.gameObject.CompareTag("Helper"))
        {
            device.TriggerHapticPulse(3000);
            helperMenuManager.TurnOnHelperBothControllers();
            helperMenuParent.SetActive(true);
        }

        if (col.gameObject.CompareTag("HelperIntro"))
        {
            device.TriggerHapticPulse(3000);
            helperMenuManager.TurnOnHelperBothControllers();
            helperMenuParent.SetActive(true);
        }

        if (col.gameObject.CompareTag("HomeIntro"))
        {
            gameObject.SetActive(false);
            device.TriggerHapticPulse(3000);
            SteamVR_LoadLevel.Begin(scene, false, 2);
        }
    }

    void grabObjectModel(Collider col)
    {
        col.transform.SetParent(gameObject.transform);
        col.GetComponent<Rigidbody>().isKinematic = true;
        device.TriggerHapticPulse(1000);
        isGrabbingModel = true;
    }

    void dropObjectModel(Collider col)
    {
        controllerMesh.SetActive(true);
        col.transform.SetParent(null);
        Rigidbody rigidBody = col.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        isGrabbingModel = false;
    }

    public void hapticTrigger()
    {
        device.TriggerHapticPulse(2000);
    }

    IEnumerator delayLaser()
    {
        laserForceOff = true;
        yield return new WaitForSeconds(.5f);
        laserForceOff = false;
    }
}
