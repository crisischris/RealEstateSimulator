using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public bool laserHand;
    public bool menuIsActive = false;






    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;
    public ObjectManager objectManager;

    public GameObject playerHead;
    public GameObject uiMenu;
    public GameObject controllerMesh;
    public GameObject controllerPrefab;

    public GameObject currentObjectBeingHit;

    public string scene;

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



       // Debug.Log(currentObjectBeingHit);


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



                        // currentObjectBeingHitScale = currentObjectBeingHit.transform.localScale;
                        //
                        // currentObjectBeingHit.transform.localScale = hoverEnlargedScale;
                        //
                        // if(hoverEnlargedScale.x >  * hoverEnlarged)
                        // {
                        //     hoverEnlargedScale = currentObjectBeingHit.transform.localScale;
                        // }
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


                            // currentObjectBeingHit.transform.localScale = currentObjectBeingHitStartScale;

                           // Debug.Log("laser is on something");
                            
                        }
                        
                    }

                    else
                    {
                        lr.enabled = false;
                    }
                }
            }
        }



        //if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, clickMask[2]))
        //{
        //    currentObjectBeingHit = hit.transform.gameObject;
        //    lr.SetPosition(1, hit.point);
        //    lr.enabled = true;
        //}

        /*
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, clickMask2))
                {
                    lr.SetPosition(1, hit.point);
                    lr.enabled = true;

                    if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        objectManager.enableModel2();
                    }
                    //     Debug.Log(hit.point);

                }
                else
                {
                    lr.enabled = false;
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

            */


        //-------------------------------------------------------------------------
        //                        UI MENU FUNCTIONALITY 
        //
        // this module allows the user to enable the interactive UI menu.
        // using grip button input, menu is enables and controller model 
        // is disabled
        //-------------------------------------------------------------------------

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

        /*

         //scale object being grabbed by a remapped
         //scale of the distance of the object to HMD
         
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
  */

    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Home"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                device.TriggerHapticPulse(3000);
                // Debug.Log("controller pressed and hit button");
                SteamVR_LoadLevel.Begin(scene, false, 2);
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

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Contains("Model"))
        {
            device.TriggerHapticPulse(1000);
        }
    }

    void grabObjectModel(Collider col)
    {
        //Debug.Log("grabbed");
        col.transform.SetParent(gameObject.transform);
        col.GetComponent<Rigidbody>().isKinematic = true;
        device.TriggerHapticPulse(1000);
        isGrabbingModel = true;
    }

    void dropObjectModel(Collider col)
    {
        //Debug.Log("dropped");
        controllerMesh.SetActive(true);
        col.transform.SetParent(null);
        Rigidbody rigidBody = col.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        isGrabbingModel = false;
    }




    /*  {
      public void checkRaycastHitBillboard()
          lr.SetPosition(0, gameObject.transform.position);
          RaycastHit hit;


          for (int i = 0; i < clickMask.Length; i++)
          {
              if (isGrabbingModel == false)
              {
                  if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, clickMask[i]))
                  {
                      lr.SetPosition(1, hit.point);
                      lr.enabled = true;
                      currentObjectBeingHit = hit.transform.gameObject;
                      // currentObjectBeingHitScale = currentObjectBeingHit.transform.localScale;
                      // Vector3 hoverEnlargedScale = new Vector3(currentObjectBeingHit.transform.localScale.x * hoverEnlarged, currentObjectBeingHit.transform.localScale.y * hoverEnlarged, currentObjectBeingHit.transform.localScale.z * hoverEnlarged);
                      // currentObjectBeingHit.transform.localScale = hoverEnlargedScale;
                      //
                      // if(hoverEnlargedScale.x >  * hoverEnlarged)
                      // {
                      //     hoverEnlargedScale = currentObjectBeingHit.transform.localScale;
                      // }

                      if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                      {
                          objectManager.enableModel1();
                          hapticTrigger();
                      }

                      else

                          lr.enabled = false;
                      // currentObjectBeingHit.transform.localScale = currentObjectBeingHitStartScale;
                  }



              }
          }
      }

      */
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

/*

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, clickMask[1]))
        {
            lr.SetPosition(1, hit.point);
            lr.enabled = true;
            currentObjectBeingHit = hit.transform.gameObject;


            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                objectManager.enableModel2();
                hapticTrigger();
}

        }


    }
    else
    {
        lr.enabled = false;
    }
}
}
*/
