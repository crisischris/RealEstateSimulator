using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public GameObject model1;
    public GameObject model2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
