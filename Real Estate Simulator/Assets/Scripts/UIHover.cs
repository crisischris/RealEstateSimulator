using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHover : MonoBehaviour
{


    public SteamVR_Controller.Device device;

    public HandController controllers;

    public float rotateSpeed = 50;
    public float sinFactor = 1f;


    public RectTransform rt;
    public float scaleFactor = 1f;
    private Vector3 scaledVector;
    private Vector3 originaScale;

    public string[] targetTags = new string[] { "Home", "Sun", "Moon", "Rain", "Dawn","Helper" };

    public RectTransform icon;


    Vector3 iconStartPos;


    // Use this for initialization
    void Start()
    {


        originaScale = gameObject.transform.localScale;

        Vector3 iconHStartPos = icon.transform.localPosition;
        scaledVector = new Vector3(scaleFactor, scaleFactor, scaleFactor);


    }

    // Update is called once per frame
    void Update()
    {
        device = controllers.device;

    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.CompareTag("Controller"))
        {
            device.TriggerHapticPulse(1000);
            rt.transform.Rotate(Vector3.forward, Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.CompareTag("Controller"))
        {
            //scaledVector = new Vector3(scaleFactor / Mathf.Sin(Time.time * sinFactor), scaleFactor / Mathf.Sin(Time.time * sinFactor), scaleFactor / Mathf.Sin(Time.time * sinFactor));
            rt.localScale = scaledVector;
            rt.transform.Rotate(Vector3.forward * rotateSpeed);
        }
    }

    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.CompareTag("Controller"))
        {
            rt.localScale = originaScale;
            //  icon.transform.position = iconStartPos;

        }
    }
}

