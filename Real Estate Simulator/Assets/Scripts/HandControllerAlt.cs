using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllerAlt : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;

    public GameObject controllerModel;

    private bool isTargetTag;

    public string[] targetTags = new string[] { "Home","Sun","Moon","Rain","Dawn"};



    


    // Use this for initialization
    void Start()
    {

        trackedObj = GetComponent<SteamVR_TrackedObject>();
        controllerModel.SetActive(true);
        
 
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    //UI Click options
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
    }
}



