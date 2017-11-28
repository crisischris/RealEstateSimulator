using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHover : MonoBehaviour
{

    public RectTransform rt;
    public float scaleFactor = 1.1f;
    private Vector3 scaledVector;
    private Vector3 originaScale;


    // Use this for initialization
    void Start()
    {
        originaScale = gameObject.transform.localScale;
        scaledVector = new Vector3(scaleFactor, scaleFactor, scaleFactor);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.CompareTag("Controller"))
        {
            rt.localScale = scaledVector;
        }
    }

    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.CompareTag("Controller"))
        {
            rt.localScale = originaScale;
        }
    }
}
