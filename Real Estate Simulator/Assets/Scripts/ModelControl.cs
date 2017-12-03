using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ModelControl : MonoBehaviour
{

    public HandController handController1;
    public HandController handController2;

    public GameObject eyes;

    public int rotateSpeed = 10;
    public float vectorToLock = 0;
    public float speed = 1;
    float step;

    public float distanceFromStartPos;
    public float distanceToStartPosY;


    public Vector3 startPos;
    public Vector3 startPosHigh;

    public Vector3 currentPos;
    public Vector3 currentPosInParent;
    public string scene;

    public LineRenderer lr;



    public Rigidbody rb;

    public bool beingMoved = false;

    public float modelDistanceToHead;


    // Use this for initialization
    void Start()
    {

        startPos = gameObject.transform.position;
        startPosHigh = new Vector3(startPos.x, startPos.y + 5, startPos.z);
        step = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

        currentPos = gameObject.transform.position;
        distanceToStartPosY = 1 + startPos.y;
        startPosHigh = new Vector3(startPos.x, distanceFromStartPos, startPos.y);

        Debug.Log(modelDistanceToHead);

        //if(currentPos == startPos)
        //{
        //    rb.useGravity = true;
        //}

        if (handController1.isGrabbingModel == true || handController2.isGrabbingModel == true)
        {
            return;
        }

        else
        {
            if (currentPos != startPos)
            {
                beingMoved = true;

                if (currentPos.y < startPos.y)
                {
                    gameObject.transform.position = Vector3.MoveTowards(transform.position, startPosHigh, step);
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                else
                {

                    gameObject.transform.position = Vector3.MoveTowards(transform.position, startPos, step);
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

                }
            }

            if (currentPos == startPos)
            {
                beingMoved = false;
            }


            if (beingMoved == true)
            {

                modelDistanceToHead = Vector3.Distance(currentPosInParent, eyes.transform.localPosition);
                

                if (modelDistanceToHead < .15f)
                {
                    warp();
                }
            }
            //  if (gameObject.transform.position.y < startPos.y)
            //  
            //  gameObject.transform.position = Vector3.up*Time.deltaTime;
            //
            //  if (gameObject.transform.position.y >= startPos.y)
            //  {



        }


        distanceFromStartPos = Vector3.Distance(currentPos, startPos);


        //if (distanceFromStartPos > 0)
        //{
        //
        //    if (handController1.isGrabbingModel == true)
        //    {
        //       
        //    }
        //
        //        gameObject.transform.position = Vector3.MoveTowards(transform.position, startPos, step);
        //  //  if (handController1.isGrabbingModel == false)





        //rotate model
        transform.Rotate(Vector3.up * (Time.deltaTime * rotateSpeed));

        //gameObject.transform.Rotate(Vector3.up * (Time.deltaTime * rotateSpeed));
        //vectorToNotRotate = 0;



    }


    public void warp()
    {


        SteamVR_LoadLevel.Begin(scene, false, 2);


    }



}
