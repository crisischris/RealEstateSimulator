using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMenuManager : MonoBehaviour
{

    public bool helperMenuIsOff = false;
    public int helperMenuInt;


    public GameObject helperMenuParent;
    public List<GameObject> helperMenuText;
    public int currentMenuPlace = 0;
    public int IntroCounter = 0;

    public GameObject controller1;
    public GameObject controller2;

    public GameObject audioManager;
    public AudioSource[] sources;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //-------------------------------------------------------------------------
    //                        HELPER MENU FUNCTIONALITY 
    //
    //   helper menu 
    //-------------------------------------------------------------------------

    public void helperMenuRight()
    {
        sources[0].Play();
        helperMenuText[currentMenuPlace].SetActive(false);
        currentMenuPlace++;

        if (currentMenuPlace >= helperMenuText.Count)
        {
            helperMenuParent.SetActive(false);
            controller1.GetComponent<HandController>().helperMenuOn = false;
            controller2.GetComponent<HandController>().helperMenuOn = false;
            currentMenuPlace = 0;
            IntroCounter = 1;
        }

        else

            helperMenuText[currentMenuPlace].SetActive(true);
    }

    public void helperMenuLeft()
    {
        sources[0].Play();
        helperMenuText[currentMenuPlace].SetActive(false);
        currentMenuPlace--;
        if (currentMenuPlace < 0)
        {
            currentMenuPlace = 0;
        }

        helperMenuText[currentMenuPlace].SetActive(true);

    }

    public void TurnOnHelperBothControllers()
    {
        controller1.GetComponent<HandController>().helperMenuOn = true;
        controller2.GetComponent<HandController>().helperMenuOn = true;
    }
}
